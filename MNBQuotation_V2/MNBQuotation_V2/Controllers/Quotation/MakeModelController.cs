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
    public class MakeModelController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        [HttpGet]
        public IEnumerable<MakeModel> Get()
        {


            List<MakeModel> makeModeleList = new List<MakeModel>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

            string sql = "SELECT t.MAKE_MODEL_CODE,t.MAKE_MODEL_NAME  FROM mnbq_make_model t ";



            OracleCommand cmd = new OracleCommand(sql, con);

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                makeModeleList = (from DataRow drow in dt.Rows
                                  select new MakeModel()
                                  {
                                      MakeModelId = Convert.ToInt32(drow[0]),
                                      MakeModelName = drow[1].ToString()
                                  }).ToList();

                if (makeModeleList.Count == 0)
                {
                    makeModeleList.Add(new MakeModel() { MakeModelId = 0, MakeModelName = "" });

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
            return makeModeleList;
        }

        [HttpGet]
        [ActionName("GetMakeModelByRiskTypeID")]
        public IEnumerable<MakeModel> GetMakeModelByRiskTypeID(int id)
        {


            List<MakeModel> makeModeleList = new List<MakeModel>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

           string sql = "SELECT t.MAKE_MODEL_CODE,t.MAKE_MODEL_NAME  FROM mnbq_make_model t WHERE t.RISK_TYPE_CODE=:V_RISK_TYPE_ID ";

           

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", id));

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                makeModeleList = (from DataRow drow in dt.Rows
                                    select new MakeModel()
                                    {
                                        MakeModelId = Convert.ToInt32(drow[0]),
                                        MakeModelName = drow[1].ToString()
                                    }).ToList();

                if (makeModeleList.Count == 0)
                {
                    makeModeleList.Add(new MakeModel() { MakeModelId = 0, MakeModelName = "" });

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
            return makeModeleList;
        }
    }
}
