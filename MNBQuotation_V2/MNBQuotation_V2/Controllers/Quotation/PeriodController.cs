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
    public class PeriodController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();


        public IEnumerable<Period> Get()
        {
            List<Period> periodList = new List<Period>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "select mp.period_code,mp.period from mnbq_period mp  where mp.is_active=1  order by  mp.period_code ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                periodList = (from DataRow drow in dt.Rows
                                  select new Period()
                                  {
                                      PeriodId = Convert.ToInt32(drow["period_code"]),
                                      PeriodName = drow["period"].ToString()
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
            return periodList;
        }


        [HttpGet]
        [ActionName("GetPeriodByPeriodTypeId")]
        public IEnumerable<Period> GetPeriodByPeriodTypeId(int id)
        {
            List<Period> periodList = new List<Period>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "select mp.period_code,mp.period from mnbq_period mp   where mp.period_type_code =:V_PERIOD_TYPE_CODE  AND  mp.is_active=1  order by  mp.period_code  ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PERIOD_TYPE_CODE", id));

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                periodList = (from DataRow drow in dt.Rows
                              select new Period()
                              {
                                  PeriodId = Convert.ToInt32(drow["period_code"]),
                                  PeriodName = drow["period"].ToString()
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
            return periodList;
        }


    }
}
