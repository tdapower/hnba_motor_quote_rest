using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationSearchRequest
    {
        public string QuotationNo { get; set; }
        public string VehicleChasisNo { get; set; }
        public string JobId { get; set; }
        public string Status { get; set; }
        public string AgentBrokerCode { get; set; }
        public string RequestBy { get; set; }
        public string ClientName { get; set; }
        public string RequestDate { get; set; }

    }
}