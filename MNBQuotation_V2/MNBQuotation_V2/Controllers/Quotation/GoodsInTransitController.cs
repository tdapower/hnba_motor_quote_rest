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
    public class GoodsInTransitController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<GoodsInTransit> Get()
        {
            List<GoodsInTransit> goodsInTransitList = new List<GoodsInTransit>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.GIT_TYPE_CODE,t.GIT_TYPE   FROM MNBQ_GIT_TYPE t order by t.GIT_TYPE ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                goodsInTransitList = (from DataRow drow in dt.Rows
                                      select new GoodsInTransit()
                                      {
                                          GoodsInTransitId = Convert.ToInt32(drow["GIT_TYPE_CODE"]),
                                          GoodsInTransitName = drow["GIT_TYPE"].ToString()
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
            return goodsInTransitList;
        }



        [HttpGet]
        [ActionName("GetGoodsInTransitById")]
        public GoodsInTransit Get(int id)
        {
            GoodsInTransit goodsInTransit = new GoodsInTransit();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.GIT_TYPE_CODE,t.GIT_TYPE     FROM MNBQ_GIT_TYPE t WHERE t.GIT_TYPE_CODE=:V_GIT_TYPE_CODE ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_GIT_TYPE_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    goodsInTransit.GoodsInTransitId = Convert.ToInt32(dr["GIT_TYPE_CODE"]);
                    goodsInTransit.GoodsInTransitName = dr["GIT_TYPE"].ToString();

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

            return goodsInTransit;
        }


     
    }
}
