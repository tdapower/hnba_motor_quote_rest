using MNBQuotation_V2.Controllers.Quotation.Quotation;
using MNBQuotation_V2.Controllers.User;
using MNBQuotation_V2.Models.Quotation;
using MNBQuotation_V2.Models.Quotation.Quotation;
using MNBQuotation_V2.Models.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{
    public class QuotationCoverTakafulController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();


        [HttpGet]
        [ActionName("GetQuotationCoversByJobId")]
        public IEnumerable<QuotationCover> Get(int id)
        {
            List<QuotationCover> quotationCoverList = new List<QuotationCover>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT  " +
                "CASE WHEN A.JOB_ID   IS NULL THEN 0  ELSE A.JOB_ID   END," + //0
                "CASE WHEN A.REVISION_NO  IS NULL THEN 0  ELSE A.REVISION_NO   END," +//1
                "CASE WHEN A.POLICY_COVER_CODE  IS NULL THEN ''  ELSE A.POLICY_COVER_CODE   END," +//2
                "CASE WHEN A.COVER_TYPE IS NULL THEN ''  ELSE A.COVER_TYPE   END," +//3
                "CASE WHEN A.COVER_AMOUNT IS NULL THEN ''  ELSE A.COVER_AMOUNT   END " +//4
                " FROM MNB_T_POLICY_COVER_LIST A  " +
                "WHERE  A.JOB_ID=:V_JOB_ID ORDER BY A.JOB_ID";


            OracleCommand command = new OracleCommand(sql, con);

            command.Parameters.Add(new OracleParameter("V_JOB_ID", id));
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();


                quotationCoverList = (from DataRow drow in dt.Rows
                                      select new QuotationCover()
                                      {
                                          JobId = Convert.ToInt32(drow[0].ToString()),
                                          Cover = drow[2].ToString(),
                                          Type = drow[3].ToString(),
                                          Amount = drow[4].ToString()

                                      }).ToList();



            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }

            }
            return quotationCoverList;
        }



        [HttpPost]
        public HttpResponseMessage AddQuotationCoverDetails(PostQuotationCoverRequest postQuotationCoverRequest)
        {

            // api/QuotationCover/AddQuotationCoverDetails?jobId=54163&RevisionId=0

            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());


            QuotationMainTakafulController quotationMainController = new QuotationMainTakafulController();

            QuotationMainTakaful quotationMain;


            CoverController coverController = new CoverController();


            List<Cover> allowedCoverList = coverController.GetAllowedCovers(user.Company);


            DeleteCovers(Convert.ToInt32(postQuotationCoverRequest.JobId), Convert.ToInt32(postQuotationCoverRequest.RevisionId));

            foreach (QuotationCover cover in postQuotationCoverRequest.QuotationCovers)
            {
                if (cover.Cover == "chk4" || cover.Cover == "chk5" || cover.Cover == "chk8")
                {
                    if (!validateCoverAmount(cover.Cover, cover.Amount))
                    {

                        var message = string.Format("Amount of cover {0} not in the allowed range", cover.Cover);
                        HttpError err = new HttpError(message);
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, err);
                    }
                }

                //To validate the allowed covers
                var item = allowedCoverList.Find(x => x.CoverCode == cover.Cover);

                if (item == null)
                {
                    continue;
                }
                ///////////

                OracleConnection con = new OracleConnection(ConnectionString);
                try
                {

                    con.Open();
                    OracleCommand cmd = null;

                    cmd = new OracleCommand("MNB_T_INSERT_MTR_COVER_DETAILS");

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    cmd.Parameters.Add("jobid", OracleType.Int32).Value = cover.JobId;
                    cmd.Parameters.Add("revision_id", OracleType.VarChar).Value = cover.RevisionId;
                    cmd.Parameters.Add("cover", OracleType.VarChar).Value = cover.Cover;
                    cmd.Parameters.Add("pol_type", OracleType.VarChar).Value = cover.Type;
                    cmd.Parameters.Add("amount", OracleType.VarChar).Value = cover.Amount;



                    cmd.ExecuteNonQuery();
                    con.Close();



                    //saveDefaultCovers(postQuotationCoverRequest.JobId, user);

                }
                catch (Exception ex)
                {
                    con.Close();
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
            }




            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public void saveDefaultCovers(int jobId, UserAccount user)
        {

            QuotationMain quotationMain;

            QuotationMainController quotationMainController = new QuotationMainController();

            quotationMain = quotationMainController.Get(Convert.ToInt32(jobId));


            if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
            {
                string[] defaultCoverCodes = new string[5];

                defaultCoverCodes[0] = "chk18";//Flood
                defaultCoverCodes[1] = "chk19";//Natural Perils
                defaultCoverCodes[2] = "chk24";//Adjustment Fee
                defaultCoverCodes[3] = "chk26";//SRCC - for Vehicles
                defaultCoverCodes[4] = "multiple rebate";


                for (int a = 0; a < defaultCoverCodes.Length; a++)
                {
                    if (defaultCoverCodes[a] == "multiple rebate")
                    {
                        double allowedMaxMR = 0.00;
                        MRRateController mRRateController = new MRRateController();

                        allowedMaxMR = mRRateController.getRelevantMRRate(quotationMain.RiskTypeId, quotationMain.VehicleUsageId, quotationMain.FuelTypeCode, quotationMain.SumInsured, user.Company, quotationMain.MakeModelCode, quotationMain.YearOfManufactureValidationId);


                        SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, defaultCoverCodes[a], "0", allowedMaxMR.ToString());
                    }
                    else
                    {
                        SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, defaultCoverCodes[a], "0", "0");
                    }

                }

                //save stamp duty for private
                if (quotationMain.VehicleUsageId == Properties.Settings.Default.MNBQUsagePrivate)
                {
                    SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, "chk20", "0", "0");
                }


                //Up front ncb - 1 year
                if (!new int[] { Properties.Settings.Default.MNBQMotorCycleRiskTypeId, Properties.Settings.Default.MNBQThreeWheelerRiskType }.Contains(quotationMain.RiskTypeId))
                {
                    if (!new int[] { Properties.Settings.Default.FuelTypeElectric, Properties.Settings.Default.FuelTypeHybrid }.Contains(quotationMain.FuelTypeCode))
                    {
                        SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, "chk10", "1 Year", "20");
                    }

                }

                //	Inclusion of Excluded Items for hiring
                if (quotationMain.VehicleUsageId == Properties.Settings.Default.MNBQUsageHiring)
                {
                    SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, "chk14", "0", "0");
                }

                //	TPPD -  for vehicles except Motor Car(Private)
                if (quotationMain.RiskTypeId == Properties.Settings.Default.MNBQMotorCarRiskTypeId && quotationMain.VehicleUsageId == Properties.Settings.Default.MNBQUsagePrivate)
                {

                }
                else
                {
                    SaveDefaultCovers(quotationMain.JobId, quotationMain.RevisionNo, "chk12", "500,000.00", "0");
                }

            }

        }

        private bool validateCoverAmount(string coverCode, string amount)
        {
            bool returnVal = false;
            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {

                OracleDataReader dr;
                con.Open();
                String sql = "";
                sql = "select t.* from mnb_cover_amount_range t where t.cover_code =:V_COVER_CODE and t.min_amount <=:V_AMOUNT and t.max_amount >= :V_AMOUNT";

                OracleCommand command = new OracleCommand(sql, con);

                command.Parameters.Add(new OracleParameter("V_COVER_CODE", coverCode));
                command.Parameters.Add(new OracleParameter("V_AMOUNT", amount));

                dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    returnVal = true;
                }

                dr.Close();
                dr.Dispose();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }


            return returnVal;
        }

        private void SaveDefaultCovers(int jobId, int revisionNo, string cover, string coverType, string amount)
        {
            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {

                con.Open();
                OracleCommand cmd = null;

                cmd = new OracleCommand("MNB_T_INSERT_MTR_COVER_DETAILS");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("jobid", OracleType.Int32).Value = jobId;
                cmd.Parameters.Add("revision_id", OracleType.VarChar).Value = revisionNo;
                cmd.Parameters.Add("cover", OracleType.VarChar).Value = cover;
                cmd.Parameters.Add("pol_type", OracleType.VarChar).Value = coverType;
                cmd.Parameters.Add("amount", OracleType.VarChar).Value = amount;



                cmd.ExecuteNonQuery();
                con.Close();



            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        public void DeleteCovers(int jobId, int revisionId)
        {
            OracleConnection con = new OracleConnection(ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from MNB_T_POLICY_COVER_LIST t  where t.JOB_ID=:V_JOB_ID AND REVISION_NO=:V_REVISION_NO";

            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", jobId));
            cmd.Parameters.Add(new OracleParameter("V_REVISION_NO", revisionId));

            cmd.Connection = con;
            con.Open();
            int rowDeleted = cmd.ExecuteNonQuery();
            con.Close();
        }






    }
}
