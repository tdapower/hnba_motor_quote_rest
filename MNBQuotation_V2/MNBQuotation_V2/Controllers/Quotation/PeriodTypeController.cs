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
    public class PeriodTypeController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<PeriodType> Get()
        {
            List<PeriodType> periodTypeList = new List<PeriodType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.PERIOD_TYPE_CODE,t.PERIOD_TYPE_NAME   FROM MNBQ_PERIOD_TYPE t where t.is_active=1 order by t.PERIOD_TYPE_NAME ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                periodTypeList = (from DataRow drow in dt.Rows
                                  select new PeriodType()
                                  {
                                      PeriodTypeId = Convert.ToInt32(drow["PERIOD_TYPE_CODE"]),
                                      PeriodTypeName = drow["PERIOD_TYPE_NAME"].ToString()
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
            return periodTypeList;
        }



        [HttpGet]
        [ActionName("GetPeriodTypeById")]
        public PeriodType Get(int id)
        {
            PeriodType periodType = new PeriodType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.PERIOD_TYPE_CODE,t.PERIOD_TYPE_NAME     FROM MNBQ_PERIOD_TYPE t WHERE t.PERIOD_TYPE_CODE=:V_PERIOD_TYPE_CODE and  t.is_active=1  ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PERIOD_TYPE_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    periodType.PeriodTypeId = Convert.ToInt32(dr["PERIOD_TYPE_CODE"]);
                    periodType.PeriodTypeName = dr["PERIOD_TYPE_NAME"].ToString();

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

            return periodType;
        }




    }
}
