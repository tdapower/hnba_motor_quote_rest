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
    public class LegalLiabilityController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<LegalLiability> Get()
        {
            List<LegalLiability> legalLiabilityList = new List<LegalLiability>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.NAME   FROM MNB_COVER_AMOUNTS_AND_RATES t "+
                " WHERE  t.TYPE='Legal Liability'  " +
               " AND   TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(t.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(t.END_DATE,'dd/mm/yyyy')"+
                " order by t.VALUE ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                legalLiabilityList = (from DataRow drow in dt.Rows
                                      select new LegalLiability()
                                      {
                                          LegalLiabilityName = drow["NAME"].ToString()
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
            return legalLiabilityList;
        }

        [HttpGet]
        [ActionName("GetLegalLiabilityById")]
        public LegalLiability Get(int id)
        {
            LegalLiability legalLiability = new LegalLiability();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


             sql = "SELECT t.NAME   FROM MNB_COVER_AMOUNTS_AND_RATES t " +
                 " WHERE  t.TYPE='Legal Liability'    " +
                " AND   TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(t.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(t.END_DATE,'dd/mm/yyyy')" +
                 " order by t.NAME ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_NAME", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    legalLiability.LegalLiabilityName = dr["NAME"].ToString();

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

            return legalLiability;
        }


        
    }
}
