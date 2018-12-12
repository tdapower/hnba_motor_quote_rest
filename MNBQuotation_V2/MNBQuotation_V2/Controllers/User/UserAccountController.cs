﻿using MNBQuotation_V2.Controllers.User;
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

namespace MNBQuotation_V2.Controllers.User
{
    public class UserAccountController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
        public UserAccount AuthenticateAndLoad(string userName, string password)
        {
            UserAccount u = null;
            //if (_userName == "tda" && _password == "tda")
            //{
            //    u = new UserAccount();
            //    u.userName = _userName;
            //    u.password = _password;
            //}
            u = checkAndLoadUser(userName, password);


            return u;
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("checkAndLoadUser")]
        [TDABasicAuthenticationFilter(false)]
        public UserAccount checkAndLoadUser(String userName, string password)
        {


            UserAccount u = new UserAccount();



            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr;

            con.Open();

            String sql = "";



            sql = "   SELECT T.USER_CODE,T.USER_NAME,T.COMPANY_CODE,T.EPF_NO,T.USER_ROLE_CODE,T.BRANCH_ID FROM WF_ADMIN_USERS_VW T  " +
               " WHERE T.USER_CODE=:V_USER_NAME AND  T.PASSWORD =:V_PASSWORD  AND T.STATUS=1 ";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_USER_NAME", userName));
            cmd.Parameters.Add(new OracleParameter("V_PASSWORD", password));



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                u.UserName = userName;
                u.Password = password;

                u.UserDisplayName = dr[1].ToString();
                u.Company = dr[2].ToString();


                u.Epf = dr[3].ToString();
                u.UserRoleCode = Convert.ToInt32(dr[4].ToString());
                u.BranchCode= dr[5].ToString();

                //var response = new HttpResponseMessage();


                //var nv = new NameValueCollection();
                //nv["userName"] = u.userName;
                //nv["userDisplayName"] = u.userDisplayName;
                //nv["company"] = u.company;
                //nv["epf"] = u.epf;
                //nv["userRoleCode"] = u.userRoleCode.ToString();
                //var cookie = new CookieHeaderValue("userSession", nv);



                //response.Headers.AddCookies(new CookieHeaderValue[] { cookie });


            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return u;


        }

        public UserAccount getUserFromUserName(String userName )
        {


            UserAccount u = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr;

            con.Open();

            String sql = "";
            sql = "   SELECT T.USER_CODE,T.USER_NAME,T.COMPANY_CODE,T.BRANCH_ID,T.EPF_NO,T.USER_ROLE_CODE FROM WF_ADMIN_USERS T  " +
               " WHERE T.USER_CODE=:V_USER_NAME   AND T.STATUS=1 ";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_USER_NAME", userName));



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                u = new UserAccount();
                u.UserName = userName;

                u.UserDisplayName = dr[1].ToString();
                u.Company = dr[2].ToString();
                u.BranchCode = dr[3].ToString();

                u.Epf = dr[4].ToString();
                u.UserRoleCode = Convert.ToInt32(dr[5].ToString());
                

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return u;


        }

        [HttpGet]
        [ActionName("ValidateUser")]
        [TDABasicAuthenticationFilter(false)]
        private string ValidateUser(String userName, string password)
        {

            string returnValue = "false";


            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr;

            con.Open();

            String sql = "";



            sql = "   SELECT T.USER_CODE,T.USER_NAME,T.COMPANY_CODE,T.EPF_NO,T.USER_ROLE_CODE FROM WF_ADMIN_USERS_VW T  " +
               " WHERE T.USER_CODE=:V_USER_NAME AND  T.PASSWORD =:V_PASSWORD  AND T.STATUS=1 ";



            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(new OracleParameter("V_USER_NAME", userName));
            cmd.Parameters.Add(new OracleParameter("V_PASSWORD", password));



            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                returnValue = "true";

            }
            dr.Close();
            dr.Dispose();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            return returnValue;


        }
    }
}