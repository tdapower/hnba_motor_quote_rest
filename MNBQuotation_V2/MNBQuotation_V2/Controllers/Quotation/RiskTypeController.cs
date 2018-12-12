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
    public class RiskTypeController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
     
        // GET api/risktype
        public IEnumerable<RiskType> Get()
        {
            List<RiskType> riskTypeList = new List<RiskType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.RISK_TYPE_ID,t.RISK_TYPE   FROM MNB_RISK_TYPE t order by t.RISK_TYPE ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                riskTypeList = (from DataRow drow in dt.Rows
                                select new RiskType()
                                {
                                    RiskTypeId = Convert.ToInt32(drow["RISK_TYPE_ID"]),
                                    RiskTypeName = drow["RISK_TYPE"].ToString()
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
            return riskTypeList;
        }



        [HttpGet]
        [ActionName("GetRiskTypeByID")]
        public RiskType Get(int id)
        {
            RiskType riskType = new RiskType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.RISK_TYPE_ID,t.RISK_TYPE     FROM MNB_RISK_TYPE t WHERE t.RISK_TYPE_ID=:V_RISK_TYPE_ID ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    riskType.RiskTypeId = Convert.ToInt32(dr["RISK_TYPE_ID"]);
                    riskType.RiskTypeName = dr["RISK_TYPE"].ToString();

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

            return riskType;
        }

    }
}
