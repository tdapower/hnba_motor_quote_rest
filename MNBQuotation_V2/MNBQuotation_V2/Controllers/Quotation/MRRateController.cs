using MNBQuotation_V2.Models.Quotation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{
    public class MRRateController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();


        //[HttpPost]
        //[ActionName("ValidateMRRate")]
        //public HttpResponseMessage ValidateMRRate(MRRate mRRate)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);


        //    string branchType = "";
        //    string relevantLimitRange = "";

        //    double maximumAllowedRate = 0.00;
        //    double sumInsured = 0.00;

        //    OracleConnection con = new OracleConnection(ConnectionString);
        //    OracleDataReader dr;
        //    con.Open();
        //    String sql = "";


        //    if (mRRate.BranchCode != null && mRRate.BranchCode != "")
        //    {

        //        branchType = getBranchType(mRRate.BranchCode);

        //    }

        //    if (branchType == "")
        //    {
        //        branchType = mRRate.BranchCode;

        //    }


        //    string AgentTypeMNBQHNB = Properties.Settings.Default.MNBQHNB;



        //    sumInsured = mRRate.SumInsured;

        //    relevantLimitRange = getRelevantLimitRange(sumInsured);


        //    if (relevantLimitRange == "")
        //    {
        //        relevantLimitRange = "RATE_FOR_LIMIT_5";
        //    }

        //    if (mRRate.AgentType == AgentTypeMNBQHNB)
        //    {
        //        sql = "SELECT " + relevantLimitRange + " FROM MNBQ_MR_LIMIT_GUIDE  " +
        //             " WHERE BRANCH_TYPE=:V_BRANCH_TYPE " +
        //           " AND RISK_TYPE_ID=:V_RISK_TYPE_ID " +
        //           " AND USAGE_ID=:V_USAGE_ID " +
        //            " AND FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE " +
        //               " AND AGENT_TYPE_CODE=4" +
        //             " AND MAKE_MODEL=:V_MAKE_MODEL" +
        //            " AND YEAR_OF_MANUFACTURE=:V_YEAR_OF_MANU " +
        //               " AND TO_DATE(SYSDATE,'DD/MM/RRRR') >=  TO_DATE(START_DATE,'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') <=TO_DATE(END_DATE,'DD/MM/RRRR') ";
        //    }
        //    else
        //    {
        //        sql = "SELECT " + relevantLimitRange + " FROM MNBQ_MR_LIMIT_GUIDE  " +
        //           " WHERE BRANCH_TYPE=:V_BRANCH_TYPE " +
        //           " AND RISK_TYPE_ID=:V_RISK_TYPE_ID " +
        //           " AND USAGE_ID=:V_USAGE_ID " +
        //            " AND FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE " +
        //           " AND AGENT_TYPE_CODE=0" +
        //             " AND MAKE_MODEL=:V_MAKE_MODEL" +
        //            " AND YEAR_OF_MANUFACTURE=:V_YEAR_OF_MANU " +
        //           " AND TO_DATE(SYSDATE,'DD/MM/RRRR') >=  TO_DATE(START_DATE,'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') <=TO_DATE(END_DATE,'DD/MM/RRRR') ";


        //    }




        //    OracleCommand cmd = new OracleCommand(sql, con);

        //    cmd.Parameters.Add(new OracleParameter("V_BRANCH_TYPE", branchType));
        //    cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", mRRate.RiskTypeId));
        //    cmd.Parameters.Add(new OracleParameter("V_USAGE_ID", mRRate.VehicleClassId));
        //    cmd.Parameters.Add(new OracleParameter("V_FUEL_TYPE_CODE", mRRate.FuelTypeCode));
        //    cmd.Parameters.Add(new OracleParameter("V_MAKE_MODEL", mRRate.MakeModelCode));
        //    cmd.Parameters.Add(new OracleParameter("V_YEAR_OF_MANU", mRRate.YearOfManufactureValidationId));



        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        maximumAllowedRate = Convert.ToDouble(dr[0].ToString());
        //    }

        //    dr.Close();
        //    dr.Dispose();
        //    cmd.Dispose();
        //    con.Close();
        //    con.Dispose();


        //    if (mRRate.RequestedMR <= maximumAllowedRate)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Requested MR exceeded maximum MR allowed");
        //    }


        //}


        [HttpPost]
        [ActionName("ValidateMRRate")]
        public bool ValidateMRRate(MRRate mRRate)
        {
            bool returnVal = false;

            string branchType = "";
            string relevantLimitRange = "";

            double maximumAllowedRate = 0.00;
            double sumInsured = 0.00;

            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr;
            con.Open();
            String sql = "";


            if (mRRate.BranchCode != null && mRRate.BranchCode != "")
            {

                branchType = getBranchType(mRRate.BranchCode);

            }

            if (branchType == "")
            {
                branchType = mRRate.BranchCode;

            }


            string AgentTypeMNBQHNB = Properties.Settings.Default.MNBQHNB;



            sumInsured = mRRate.SumInsured;

            relevantLimitRange = getRelevantLimitRange(sumInsured);


            if (relevantLimitRange == "")
            {
                relevantLimitRange = "RATE_FOR_LIMIT_5";
            }

            if (mRRate.AgentType == AgentTypeMNBQHNB)
            {
                sql = "SELECT " + relevantLimitRange + " FROM MNBQ_MR_LIMIT_GUIDE  " +
                     " WHERE BRANCH_TYPE=:V_BRANCH_TYPE " +
                   " AND RISK_TYPE_ID=:V_RISK_TYPE_ID " +
                   " AND USAGE_ID=:V_USAGE_ID " +
                    " AND FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE " +
                       " AND AGENT_TYPE_CODE=4" +
                     " AND MAKE_MODEL=:V_MAKE_MODEL" +
                    " AND YEAR_OF_MANUFACTURE=:V_YEAR_OF_MANU " +
                       " AND TO_DATE(SYSDATE,'DD/MM/RRRR') >=  TO_DATE(START_DATE,'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') <=TO_DATE(END_DATE,'DD/MM/RRRR') ";
            }
            else
            {
                sql = "SELECT " + relevantLimitRange + " FROM MNBQ_MR_LIMIT_GUIDE  " +
                   " WHERE BRANCH_TYPE=:V_BRANCH_TYPE " +
                   " AND RISK_TYPE_ID=:V_RISK_TYPE_ID " +
                   " AND USAGE_ID=:V_USAGE_ID " +
                    " AND FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE " +
                   " AND AGENT_TYPE_CODE=0" +
                     " AND MAKE_MODEL=:V_MAKE_MODEL" +
                    " AND YEAR_OF_MANUFACTURE=:V_YEAR_OF_MANU " +
                   " AND TO_DATE(SYSDATE,'DD/MM/RRRR') >=  TO_DATE(START_DATE,'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') <=TO_DATE(END_DATE,'DD/MM/RRRR') ";


            }




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_BRANCH_TYPE", branchType));
            cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", mRRate.RiskTypeId));
            cmd.Parameters.Add(new OracleParameter("V_USAGE_ID", mRRate.VehicleClassId));
            cmd.Parameters.Add(new OracleParameter("V_FUEL_TYPE_CODE", mRRate.FuelTypeCode));
            cmd.Parameters.Add(new OracleParameter("V_MAKE_MODEL", mRRate.MakeModelCode));
            cmd.Parameters.Add(new OracleParameter("V_YEAR_OF_MANU", mRRate.YearOfManufactureValidationId));



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                maximumAllowedRate = Convert.ToDouble(dr[0].ToString());
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            if (mRRate.RequestedMR <= maximumAllowedRate)
            {
                returnVal = true;
            }


            return returnVal;


        }


        private string getBranchType(string branchCode)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            con.Open();
            String sql = "";
            sql = "SELECT t.BRANCH_TYPE " +
                         " FROM mis_gi_branches t " +
                        " WHERE  t.BRANCH_CODE=:V_BRANCH_CODE";




            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_BRANCH_CODE", branchCode));


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    returnVal = dr[0].ToString();
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            return returnVal;
        }


        private string getRelevantLimitRange(double sumInsured)
        {
            string returnVal = "";


            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr;
            con.Open();
            String sql = "";
            sql = "select t.rate_no_for_range from mnbq_mr_limit_ranges t " +
                " where t.range_start<:V_SUM_INSURED and t.range_end>=:V_SUM_INSURED";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_SUM_INSURED", sumInsured));


            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    returnVal = dr[0].ToString();
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            return returnVal;
        }

      
        public double getRelevantMRRate(int riskType, int usageType, int fuelType, double ReqSumInsured, string branchType, string makeModel, string yearOfManu)
        {
            double returnVal = 0.00;
            string relevantLimitRange = "";

            try
            {
                OracleConnection con = new OracleConnection(ConnectionString);
                OracleDataReader dr;
                con.Open();
                String sql = "";





                relevantLimitRange = getRelevantLimitRange(ReqSumInsured);


                if (relevantLimitRange == "")
                {
                    relevantLimitRange = "RATE_FOR_LIMIT_5";
                }



                sql = "SELECT " + relevantLimitRange + " FROM MNBQ_MR_LIMIT_GUIDE  " +
                   " WHERE BRANCH_TYPE=:V_BRANCH_TYPE " +
                   " AND RISK_TYPE_ID=:V_RISK_TYPE_ID" +
                   " AND USAGE_ID=:V_USAGE_ID" +
                    " AND FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE" +
                   " AND AGENT_TYPE_CODE=0" +
                     " AND MAKE_MODEL=:V_MAKE_MODEL" +
                    " AND YEAR_OF_MANUFACTURE=:V_YEAR_OF_MANU " +
                   " AND TO_DATE(SYSDATE,'DD/MM/RRRR') >=  TO_DATE(START_DATE,'DD/MM/RRRR') AND TO_DATE(SYSDATE,'DD/MM/RRRR') <=TO_DATE(END_DATE,'DD/MM/RRRR') ";


                OracleCommand command = new OracleCommand(sql, con);

                // command.Parameters.Add(new OracleParameter("V_JOB_ID", relevantLimitRange));
                command.Parameters.Add(new OracleParameter("V_BRANCH_TYPE", branchType));
                command.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", riskType));
                command.Parameters.Add(new OracleParameter("V_USAGE_ID", usageType));
                command.Parameters.Add(new OracleParameter("V_FUEL_TYPE_CODE", fuelType));

                command.Parameters.Add(new OracleParameter("V_MAKE_MODEL", makeModel));
                command.Parameters.Add(new OracleParameter("V_YEAR_OF_MANU", yearOfManu));

                dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    returnVal = Convert.ToDouble(dr[0].ToString());
                }

                dr.Close();
                dr.Dispose();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception e)
            {

                throw;
            }



            return returnVal;
        }


        [HttpGet]
        [ActionName("CheckIsRequestSent")]
        public bool CheckIsRequestSent(string jobNo, string revisionNo)
        {
            bool returnVal = false;
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            con.Open();
            String sql = "";
            sql = "SELECT  COUNT(*)  FROM MNBQ_MR_APPROVAL T " +
                        " WHERE  T.JOB_ID=:V_JOB_ID AND  T.REVISION_NO=:V_REVISION_NO";

            
            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", jobNo));
            cmd.Parameters.Add(new OracleParameter("V_REVISION_NO", revisionNo));

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (Convert.ToInt32(dr[0].ToString()) >= 1)
                {
                    returnVal = true;
                }
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            return returnVal;
        }

        [HttpGet]
        [ActionName("CheckRequestStatus")]
        public string CheckRequestStatus(string jobNo, string revisionNo, string requestedMR)
        {
            string returnVal = "";
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            con.Open();
            String sql = "";
            sql = "SELECT T.REQUEST_STATUS  FROM MNBQ_MR_APPROVAL T " +
                        " WHERE  T.JOB_ID=:V_JOB_ID  AND T.REVISION_NO=:V_REVISION_NO" +
                        " AND T.SEQ_ID=(SELECT max(m.SEQ_ID) from MNBQ_MR_APPROVAL m where m.JOB_ID=:V_JOB_ID  AND m.REVISION_NO=:V_REVISION_NO) " +
                        " AND T.REQUESTED_MR>=:V_REQUESTED_MR";

            

            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", jobNo));
            cmd.Parameters.Add(new OracleParameter("V_REVISION_NO", revisionNo));
            cmd.Parameters.Add(new OracleParameter("V_REQUESTED_MR", requestedMR));

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                returnVal = dr[0].ToString();
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();


            return returnVal;
        }

    }
}
