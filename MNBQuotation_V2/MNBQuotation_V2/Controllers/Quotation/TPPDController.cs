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
    public class TPPDController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<TPPD> Get()
        {
            List<TPPD> tPPDList = new List<TPPD>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

            string sql = "SELECT t.NAME   FROM MNB_COVER_AMOUNTS_AND_RATES t  " +
                " WHERE  t.TYPE='TPPD'  " +
               " AND   TO_DATE(SYSDATE,'dd/mm/yyyy') >=  TO_DATE(t.START_DATE,'dd/mm/yyyy') AND TO_DATE(SYSDATE,'dd/mm/yyyy') <=TO_DATE(t.END_DATE,'dd/mm/yyyy')" +
                " order by t.value ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                tPPDList = (from DataRow drow in dt.Rows
                            select new TPPD()
                            {
                                TPPDName = drow["NAME"].ToString()
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
            return tPPDList;
        }



    }
}
