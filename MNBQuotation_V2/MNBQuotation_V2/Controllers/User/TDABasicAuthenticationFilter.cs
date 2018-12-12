using MNBQuotation_V2.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace MNBQuotation_V2.Controllers.User
{
    public class TDABasicAuthenticationFilter : BasicAuthenticationFilter
    {

        public TDABasicAuthenticationFilter()
        { }

        public TDABasicAuthenticationFilter(bool active) : base(active)
        { }


        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var userAcc = new UserAccountController();

            var user = userAcc.AuthenticateAndLoad(username, password);
            if (user == null)
                return false;

            return true;
        }
    }

}