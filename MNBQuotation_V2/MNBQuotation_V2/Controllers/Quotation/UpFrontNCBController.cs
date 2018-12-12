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
    public class UpFrontNCBController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();


        [HttpGet]
        [ActionName("GetUpFrontNCBByRiskTypeId")]
        public IEnumerable<UpFrontNCB> Get(int id)
        {
            List<UpFrontNCB> upFrontNCBList = new List<UpFrontNCB>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

            string sql = "";

            if (id == 7)
            {
                sql = "SELECT T.NAME,T.VALUE  FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-UP-MC'  AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY T.ID  ";
            }
            else
            {
                sql = "SELECT T.NAME,T.VALUE FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-UP-OTHER' AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY  T.ID  ";
            }

      

            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                upFrontNCBList = (from DataRow drow in dt.Rows
                                 select new UpFrontNCB()
                                 {
                                     UpFrontNCBName = drow["NAME"].ToString(),
                                     UpFrontNCBValue = drow["VALUE"].ToString()
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
            return upFrontNCBList;
        }

    }
}
