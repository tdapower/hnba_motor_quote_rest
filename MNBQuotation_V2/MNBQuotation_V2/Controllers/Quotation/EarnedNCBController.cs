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
    public class EarnedNCBController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();


        [HttpGet]
        [ActionName("GetEarnedNCBByRiskTypeId")]
        public IEnumerable<EarnedNCB> Get(int id)
        {
            List<EarnedNCB> earnedNCBList = new List<EarnedNCB>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = ""; 

            if ( id == 7)
            {
                sql = "SELECT T.NAME,T.VALUE  FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-MC'  AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY T.ID  ";
            }
            else
            {
                sql = "SELECT T.NAME,T.VALUE FROM MNB_COVER_AMOUNTS_AND_RATES T WHERE T.TYPE='NCB-OTHER' AND TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(T.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(T.END_DATE,'dd/mm/yyyy') ORDER BY  T.ID  ";
            }

            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                earnedNCBList = (from DataRow drow in dt.Rows
                                 select new EarnedNCB()
                                 {
                                     EarnedNCBName = drow["NAME"].ToString(),
                                     EarnedNCBValue = drow["VALUE"].ToString()
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
            return earnedNCBList;
        }



    }
}
