using MNBQuotation_V2.Models.Quotation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{

    public class MiscTypesController : ApiController
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();



        [HttpGet]
        [ActionName("GetVoluntaryByRiskTypeId")]
        public IEnumerable<MiscTypes> GetVoluntaryByRiskTypeId(int id)
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

            string sql = "";

            int MotorCycleRiskType = Properties.Settings.Default.MNBQMotorCycleRiskTypeId;

            if (id == MotorCycleRiskType)
            {
                sql = "SELECT t.name FROM MNB_COVER_AMOUNTS_AND_RATES T " +
                            " WHERE T.type='VOLUNTARY_EXCESS_MOTOR_CYCLE' AND  TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')  ";
            }
            else
            {
                sql = "SELECT t.name FROM MNB_COVER_AMOUNTS_AND_RATES T " +
                                 " WHERE T.type='VOLUNTARY_EXCESS_OTHER' AND  TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy')  ";

            }



            OracleCommand cmd = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                miscTypesList = (from DataRow drow in dt.Rows
                                 select new MiscTypes()
                                 {
                                     MiscTypeId = drow[0].ToString(),
                                     MiscTypeName = drow[0].ToString()
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
            return miscTypesList;
        }



        [HttpGet]
        [ActionName("ValidateAgentOrBrokerCode")]
        public HttpResponseMessage ValidateAgentOrBrokerCode(string agentBrokerCode)
        {

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {

                OracleConnection con = new OracleConnection(ConnectionString);
                OracleDataReader dr;
                con.Open();
                String sql = "";

                sql = "SELECT DISTINCT PPT.PTY_PARTY_CODE " +
                             " FROM T_PARTY PPT " +
                            " WHERE PPT.PTY_PARTY_CODE=:V_PTY_PARTY_CODE";

                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(new OracleParameter("V_PTY_PARTY_CODE", agentBrokerCode));

                dr = cmd.ExecuteReader();
                if (dr.HasRows) {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Agent or Broker Code");
                }

                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                con.Close();
                con.Dispose();

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpGet]
        [ActionName("GetPABToDriver")]
        public IEnumerable<MiscTypes> GetPABToDriver()
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();
            for (int i = 1; i <= 3; i++)
            {
                miscTypesList.Add(new MiscTypes() { MiscTypeId = i.ToString(), MiscTypeName = i.ToString() });
            }

            return miscTypesList;

        }


        [HttpGet]
        [ActionName("GetPABToPassenger")]
        public IEnumerable<MiscTypes> GetPABToPassenger()
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();
            for (int i = 1; i <= 20; i++)
            {
                miscTypesList.Add(new MiscTypes() { MiscTypeId = i.ToString(), MiscTypeName = i.ToString() });
            }

            return miscTypesList;

        }




        [HttpGet]
        [ActionName("GetLegalLiability")]
        public IEnumerable<MiscTypes> GetLegalLiability()
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();
            for (int i = 1; i <= 55; i++)
            {
                miscTypesList.Add(new MiscTypes() { MiscTypeId = i.ToString(), MiscTypeName = i.ToString() });
            }

            return miscTypesList;

        }

        

        [HttpGet]
        [ActionName("GetWCI")]
        public IEnumerable<MiscTypes> GetWCI()
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();
            for (int i = 1; i <= 3; i++)
            {
                miscTypesList.Add(new MiscTypes() { MiscTypeId = i.ToString(), MiscTypeName = i.ToString() });
            }

            return miscTypesList;

        }


        [HttpGet]
        [ActionName("GetYearOfManufactureValidation")]
        public IEnumerable<MiscTypes> GetYearOfManufactureValidation()
        {
            List<MiscTypes> miscTypesList = new List<MiscTypes>();

            miscTypesList.Add(new MiscTypes() { MiscTypeId = "Below 2010", MiscTypeName = "Below 2010" });
            miscTypesList.Add(new MiscTypes() { MiscTypeId = "Above 2010", MiscTypeName = "Above 2010" });

            miscTypesList.Add(new MiscTypes() { MiscTypeId = "Below 2013", MiscTypeName = "Below 2013" });
            miscTypesList.Add(new MiscTypes() { MiscTypeId = "Above 2013", MiscTypeName = "Above 2013" });

            return miscTypesList;

        }




    }
}
