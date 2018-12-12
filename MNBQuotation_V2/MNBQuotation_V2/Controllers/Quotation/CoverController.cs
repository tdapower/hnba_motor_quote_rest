using MNBQuotation_V2.Controllers.User;
using MNBQuotation_V2.Models.Quotation.Quotation;
using MNBQuotation_V2.Models.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation.Quotation
{
    public class CoverController : ApiController
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
        // GET: api/Cover

        //[TDABasicAuthenticationFilter(false)]
        public IEnumerable<Cover> Get()
        {

            var uIdentity = Thread.CurrentPrincipal.Identity;
            UserAccountController uc = new UserAccountController();
            UserAccount user= uc.getUserFromUserName(uIdentity.Name.ToString());


            List<Cover> coverList = new List<Cover>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            //string sql = "SELECT T.ID,T.COVER_CODE,T.COVER  FROM mnbq_covers T " +
            //    "   WHERE T.COVER_CODE IN (SELECT ALLOWED_COVER_CODES FROM MNB_ALLOWED_COVERS WHERE COMPANY=:V_COMPANY) " +
            //    " order by t.COVER_CODE ";


            //string sql = "SELECT T.ID,T.COVER_CODE,T.COVER  FROM mnbq_covers T " +
            //        "   WHERE T.COVER_CODE IN ('chk4','chk5','chk7','chk8','chk11','chk12','chk13','chk17') " +
            //        " order by t.COVER_CODE ";


            string sql = "SELECT T.ID,ac.COVER_CODE,T.COVER "+
                              " FROM MNB_ALLOWED_COVERS ac " +
                              " inner join mnbq_covers T on t.cover_code = ac.cover_code " +
                              " WHERE ac.COMPANY = :V_COMPANY";

    

            OracleCommand command = new OracleCommand(sql, con);
            command.Parameters.Add(new OracleParameter("V_COMPANY", user.Company));


            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                coverList = (from DataRow drow in dt.Rows
                             select new Cover()
                             {
                                 CoverId = Convert.ToInt32(drow["ID"].ToString()),
                                 CoverCode = drow["COVER_CODE"].ToString(),
                                 CoverName = drow["COVER"].ToString()
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
            return coverList;
        }


        public  List<Cover> GetAllowedCovers(string company)
        {

         

            List<Cover> coverList = new List<Cover>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);

            string sql = "SELECT T.ID,ac.COVER_CODE,T.COVER " +
                              " FROM MNB_ALLOWED_COVERS ac " +
                              " inner join mnbq_covers T on t.cover_code = ac.cover_code " +
                              " WHERE ac.COMPANY = :V_COMPANY";



            OracleCommand command = new OracleCommand(sql, con);
            command.Parameters.Add(new OracleParameter("V_COMPANY", company));


            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                coverList = (from DataRow drow in dt.Rows
                             select new Cover()
                             {
                                 CoverId = Convert.ToInt32(drow["ID"].ToString()),
                                 CoverCode = drow["COVER_CODE"].ToString(),
                                 CoverName = drow["COVER"].ToString()
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
            return coverList;
        }
    }
}
