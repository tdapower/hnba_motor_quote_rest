using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MNBQuotation_V2.Controllers.User;
using MNBQuotation_V2.Filters;
using MNBQuotation_V2.Models.Quotation;
using MNBQuotation_V2.Models.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;


namespace MNBQuotation_V2.Controllers.Quotation
{
    public class QuotationMainTakafulController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<QuotationMainTakaful> Get()
        {
            List<QuotationMainTakaful> quotationMainList = new List<QuotationMainTakaful>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);



            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());

            string sql = "";


            OracleCommand cmd = null;

            if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
            {

                sql = " SELECT " +
                               "CASE WHEN MM.QUOTATION_NO        IS NULL THEN ''  ELSE MM.QUOTATION_NO       END, " +
                               "CASE WHEN MM.JOB_ID               IS NULL THEN 0  ELSE MM.JOB_ID              END, " +
                               "CASE WHEN MM.REQUEST_BY         IS NULL THEN ''  ELSE MM.REQUEST_BY        END, " +
                               "CASE WHEN MM.CLIENT_NAME         IS NULL THEN ''  ELSE MM.CLIENT_NAME        END, " +
                               "CASE WHEN MM.VEHICLE_CHASIS_NO   IS NULL THEN ''  ELSE MM.VEHICLE_CHASIS_NO  END, " +
                               "CASE WHEN MM.RISK_TYPE_ID   IS NULL THEN 0  ELSE MM.RISK_TYPE_ID  END, " +
                               "CASE WHEN MM.VEHICLE_TYPE_ID     IS NULL THEN 0  ELSE MM.VEHICLE_TYPE_ID    END, " +
                               "CASE WHEN MM.VEHICLE_CLASS_ID    IS NULL THEN 0  ELSE MM.VEHICLE_CLASS_ID   END, " +
                               "CASE WHEN MM.SUM_INSURED         IS NULL THEN 0  ELSE MM.SUM_INSURED        END, " +
                               "CASE WHEN MM.PERIOD_TYPE_CODE    IS NULL THEN 0  ELSE MM.PERIOD_TYPE_CODE   END, " +
                               "CASE WHEN MM.PERIOD_CODE  IS NULL THEN 0  ELSE MM.PERIOD_CODE END, " +
                               "CASE WHEN MM.AGENT_BROKER       IS NULL THEN ''  ELSE MM.AGENT_BROKER      END, " +
                               "CASE WHEN MM.LEASING_TYPE        IS NULL THEN 0  ELSE MM.LEASING_TYPE       END, " +
                               "CASE WHEN MM.FUEL_TYPE_CODE  IS NULL THEN 0  ELSE MM.FUEL_TYPE_CODE END, " +
                               "CASE WHEN MM.PRODUCT_CODE  IS NULL THEN 0  ELSE MM.PRODUCT_CODE END, " +
                               "CASE WHEN MM.BRANCH_ID           IS NULL THEN ''  ELSE MM.BRANCH_ID          END, " +
                               "CASE WHEN MM.REMARK              IS NULL THEN ''  ELSE MM.REMARK             END, " +
                               "CASE WHEN MM.REQUEST_DATE        IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')  ELSE to_date( MM.REQUEST_DATE,'DD/MM/RRRR')       END, " +
                               "CASE WHEN MM.STATUS              IS NULL THEN 0  ELSE MM.STATUS             END, " +
                               "CASE WHEN MM.USER_ID             IS NULL THEN ''  ELSE MM.USER_ID            END, " +
                               "CASE WHEN MM.REVISION_NO  IS NULL THEN 0  ELSE MM.REVISION_NO END, " +
                               "CASE WHEN MM.QUOT_YEAR           IS NULL THEN ''  ELSE MM.QUOT_YEAR          END, " +
                               "CASE WHEN MM.AGENT_BROKER_CODE   IS NULL THEN ''  ELSE MM.AGENT_BROKER_CODE  END, " +
                               "CASE WHEN MM.YEAR_OF_MANU_VALIDATION           IS NULL THEN ''  ELSE MM.YEAR_OF_MANU_VALIDATION          END, " +
                               "CASE WHEN MM.MAKE_AND_MODEL_CODE   IS NULL THEN 0  ELSE MM.MAKE_AND_MODEL_CODE  END " +
                                " FROM MNBQ_T_MAIN MM  " +
                                " WHERE MM.USER_ID =:V_USER_ID  ";

                cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(new OracleParameter("V_USER_ID", user.UserName));
            }
            else
            {
                sql = " SELECT " +
                  "CASE WHEN MM.QUOTATION_NO        IS NULL THEN ''  ELSE MM.QUOTATION_NO       END, " +
                  "CASE WHEN MM.JOB_ID               IS NULL THEN 0  ELSE MM.JOB_ID              END, " +
                  "CASE WHEN MM.REQUEST_BY         IS NULL THEN ''  ELSE MM.REQUEST_BY        END, " +
                  "CASE WHEN MM.CLIENT_NAME         IS NULL THEN ''  ELSE MM.CLIENT_NAME        END, " +
                  "CASE WHEN MM.VEHICLE_CHASIS_NO   IS NULL THEN ''  ELSE MM.VEHICLE_CHASIS_NO  END, " +
                  "CASE WHEN MM.RISK_TYPE_ID   IS NULL THEN 0  ELSE MM.RISK_TYPE_ID  END, " +
                  "CASE WHEN MM.VEHICLE_TYPE_ID     IS NULL THEN 0  ELSE MM.VEHICLE_TYPE_ID    END, " +
                  "CASE WHEN MM.VEHICLE_CLASS_ID    IS NULL THEN 0  ELSE MM.VEHICLE_CLASS_ID   END, " +
                  "CASE WHEN MM.SUM_INSURED         IS NULL THEN 0  ELSE MM.SUM_INSURED        END, " +
                  "CASE WHEN MM.PERIOD_TYPE_CODE    IS NULL THEN 0  ELSE MM.PERIOD_TYPE_CODE   END, " +
                  "CASE WHEN MM.PERIOD_CODE  IS NULL THEN 0  ELSE MM.PERIOD_CODE END, " +
                  "CASE WHEN MM.AGENT_BROKER       IS NULL THEN ''  ELSE MM.AGENT_BROKER      END, " +
                  "CASE WHEN MM.LEASING_TYPE        IS NULL THEN 0  ELSE MM.LEASING_TYPE       END, " +
                  "CASE WHEN MM.FUEL_TYPE_CODE  IS NULL THEN 0  ELSE MM.FUEL_TYPE_CODE END, " +
                  "CASE WHEN MM.PRODUCT_CODE  IS NULL THEN 0  ELSE MM.PRODUCT_CODE END, " +
                  "CASE WHEN MM.BRANCH_ID           IS NULL THEN ''  ELSE MM.BRANCH_ID          END, " +
                  "CASE WHEN MM.REMARK              IS NULL THEN ''  ELSE MM.REMARK             END, " +
                  "CASE WHEN MM.REQUEST_DATE        IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')  ELSE to_date( MM.REQUEST_DATE,'DD/MM/RRRR')       END, " +
                  "CASE WHEN MM.STATUS              IS NULL THEN 0  ELSE MM.STATUS             END, " +
                  "CASE WHEN MM.USER_ID             IS NULL THEN ''  ELSE MM.USER_ID            END, " +
                  "CASE WHEN MM.REVISION_NO  IS NULL THEN 0  ELSE MM.REVISION_NO END, " +
                  "CASE WHEN MM.QUOT_YEAR           IS NULL THEN ''  ELSE MM.QUOT_YEAR          END, " +
                  "CASE WHEN MM.AGENT_BROKER_CODE   IS NULL THEN ''  ELSE MM.AGENT_BROKER_CODE  END, " +
                  "CASE WHEN MM.YEAR_OF_MANU_VALIDATION           IS NULL THEN ''  ELSE MM.YEAR_OF_MANU_VALIDATION          END, " +
                  "CASE WHEN MM.MAKE_AND_MODEL_CODE   IS NULL THEN 0  ELSE MM.MAKE_AND_MODEL_CODE  END " +
                   " FROM MNBQ_T_MAIN MM  ";

                cmd = new OracleCommand(sql, con);
            }



            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                quotationMainList = (from DataRow drow in dt.Rows
                                     select new QuotationMainTakaful()
                                     {
                                         QuotationNo = drow[0].ToString(),
                                         JobId = Convert.ToInt32(drow[1].ToString()),
                                         RequestBy = drow[2].ToString(),
                                         ClientName = drow[3].ToString(),
                                         VehicleChasisNo = drow[4].ToString(),
                                         RiskTypeId = Convert.ToInt32(drow[5].ToString()),
                                         VehicleTypeId = Convert.ToInt32(drow[6].ToString()),
                                         VehicleUsageId = Convert.ToInt32(drow[7].ToString()),
                                         SumInsured = Convert.ToDouble(drow[8].ToString()),
                                         PeriodTypeCode = Convert.ToInt32(drow[9].ToString()),
                                         PeriodOfCoverCode = Convert.ToInt32(drow[10].ToString()),
                                         AgentBroker = drow[11].ToString(),
                                         LeasingType = Convert.ToInt32(drow[12].ToString()),
                                         FuelTypeCode = Convert.ToInt32(drow[13].ToString()),
                                         ProductCode = Convert.ToInt32(drow[14].ToString()),
                                         BranchId = drow[15].ToString(),
                                         Remark = drow[16].ToString(),
                                         RequestDate = drow[17].ToString(),
                                         Status = drow[18].ToString(),
                                         UserId = drow[19].ToString(),
                                         RevisionNo = Convert.ToInt32(drow[20].ToString()),
                                         QuotYear = drow[21].ToString(),
                                         AgentBrokerCode = drow[22].ToString(),
                                         YearOfManufactureValidationId = drow[23].ToString(),
                                         MakeModelCode = drow[24].ToString()

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
            return quotationMainList;
        }

        [HttpGet]
        [ActionName("GetQuotationByJobId")]
        public QuotationMainTakaful Get(int id)
        {
            QuotationMainTakaful quotationMain = new QuotationMainTakaful();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = " SELECT " +
                               "CASE WHEN MM.QUOTATION_NO        IS NULL THEN ''  ELSE MM.QUOTATION_NO       END, " +
                            "CASE WHEN MM.JOB_ID               IS NULL THEN 0  ELSE MM.JOB_ID              END, " +
                            "CASE WHEN MM.REQUEST_BY         IS NULL THEN ''  ELSE MM.REQUEST_BY        END, " +
                            "CASE WHEN MM.CLIENT_NAME         IS NULL THEN ''  ELSE MM.CLIENT_NAME        END, " +
                            "CASE WHEN MM.VEHICLE_CHASIS_NO   IS NULL THEN ''  ELSE MM.VEHICLE_CHASIS_NO  END, " +
                            "CASE WHEN MM.RISK_TYPE_ID   IS NULL THEN 0  ELSE MM.RISK_TYPE_ID  END, " +
                            "CASE WHEN MM.VEHICLE_TYPE_ID     IS NULL THEN 0  ELSE MM.VEHICLE_TYPE_ID    END, " +
                            "CASE WHEN MM.VEHICLE_CLASS_ID    IS NULL THEN 0  ELSE MM.VEHICLE_CLASS_ID   END, " +
                            "CASE WHEN MM.SUM_INSURED         IS NULL THEN 0  ELSE MM.SUM_INSURED        END, " +
                            "CASE WHEN MM.PERIOD_TYPE_CODE    IS NULL THEN 0  ELSE MM.PERIOD_TYPE_CODE   END, " +
                            "CASE WHEN MM.PERIOD_CODE  IS NULL THEN 0  ELSE MM.PERIOD_CODE END, " +
                            "CASE WHEN MM.AGENT_BROKER       IS NULL THEN ''  ELSE MM.AGENT_BROKER      END, " +
                            "CASE WHEN MM.LEASING_TYPE        IS NULL THEN 0  ELSE MM.LEASING_TYPE       END, " +
                            "CASE WHEN MM.FUEL_TYPE_CODE  IS NULL THEN 0  ELSE MM.FUEL_TYPE_CODE END, " +
                            "CASE WHEN MM.PRODUCT_CODE  IS NULL THEN 0  ELSE MM.PRODUCT_CODE END, " +
                            "CASE WHEN MM.BRANCH_ID           IS NULL THEN ''  ELSE MM.BRANCH_ID          END, " +
                            "CASE WHEN MM.REMARK              IS NULL THEN ''  ELSE MM.REMARK             END, " +
                            "CASE WHEN MM.REQUEST_DATE        IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')  ELSE to_date( MM.REQUEST_DATE,'DD/MM/RRRR')       END, " +
                            "CASE WHEN MM.STATUS              IS NULL THEN 0  ELSE MM.STATUS             END, " +
                            "CASE WHEN MM.USER_ID             IS NULL THEN ''  ELSE MM.USER_ID            END, " +
                            "CASE WHEN MM.REVISION_NO  IS NULL THEN 0  ELSE MM.REVISION_NO END, " +
                            "CASE WHEN MM.QUOT_YEAR           IS NULL THEN ''  ELSE MM.QUOT_YEAR          END, " +
                            "CASE WHEN MM.AGENT_BROKER_CODE   IS NULL THEN ''  ELSE MM.AGENT_BROKER_CODE  END, " +
                              "CASE WHEN MM.YEAR_OF_MANU_VALIDATION           IS NULL THEN ''  ELSE MM.YEAR_OF_MANU_VALIDATION          END, " +
                              "CASE WHEN MM.MAKE_AND_MODEL_CODE   IS NULL THEN 0  ELSE MM.MAKE_AND_MODEL_CODE  END " +
                             " FROM MNBQ_T_MAIN MM  " +
                            " WHERE  MM.JOB_ID=:V_JOB_ID   ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    quotationMain.QuotationNo = dr[0].ToString();
                    quotationMain.JobId = Convert.ToInt32(dr[1].ToString());
                    quotationMain.RequestBy = dr[2].ToString();
                    quotationMain.ClientName = dr[3].ToString();
                    quotationMain.VehicleChasisNo = dr[4].ToString();
                    quotationMain.RiskTypeId = Convert.ToInt32(dr[5].ToString());
                    quotationMain.VehicleTypeId = Convert.ToInt32(dr[6].ToString());
                    quotationMain.VehicleUsageId = Convert.ToInt32(dr[7].ToString());
                    quotationMain.SumInsured = Convert.ToDouble(dr[8].ToString());
                    quotationMain.PeriodTypeCode = Convert.ToInt32(dr[9].ToString());
                    quotationMain.PeriodOfCoverCode = Convert.ToInt32(dr[10].ToString());
                    quotationMain.AgentBroker = dr[11].ToString();
                    quotationMain.LeasingType = Convert.ToInt32(dr[12].ToString());
                    quotationMain.FuelTypeCode = Convert.ToInt32(dr[13].ToString());
                    quotationMain.ProductCode = Convert.ToInt32(dr[14].ToString());
                    quotationMain.BranchId = dr[15].ToString();
                    quotationMain.Remark = dr[16].ToString();
                    quotationMain.RequestDate = dr[17].ToString();
                    quotationMain.Status = dr[18].ToString();
                    quotationMain.UserId = dr[19].ToString();
                    quotationMain.RevisionNo = Convert.ToInt32(dr[20].ToString());
                    quotationMain.QuotYear = dr[21].ToString();
                    quotationMain.AgentBrokerCode = dr[22].ToString();
                    quotationMain.YearOfManufactureValidationId = dr[23].ToString();
                    quotationMain.MakeModelCode = dr[24].ToString();



                    dr.Close();
                    con.Close();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if (dr != null)
                {
                    dr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            finally
            {
                con.Close();

            }

            return quotationMain;
        }



        [ValidateModel]
        [HttpPost]
        public HttpResponseMessage AddQuotationMainDetails(QuotationMainTakaful quotationMain)
        {

            if (CheckVehicleNoAlreadyAvailable(quotationMain.JobId, quotationMain.VehicleChasisNo))
            {
                HttpError err = new HttpError("Vehicle/Chassi Number Already available and cannot duplicated");
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }

            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());




            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {

                con.Open();
                OracleCommand cmd = null;

                cmd = new OracleCommand("INSERT_MNBQ_T_MAIN");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_REQUEST_BY", OracleType.VarChar).Value = quotationMain.RequestBy;
                cmd.Parameters.Add("V_CLIENT_NAME", OracleType.VarChar).Value = quotationMain.ClientName;
                cmd.Parameters.Add("V_VEHICLE_CHASIS_NO", OracleType.VarChar).Value = quotationMain.VehicleChasisNo;

                cmd.Parameters.Add("V_RISK_TYPE_ID", OracleType.Number).Value = quotationMain.RiskTypeId;
                cmd.Parameters.Add("V_VEHICLE_TYPE_ID", OracleType.Number).Value = quotationMain.VehicleTypeId;
                cmd.Parameters.Add("V_VEHICLE_CLASS_ID", OracleType.Number).Value = quotationMain.VehicleUsageId;

                cmd.Parameters.Add("V_SUM_INSURED", OracleType.Double).Value = quotationMain.SumInsured;

                cmd.Parameters.Add("V_PERIOD_TYPE_CODE", OracleType.Number).Value = quotationMain.PeriodTypeCode;
                cmd.Parameters.Add("V_PERIOD_CODE", OracleType.Number).Value = quotationMain.PeriodOfCoverCode;
                cmd.Parameters.Add("V_AGENT_BROKER", OracleType.VarChar).Value = quotationMain.AgentBroker;
                cmd.Parameters.Add("V_AGENT_BROKER_CODE", OracleType.VarChar).Value = quotationMain.AgentBrokerCode;

                cmd.Parameters.Add("V_LEASING_TYPE", OracleType.Number).Value = quotationMain.LeasingType;
                cmd.Parameters.Add("V_FUEL_TYPE_CODE", OracleType.Number).Value = quotationMain.FuelTypeCode;
                cmd.Parameters.Add("V_PRODUCT_CODE", OracleType.Number).Value = quotationMain.ProductCode;

                if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
                {
                    cmd.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = user.BranchCode;

                    cmd.Parameters.Add("V_USER_ID", OracleType.VarChar).Value = user.UserName;
                }
                else
                {
                    cmd.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = quotationMain.BranchId;

                    cmd.Parameters.Add("V_USER_ID", OracleType.VarChar).Value = quotationMain.UserId;
                }
                cmd.Parameters.Add("V_REMARK", OracleType.VarChar).Value = quotationMain.Remark;
                cmd.Parameters.Add("V_STATUS", OracleType.Number).Value = quotationMain.Status;


                cmd.Parameters.Add("V_YEAR_OF_MANU_VALIDATION", OracleType.VarChar).Value = quotationMain.YearOfManufactureValidationId;
                cmd.Parameters.Add("V_MAKE_AND_MODEL_CODE", OracleType.VarChar).Value = quotationMain.MakeModelCode;

                cmd.Parameters.Add("V_NEW_JOB_ID", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters["V_NEW_JOB_ID"].Direction = ParameterDirection.Output;



                cmd.ExecuteNonQuery();
                con.Close();


                string newJobID = "";
                newJobID = Convert.ToString(cmd.Parameters["V_NEW_JOB_ID"].Value);


                if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
                {
                    QuotationCoverTakafulController quotationCoverTakafulController = new QuotationCoverTakafulController();
                    quotationCoverTakafulController.saveDefaultCovers(Convert.ToInt32(newJobID), user);
                }

                return Request.CreateResponse(HttpStatusCode.OK, newJobID);
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateQuotationMainDetails(QuotationMainTakaful quotationMain)
        {

            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());



            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {

                con.Open();
                OracleCommand cmd = null;

                cmd = new OracleCommand("MODIFY_MNBQ_T_MAIN");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_JOB_ID", OracleType.Number).Value = quotationMain.JobId;
                cmd.Parameters.Add("V_REVISION_NO", OracleType.Number).Value = quotationMain.RevisionNo;

                cmd.Parameters.Add("V_REQUEST_BY", OracleType.VarChar).Value = quotationMain.RequestBy;
                cmd.Parameters.Add("V_CLIENT_NAME", OracleType.VarChar).Value = quotationMain.ClientName;
                cmd.Parameters.Add("V_VEHICLE_CHASIS_NO", OracleType.VarChar).Value = quotationMain.VehicleChasisNo;

                cmd.Parameters.Add("V_RISK_TYPE_ID", OracleType.Number).Value = quotationMain.RiskTypeId;
                cmd.Parameters.Add("V_VEHICLE_TYPE_ID", OracleType.Number).Value = quotationMain.VehicleTypeId;
                cmd.Parameters.Add("V_VEHICLE_CLASS_ID", OracleType.Number).Value = quotationMain.VehicleUsageId;

                cmd.Parameters.Add("V_SUM_INSURED", OracleType.Double).Value = quotationMain.SumInsured;

                cmd.Parameters.Add("V_PERIOD_TYPE_CODE", OracleType.Number).Value = quotationMain.PeriodTypeCode;
                cmd.Parameters.Add("V_PERIOD_CODE", OracleType.Number).Value = quotationMain.PeriodOfCoverCode;
                cmd.Parameters.Add("V_AGENT_BROKER", OracleType.VarChar).Value = quotationMain.AgentBroker;
                cmd.Parameters.Add("V_AGENT_BROKER_CODE", OracleType.VarChar).Value = quotationMain.AgentBrokerCode;

                cmd.Parameters.Add("V_LEASING_TYPE", OracleType.Number).Value = quotationMain.LeasingType;
                cmd.Parameters.Add("V_FUEL_TYPE_CODE", OracleType.Number).Value = quotationMain.FuelTypeCode;
                cmd.Parameters.Add("V_PRODUCT_CODE", OracleType.Number).Value = quotationMain.ProductCode;


                if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
                {
                    cmd.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = user.BranchCode;

                    cmd.Parameters.Add("V_USER_ID", OracleType.VarChar).Value = user.UserName;
                }
                else
                {
                    cmd.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = quotationMain.BranchId;

                    cmd.Parameters.Add("V_USER_ID", OracleType.VarChar).Value = quotationMain.UserId;
                }



                cmd.Parameters.Add("V_REMARK", OracleType.VarChar).Value = quotationMain.Remark;
                cmd.Parameters.Add("V_STATUS", OracleType.Number).Value = 1;

                cmd.Parameters.Add("V_YEAR_OF_MANU_VALIDATION", OracleType.VarChar).Value = quotationMain.YearOfManufactureValidationId;
                cmd.Parameters.Add("V_MAKE_AND_MODEL_CODE", OracleType.VarChar).Value = quotationMain.MakeModelCode;





                cmd.ExecuteNonQuery();
                con.Close();


                if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
                {
                    QuotationCoverTakafulController quotationCoverTakafulController = new QuotationCoverTakafulController();


                    quotationCoverTakafulController.DeleteCovers(Convert.ToInt32(quotationMain.JobId), 0);
                    quotationCoverTakafulController.saveDefaultCovers(Convert.ToInt32(quotationMain.JobId), user);
                }


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                con.Close();

                return null;
            }
        }

        public void CalculatePremium(int jobId, int revisionId, int productCode)
        {
            try
            {
                OracleConnection con = new OracleConnection(ConnectionString);
                con.Open();


                int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;

                OracleCommand cmd = null;



                if (productCode == MNBQTakafulProductCode)
                {
                    cmd = new OracleCommand("MNBQ_CALC_TAKAFUL");
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_JOB_ID", OracleType.Int32, 15).Value = jobId;

                cmd.Parameters.Add("V_REVISION_NO", OracleType.Int32, 15).Value = revisionId;

                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {

            }
        }


        [HttpPost]
        public string CalculateAndGetPremium(QuotationCalculate quotationCalculate)
        {
            double premium = 0.0;
            try
            {
                OracleConnection con = new OracleConnection(ConnectionString);
                con.Open();


                int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;


                OracleCommand cmd = null;



                if (quotationCalculate.ProductCode == MNBQTakafulProductCode)
                {
                    cmd = new OracleCommand("MNBQ_CALC_TAKAFUL");
                }




                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_JOB_ID", OracleType.Int32, 15).Value = quotationCalculate.JobId;

                cmd.Parameters.Add("V_REVISION_NO", OracleType.Int32, 15).Value = quotationCalculate.RevisionId;

                cmd.ExecuteNonQuery();
                con.Close();

                premium = getCalculatedTotalPremium(quotationCalculate.JobId.ToString(), "0");


                if (!validateThreeWheelMinPremium(quotationCalculate.RiskTypeId, premium))
                {
                    return "Three Wheeler Minimum Premium not achieved";
                }


            }
            catch (Exception ex)
            {

            }
            return premium.ToString();
        }
        [HttpGet]
        [ActionName("GetQuotationNumberByJobId")]
        public string GetQuotationNumberByJobId(int id)
        {
            string quotationNo = "";
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "select " +
                             "mm.QUOTATION_NO  " +
                             " from mnbq_t_main mm " +
                             " WHERE  mm.JOB_ID=:V_JOB_ID   ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    quotationNo = dr[0].ToString();


                    dr.Close();
                    con.Close();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if (dr != null)
                {
                    dr.Close();
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            finally
            {
                con.Close();

            }

            return quotationNo;
        }




        [HttpGet]
        [ActionName("GetCalculatedTotalPremium")]
        public double getCalculatedTotalPremium(string jobId, string revisionNo)
        {
            double premium = 0.0;

            try
            {
                OracleConnection con = new OracleConnection(ConnectionString);
                OracleDataReader dr;
                con.Open();
                String sql = "";
                sql = "  SELECT T.AMOUNT " +
                                 " FROM MNB_T_QUOT_TEMP_RESULT T  " +
                                  " WHERE JOB_ID=:V_JOB_ID AND  REVISION_NO=:V_REVISION_NO AND POLICY_COVER_CODE='PREMIUM'";



                OracleCommand cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(new OracleParameter("V_JOB_ID", jobId));
                cmd.Parameters.Add(new OracleParameter("V_REVISION_NO", revisionNo));



                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();


                    premium = Convert.ToDouble(dr[0].ToString());


                }

                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return premium;
        }




        private bool CheckVehicleNoAlreadyAvailable(int jobId, string vehicleNo)
        {

            bool returnVal = false;
            try
            {

                OracleConnection con = new OracleConnection(ConnectionString);
                OracleDataReader dr;
                con.Open();
                String sql = "";

                if (jobId > 0)
                {
                    sql = "SELECT VEHICLE_CHASIS_NO  FROM MNBQ_T_MAIN T " +
                                     " WHERE  T.VEHICLE_CHASIS_NO=:V_VEHICLE_CHASIS_NO AND T.JOB_ID<>:V_JOB_ID";
                }
                else
                {
                    sql = "SELECT VEHICLE_CHASIS_NO  FROM MNBQ_T_MAIN T " +
                              " WHERE  T.VEHICLE_CHASIS_NO=:V_VEHICLE_CHASIS_NO";

                }

                OracleCommand cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(new OracleParameter("V_VEHICLE_CHASIS_NO", vehicleNo));

                if (jobId > 0)
                {
                    cmd.Parameters.Add(new OracleParameter("V_JOB_ID", jobId));
                }



                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    returnVal = true;
                }

                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return returnVal;
        }



        [HttpGet]
        public Object GetQuotationResultByJobId(int id)
        {


            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());


            QuotationResult quotationResult = new QuotationResult();
            QuotationResultForExt quotationResultForExt = new QuotationResultForExt();


            QuotationMainTakaful quotationMain = new QuotationMainTakaful();
            quotationMain = Get(id);

            UserAccount quotationGeneratedUser = uc.getUserFromUserName(quotationMain.UserId);


            if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
            {
                if (user.Company != quotationGeneratedUser.Company)
                {
                    return null;
                }
            }

            Object obj;

            CalculatePremium(quotationMain.JobId, quotationMain.RevisionNo, quotationMain.ProductCode);


            if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
            {
                if (quotationMain.QuotationNo == "")
                {
                    UpdateQuotationNumber(quotationMain.JobId, quotationMain.RevisionNo, quotationMain.BranchId, quotationMain.ProductCode, quotationMain.SumInsured, quotationMain.UserId);
                }


                quotationResultForExt = GetQuotationResultForExtByJobId(id);
                obj = quotationResultForExt;
            }
            else
            {
                quotationResult = GetQuotationResultForInternalByJobId(id);
                obj = quotationResult;
            }


            return obj;

        }



        public QuotationResult GetQuotationResultForInternalByJobId(int id)
        {
            QuotationResult quotationResult = new QuotationResult();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "select " +
                             "t.JOB_ID," +//0
                             "t.REVISION_NO," +//1
                             "t.SUM_INSURED," +//2
                             "BASIC_PREMIUM," +//3
                             "CRSF," +//4
                             "CRSF_WITH_VAT," +//5
                             "MULTIPLE_REBATE," +//6
                             "MUL_REBATE_VAL," +//7
                             "NCB_TYPE," +//8
                             "NCB_PERC," +//9
                             "DEPRECIATION_CVR," +//10
                             "VAP," +//11
                             "SUB_TOTAL_A," +//12
                             "SUB_TOTAL_B," +//13
                             "SUB_TOTAL_C," +//14
                             "SUB_TOTAL_D," +//15
                             "SUB_TOTAL_E," +//16
                             "SUB_TOTAL_BEFORE_VAP," +//17
                             "SUB_TOTAL_MAN_TOT_PREM," +//18
                             "SUB_TOTAL_MAN_NET_PREM," +//19
                             "SUB_TOTAL_F," +//20
                             "SUB_TOTAL_F_WITH_VAT," +//21
                             "SRCC_FOR_VEHICLE," +//22
                             "SRCC_FOR_GOODS," +//23
                             "SRCC_FOR_PAB," +//24
                             "SRCC_FOR_WCI," +//25
                             "TOTAL_SRCC," +//26
                             "TOTAL_SRCC_WITH_VAT," +//27
                             "TC_FOR_VEHICLE," +//28
                             "TC_FOR_GOODS," +//29
                             "TC_FOR_PAB," +//30
                             "TC_FOR_WCI," +//31
                             "TOTAL_TC," +//32
                             "TOTAL_TC_WITH_VAT," +//33
                             "GROSS_PREMIUM," +//34
                             "ADMIN_FEE," +//35
                             "ADMIN_FEE_WITH_VAT," +//36
                             "POLICY_FEE," +//37
                             "POLICY_FEE_WITH_VAT," +//38
                             "TOTAL_FOR_STAMP," +//39
                             "STAMP_DUTY," +//40
                             "STAMP_DUTY_WITH_VAT," +//41
                             "NBT," +//42
                             "NBT_WITH_VAT," +//43
                             "TAXES," +//44
                             "PREMIUM," +//45
                             "PREMIUM_WITH_VAT," +//46
                             "multiple_rebate as \"Multiple rebate\"," +//47
                             "chk1 as \"Hire Purchase\"," +//48
                             "chk2 as \"Voluntary Excess\"," +//49
                             "chk3 as \"AAC Membership\"," +//50
                             "chk4 as \"PAB for driver\"," +//51
                             "chk5 as \"PAB for Passenger\"," +//52
                             "chk6 as \"Good in Transit\"," +//53
                             "chk7 as \"Legal Liability\"," +//54
                             "chk8 as \"Towing Charge\"," +//55
                             "chk9 as \"NCB\"," +//56
                             "chk10 as \"Up Front NCB\"," +//57
                             "chk11 as \"Windscreen\"," +//58
                             "chk12 as \"TPPD\"," +//59
                             "chk13 as \"WCI\"," +//60
                             "chk14 as \"Inclusion of Excluded Items\"," +//61
                             "chk15 as \"Learner Rider/Driver\"," +//62
                             "chk16 as \"CTB\"," +//63
                             "chk17 as \"Rent A Car\"," +//64
                             "chk18 as \"Flood\"," +//65
                             "chk22 as \"Driving Tuition\"," +//66
                             "chk23 as \"Duty Free/Duty Concession\"," +//67
                             "chk24 as \"Adjustment Fee\"," +//68
                             "chk25 as \"Theft of Parts\"," +//69
                             "chk26 as \"SRCC - for Vehicles\"," +//70
                             "chk27 as \"SRCC - for Goods\"," +//71
                             "chk28 as \"SRCC - for PAB\"," +//72
                             "chk29 as \"TC - for PAB\"," +//73
                             "chk30 as \"SRCC - for WCI\"," +//74
                             "chk33 as \"TC - for Vehicles\"," +//75
                             "chk34 as \"TC - for Goods\"," +//76
                             "chk31 as \"TC - for WCI\"," +//77
                             "chk35 as \"Air Bag Replacement\", " +//78
                             "mm.QUOTATION_NO as \"Quotation No\" " +//79
                             " from mnb_t_quot_get_results t " +
                             " inner join mnbq_t_main mm on mm.JOB_ID=t.JOB_ID " +
                             " WHERE  t.JOB_ID=:V_JOB_ID   ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    quotationResult.JOB_ID = Convert.ToInt32(dr[0]);
                    quotationResult.REVISION_NO = Convert.ToInt32(dr[1]);
                    quotationResult.QUOTATION_NO = dr[79].ToString();
                    quotationResult.SUM_INSURED = Convert.ToDouble(dr[2]);
                    quotationResult.BASIC_PREMIUM = Convert.ToDouble(dr[3]);
                    quotationResult.CRSF = Convert.ToDouble(dr[4]);
                    quotationResult.CRSF_WITH_VAT = Convert.ToDouble(dr[5]);
                    quotationResult.MULTIPLE_REBATE = Convert.ToDouble(dr[6]);
                    quotationResult.MUL_REBATE_VAL = Convert.ToDouble(dr[7]);
                    quotationResult.NCB_TYPE = Convert.ToDouble(dr[8]);
                    quotationResult.NCB_PERC = Convert.ToDouble(dr[9]);
                    quotationResult.DEPRECIATION_CVR = Convert.ToDouble(dr[10]);
                    quotationResult.VAP = Convert.ToDouble(dr[11]);
                    quotationResult.SUB_TOTAL_A = Convert.ToDouble(dr[12]);
                    quotationResult.SUB_TOTAL_B = Convert.ToDouble(dr[13]);
                    quotationResult.SUB_TOTAL_C = Convert.ToDouble(dr[14]);
                    quotationResult.SUB_TOTAL_D = Convert.ToDouble(dr[15]);
                    quotationResult.SUB_TOTAL_E = Convert.ToDouble(dr[16]);
                    quotationResult.SUB_TOTAL_BEFORE_VAP = Convert.ToDouble(dr[17]);
                    quotationResult.SUB_TOTAL_MAN_TOT_PREM = Convert.ToDouble(dr[18]);
                    quotationResult.SUB_TOTAL_MAN_NET_PREM = Convert.ToDouble(dr[19]);
                    quotationResult.SUB_TOTAL_F = Convert.ToDouble(dr[20]);
                    quotationResult.SUB_TOTAL_F_WITH_VAT = Convert.ToDouble(dr[21]);
                    quotationResult.SRCC_FOR_VEHICLE = Convert.ToDouble(dr[22]);
                    quotationResult.SRCC_FOR_GOODS = Convert.ToDouble(dr[23]);
                    quotationResult.SRCC_FOR_PAB = Convert.ToDouble(dr[24]);
                    quotationResult.SRCC_FOR_WCI = Convert.ToDouble(dr[25]);
                    quotationResult.TOTAL_SRCC = Convert.ToDouble(dr[26]);
                    quotationResult.TOTAL_SRCC_WITH_VAT = Convert.ToDouble(dr[27]);
                    quotationResult.TC_FOR_VEHICLE = Convert.ToDouble(dr[28]);
                    quotationResult.TC_FOR_GOODS = Convert.ToDouble(dr[29]);
                    quotationResult.TC_FOR_PAB = Convert.ToDouble(dr[30]);
                    quotationResult.TC_FOR_WCI = Convert.ToDouble(dr[31]);
                    quotationResult.TOTAL_TC = Convert.ToDouble(dr[32]);
                    quotationResult.TOTAL_TC_WITH_VAT = Convert.ToDouble(dr[33]);
                    quotationResult.GROSS_PREMIUM = Convert.ToDouble(dr[34]);
                    quotationResult.ADMIN_FEE = Convert.ToDouble(dr[35]);
                    quotationResult.ADMIN_FEE_WITH_VAT = Convert.ToDouble(dr[36]);
                    quotationResult.POLICY_FEE = Convert.ToDouble(dr[37]);
                    quotationResult.POLICY_FEE_WITH_VAT = Convert.ToDouble(dr[38]);
                    quotationResult.TOTAL_FOR_STAMP = Convert.ToDouble(dr[39]);
                    quotationResult.STAMP_DUTY = Convert.ToDouble(dr[40]);
                    quotationResult.STAMP_DUTY_WITH_VAT = Convert.ToDouble(dr[41]);
                    quotationResult.NBT = Convert.ToDouble(dr[42]);
                    quotationResult.NBT_WITH_VAT = Convert.ToDouble(dr[43]);
                    quotationResult.TAXES = Convert.ToDouble(dr[44]);
                    quotationResult.PREMIUM = Convert.ToDouble(dr[45]);
                    quotationResult.PREMIUM_WITH_VAT = Convert.ToDouble(dr[46]);
                    quotationResult.multiple_rebate = Convert.ToDouble(dr[47]);
                    quotationResult.hire_purchase = Convert.ToDouble(dr[48]);
                    quotationResult.voluntary_excess = Convert.ToDouble(dr[49]);
                    quotationResult.aac_membership = Convert.ToDouble(dr[50]);
                    quotationResult.pab_for_driver = Convert.ToDouble(dr[51]);
                    quotationResult.pab_for_passenger = Convert.ToDouble(dr[52]);
                    quotationResult.good_in_transit = Convert.ToDouble(dr[53]);
                    quotationResult.legal_liability = Convert.ToDouble(dr[54]);
                    quotationResult.towing_charge = Convert.ToDouble(dr[55]);
                    quotationResult.ncb = Convert.ToDouble(dr[56]);
                    quotationResult.up_front_ncb = Convert.ToDouble(dr[57]);
                    quotationResult.windscreen = Convert.ToDouble(dr[58]);
                    quotationResult.tppd = Convert.ToDouble(dr[59]);
                    quotationResult.wci = Convert.ToDouble(dr[60]);
                    quotationResult.inclusion_of_excluded_items = Convert.ToDouble(dr[61]);
                    quotationResult.learner_rider_driver = Convert.ToDouble(dr[62]);
                    quotationResult.ctb = Convert.ToDouble(dr[63]);
                    quotationResult.rent_a_car = Convert.ToDouble(dr[64]);
                    quotationResult.flood = Convert.ToDouble(dr[65]);
                    quotationResult.driving_tuition = Convert.ToDouble(dr[66]);
                    quotationResult.duty_free_duty_concession = Convert.ToDouble(dr[67]);
                    quotationResult.adjustment_fee = Convert.ToDouble(dr[68]);
                    quotationResult.theft_of_parts = Convert.ToDouble(dr[69]);
                    quotationResult.srcc_for_vehicles = Convert.ToDouble(dr[70]);
                    quotationResult.srcc_for_goods = Convert.ToDouble(dr[71]);
                    quotationResult.srcc_for_pab = Convert.ToDouble(dr[72]);
                    quotationResult.tc_for_pab = Convert.ToDouble(dr[73]);
                    quotationResult.srcc_for_wci = Convert.ToDouble(dr[74]);
                    quotationResult.tc_for_vehicles = Convert.ToDouble(dr[75]);
                    quotationResult.tc_for_goods = Convert.ToDouble(dr[76]);
                    quotationResult.tc_for_wci = Convert.ToDouble(dr[77]);
                    quotationResult.air_bag_replacement = Convert.ToDouble(dr[78]);

                    dr.Close();
                    con.Close();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }
            }
            finally
            {
                con.Close();

            }

            return quotationResult;
        }


        public QuotationResultForExt GetQuotationResultForExtByJobId(int id)
        {
            QuotationResultForExt quotationResult = new QuotationResultForExt();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "select " +
                             "t.JOB_ID," +//0
                             "t.REVISION_NO," +//1
                             "t.SUM_INSURED," +//2
                             "BASIC_PREMIUM," +//3
                             "CRSF," +//4
                             "CRSF_WITH_VAT," +//5
                             "MULTIPLE_REBATE," +//6
                             "MUL_REBATE_VAL," +//7
                             "NCB_TYPE," +//8
                             "NCB_PERC," +//9
                             "DEPRECIATION_CVR," +//10
                             "VAP," +//11
                             "SUB_TOTAL_A," +//12
                             "SUB_TOTAL_B," +//13
                             "SUB_TOTAL_C," +//14
                             "SUB_TOTAL_D," +//15
                             "SUB_TOTAL_E," +//16
                             "SUB_TOTAL_BEFORE_VAP," +//17
                             "SUB_TOTAL_MAN_TOT_PREM," +//18
                             "SUB_TOTAL_MAN_NET_PREM," +//19
                             "SUB_TOTAL_F," +//20
                             "SUB_TOTAL_F_WITH_VAT," +//21
                             "SRCC_FOR_VEHICLE," +//22
                             "SRCC_FOR_GOODS," +//23
                             "SRCC_FOR_PAB," +//24
                             "SRCC_FOR_WCI," +//25
                             "TOTAL_SRCC," +//26
                             "TOTAL_SRCC_WITH_VAT," +//27
                             "TC_FOR_VEHICLE," +//28
                             "TC_FOR_GOODS," +//29
                             "TC_FOR_PAB," +//30
                             "TC_FOR_WCI," +//31
                             "TOTAL_TC," +//32
                             "TOTAL_TC_WITH_VAT," +//33
                             "GROSS_PREMIUM," +//34
                             "ADMIN_FEE," +//35
                             "ADMIN_FEE_WITH_VAT," +//36
                             "POLICY_FEE," +//37
                             "POLICY_FEE_WITH_VAT," +//38
                             "TOTAL_FOR_STAMP," +//39
                             "STAMP_DUTY," +//40
                             "STAMP_DUTY_WITH_VAT," +//41
                             "NBT," +//42
                             "NBT_WITH_VAT," +//43
                             "TAXES," +//44
                             "PREMIUM," +//45
                             "PREMIUM_WITH_VAT," +//46
                             "multiple_rebate as \"Multiple rebate\"," +//47
                             "chk1 as \"Hire Purchase\"," +//48
                             "chk2 as \"Voluntary Excess\"," +//49
                             "chk3 as \"AAC Membership\"," +//50
                             "chk4 as \"PAB for driver\"," +//51
                             "chk5 as \"PAB for Passenger\"," +//52
                             "chk6 as \"Good in Transit\"," +//53
                             "chk7 as \"Legal Liability\"," +//54
                             "chk8 as \"Towing Charge\"," +//55
                             "chk9 as \"NCB\"," +//56
                             "chk10 as \"Up Front NCB\"," +//57
                             "chk11 as \"Windscreen\"," +//58
                             "chk12 as \"TPPD\"," +//59
                             "chk13 as \"WCI\"," +//60
                             "chk14 as \"Inclusion of Excluded Items\"," +//61
                             "chk15 as \"Learner Rider/Driver\"," +//62
                             "chk16 as \"CTB\"," +//63
                             "chk17 as \"Rent A Car\"," +//64
                             "chk18 as \"Flood\"," +//65
                             "chk22 as \"Driving Tuition\"," +//66
                             "chk23 as \"Duty Free/Duty Concession\"," +//67
                             "chk24 as \"Adjustment Fee\"," +//68
                             "chk25 as \"Theft of Parts\"," +//69
                             "chk26 as \"SRCC - for Vehicles\"," +//70
                             "chk27 as \"SRCC - for Goods\"," +//71
                             "chk28 as \"SRCC - for PAB\"," +//72
                             "chk29 as \"TC - for PAB\"," +//73
                             "chk30 as \"SRCC - for WCI\"," +//74
                             "chk33 as \"TC - for Vehicles\"," +//75
                             "chk34 as \"TC - for Goods\"," +//76
                             "chk31 as \"TC - for WCI\"," +//77
                             "chk35 as \"Air Bag Replacement\", " +//78
                             "mm.QUOTATION_NO as \"Quotation No\" " +//79
                             " from mnb_t_quot_get_results t " +
                             " inner join mnbq_t_main mm on mm.JOB_ID=t.JOB_ID " +
                             " WHERE  t.JOB_ID=:V_JOB_ID   ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    quotationResult.JOB_ID = Convert.ToInt32(dr[0]);
                    quotationResult.REVISION_NO = Convert.ToInt32(dr[1]);
                    quotationResult.QUOTATION_NO = dr[79].ToString();
                    quotationResult.SUM_INSURED = Convert.ToDouble(dr[2]);
                    quotationResult.BASIC_PREMIUM = Convert.ToDouble(dr[3]);
                    quotationResult.TOTAL_SRCC = Convert.ToDouble(dr[26]);
                    quotationResult.TOTAL_TC = Convert.ToDouble(dr[32]);
                    quotationResult.STAMP_DUTY = Convert.ToDouble(dr[40]);
                    quotationResult.TAXES = Convert.ToDouble(dr[44]);
                    quotationResult.PREMIUM_WITH_VAT = Convert.ToDouble(dr[46]);


                    dr.Close();
                    con.Close();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }
            }
            finally
            {
                con.Close();

            }

            return quotationResult;
        }

        [HttpPost]
        [ActionName("UpdateAndGetQuotationNumber")]
        public string UpdateAndGetQuotationNumber(int jobId)
        {

            string quotationNumber = "";

            if (jobId > 0)
            {
                QuotationMainTakaful quotationMain = new QuotationMainTakaful();
                quotationMain = Get(Convert.ToInt32(jobId));


                if (quotationMain.QuotationNo == "")
                {
                    UpdateQuotationNumber(quotationMain.JobId, quotationMain.RevisionNo, quotationMain.BranchId, quotationMain.ProductCode, quotationMain.SumInsured, quotationMain.UserId);

                }

                quotationNumber = GetQuotationNumberByJobId(jobId);
            }
            return quotationNumber;




        }


        private void UpdateQuotationNumber(int jobId, int revisionNo, string branchId, int productCode, double sumInsured, string userId)
        {
            OracleConnection con = new OracleConnection(ConnectionString);

            try
            {
                con.Open();
                OracleCommand cmd = null;

                cmd = new OracleCommand("UPDATE_MNBQ_T_QUOT_NUMBER");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_JOB_ID", OracleType.Number).Value = jobId;
                cmd.Parameters.Add("V_REVISION_NO", OracleType.Number).Value = revisionNo;
                cmd.Parameters.Add("V_BRANCH_ID", OracleType.VarChar).Value = branchId;
                cmd.Parameters.Add("V_PRODUCT_CODE", OracleType.Number).Value = productCode;
                cmd.Parameters.Add("V_SUM_INSURED", OracleType.Number).Value = sumInsured;
                cmd.Parameters.Add("V_USER_ID", OracleType.VarChar).Value = userId;



                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            finally
            {
                con.Close();

            }

        }




        private bool validateThreeWheelMinPremium(int VehicleRiskType, double TotalPremium)
        {
            bool returnVal = false;


            int MNBQThreeWheelerRiskType = Properties.Settings.Default.MNBQThreeWheelerRiskType;

            if (VehicleRiskType == MNBQThreeWheelerRiskType)
            {

                double dMNBQThreeWheelerMinimumPremium = Properties.Settings.Default.MNBQThreeWheelerMinimumPremium;


                if (TotalPremium >= dMNBQThreeWheelerMinimumPremium)
                {
                    returnVal = true;
                }

            }
            else
            {
                returnVal = true;
            }

            return returnVal;
        }


        [HttpPost]
        [ActionName("SearchQuotations")]
        public IEnumerable<QuotationSearchRequest> SearchQuotations(QuotationSearchRequest quotationSearchRequest)
        {
            List<QuotationSearchRequest> quotationSearchResult = new List<QuotationSearchRequest>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);



            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user = uc.getUserFromUserName(uIdentity.Name.ToString());

            string sql = "";
            string sqlWhereStatement = "";

            OracleCommand cmd = null;



            if (quotationSearchRequest.VehicleChasisNo != null && quotationSearchRequest.VehicleChasisNo != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(LOWER(MM.VEHICLE_CHASIS_NO) LIKE '%" + quotationSearchRequest.VehicleChasisNo.ToLower() + "%') AND";
            }
            if (quotationSearchRequest.RequestBy != null && quotationSearchRequest.RequestBy != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(LOWER(MM.REQUEST_BY) LIKE '%" + quotationSearchRequest.RequestBy.ToLower() + "%') AND";
            }
            if (quotationSearchRequest.RequestDate != null && quotationSearchRequest.RequestDate != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(TO_DATE(MM.REQUEST_DATE,'dd/mm/RRRR')  = TO_DATE('" + quotationSearchRequest.RequestDate + "','dd/mm/RRRR')) AND";
            }

            if (quotationSearchRequest.AgentBrokerCode != null && quotationSearchRequest.AgentBrokerCode != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(LOWER(MM.AGENT_BROKER_CODE) LIKE '%" + quotationSearchRequest.AgentBrokerCode.ToLower() + "%') AND";
            }

            if (quotationSearchRequest.JobId != null && quotationSearchRequest.JobId != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(LOWER(MM.JOB_ID) = " + quotationSearchRequest.JobId.ToLower() + ") AND";
            }

            if (quotationSearchRequest.QuotationNo != null && quotationSearchRequest.QuotationNo != "")
            {

                sqlWhereStatement = sqlWhereStatement + "(LOWER(MM.QUOTATION_NO) LIKE '%" + quotationSearchRequest.QuotationNo.ToLower() + "%') AND";
            }
            if (quotationSearchRequest.Status != null && quotationSearchRequest.Status != "")
            {
                sqlWhereStatement = sqlWhereStatement + "(MM.STATUS = " + quotationSearchRequest.Status + ") AND";
            }


            if (sqlWhereStatement.Length > 0)
            {
                sqlWhereStatement = sqlWhereStatement.Substring(0, sqlWhereStatement.Length - 3);
            }


            if (user.Company != Properties.Settings.Default.MNBQGeneralCompanyName)
            {

                sql = " SELECT " +
                                       "CASE WHEN MM.QUOTATION_NO        IS NULL THEN ''  ELSE MM.QUOTATION_NO       END, " +//0
                                       "CASE WHEN MM.JOB_ID               IS NULL THEN 0  ELSE MM.JOB_ID              END, " +//1
                                       "CASE WHEN MM.REQUEST_BY         IS NULL THEN ''  ELSE MM.REQUEST_BY        END, " +//2
                                       "CASE WHEN MM.CLIENT_NAME         IS NULL THEN ''  ELSE MM.CLIENT_NAME        END, " +//3
                                       "CASE WHEN MM.VEHICLE_CHASIS_NO   IS NULL THEN ''  ELSE MM.VEHICLE_CHASIS_NO  END, " +//4
                                       "CASE WHEN MM.REQUEST_DATE        IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')  ELSE to_date( MM.REQUEST_DATE,'DD/MM/RRRR')       END, " +//5
                                       "CASE WHEN MS.STATUS_NAME              IS NULL THEN ''  ELSE MS.STATUS_NAME            END, " +//6
                                       "CASE WHEN MM.AGENT_BROKER_CODE   IS NULL THEN ''  ELSE MM.AGENT_BROKER_CODE  END " +//7
                                        " FROM MNBQ_T_MAIN MM  " +
                                         " INNER JOIN MNBQ_STATUSES MS ON MM.STATUS=MS.STATUS_CODE " +
                                        " WHERE MM.USER_ID =:V_USER_ID  AND (" + sqlWhereStatement + ")";

                cmd = new OracleCommand(sql, con);

                cmd.Parameters.Add(new OracleParameter("V_USER_ID", user.UserName));
            }
            else
            {
                sql = " SELECT " +
                                      "CASE WHEN MM.QUOTATION_NO        IS NULL THEN ''  ELSE MM.QUOTATION_NO       END, " +//0
                                      "CASE WHEN MM.JOB_ID               IS NULL THEN 0  ELSE MM.JOB_ID              END, " +//1
                                      "CASE WHEN MM.REQUEST_BY         IS NULL THEN ''  ELSE MM.REQUEST_BY        END, " +//2
                                      "CASE WHEN MM.CLIENT_NAME         IS NULL THEN ''  ELSE MM.CLIENT_NAME        END, " +//3
                                      "CASE WHEN MM.VEHICLE_CHASIS_NO   IS NULL THEN ''  ELSE MM.VEHICLE_CHASIS_NO  END, " +//4
                                      "CASE WHEN MM.REQUEST_DATE        IS NULL THEN to_date('01/01/1900','DD/MM/RRRR')  ELSE to_date( MM.REQUEST_DATE,'DD/MM/RRRR')       END, " +//5
                                      "CASE WHEN MS.STATUS_NAME              IS NULL THEN ''  ELSE MS.STATUS_NAME             END, " +//6
                                      "CASE WHEN MM.AGENT_BROKER_CODE   IS NULL THEN ''  ELSE MM.AGENT_BROKER_CODE  END " +//7
                                       " FROM MNBQ_T_MAIN MM   " +
                                         " INNER JOIN MNBQ_STATUSES MS ON MM.STATUS=MS.STATUS_CODE " +
                                       "WHERE (" + sqlWhereStatement + ")";

                cmd = new OracleCommand(sql, con);
            }



            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                quotationSearchResult = (from DataRow drow in dt.Rows
                                         select new QuotationSearchRequest()
                                         {
                                             QuotationNo = drow[0].ToString(),
                                             JobId = drow[1].ToString(),
                                             RequestBy = drow[2].ToString(),
                                             ClientName = drow[3].ToString(),
                                             VehicleChasisNo = drow[4].ToString(),
                                             RequestDate = drow[5].ToString().Remove(10),
                                             Status = drow[6].ToString(),
                                             AgentBrokerCode = drow[7].ToString()

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
            return quotationSearchResult;
        }



        [HttpGet]
        [ActionName("GetCalculation")]
        [TDABasicAuthenticationFilter(false)]
        [AllowAnonymous]
        public HttpResponseMessage GetCalculation(string jobId, int productCode)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (jobId == null || jobId == "0")
            {
                HttpError err = new HttpError("Job Number or Revision Number not availabe");
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }

            int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;


            try
            {
                TableLogOnInfo crTableLogOnInfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();

                ReportDocument crystalReport = new ReportDocument();

                if (productCode == MNBQTakafulProductCode)
                {
                    crystalReport.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/MotorTakafulCalculation.rpt"));
                }




                crystalReport.SetDatabaseLogon("hnba_crc", "HNBACRC", "RACPROD", "");

                crystalReport.SetParameterValue("P_JOB_ID", jobId);
                crystalReport.SetParameterValue("P_REVISION_NO", "0");

                Stream oStream = null;
                byte[] byteArray = null;
                oStream = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));


                response.Content = new ByteArrayContent(byteArray);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        [HttpGet]
        [ActionName("GetQuotation")]

        [TDABasicAuthenticationFilter(false)]
        [AllowAnonymous]
        public HttpResponseMessage GetQuotation(string jobId, int productCode)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (jobId == null || jobId == "0")
            {
                HttpError err = new HttpError("Job Number or Revision Number not availabe");
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }


            int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;



            try
            {



                TableLogOnInfo crTableLogOnInfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();

                ReportDocument crystalReport = new ReportDocument();

                if (productCode == MNBQTakafulProductCode)
                {
                    crystalReport.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/MotorTakafulQuotation.rpt"));
                }




                crystalReport.SetDatabaseLogon("hnba_crc", "HNBACRC", "RACPROD", "");

                crystalReport.SetParameterValue("P_JOB_ID", jobId);
                crystalReport.SetParameterValue("P_REVISION_NO", "0");

                Stream oStream = null;
                byte[] byteArray = null;
                oStream = crystalReport.ExportToStream(ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));


                response.Content = new ByteArrayContent(byteArray);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }







    }
}
