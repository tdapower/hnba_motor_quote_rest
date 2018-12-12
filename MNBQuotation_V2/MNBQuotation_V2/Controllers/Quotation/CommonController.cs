using MNBQuotation_V2.Controllers.Quotation.Quotation;
using MNBQuotation_V2.Controllers.User;
using MNBQuotation_V2.Models.Quotation;
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
using System.Threading;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{
    public class CommonController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();



     
    }
}
