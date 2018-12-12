using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationCalculate
    {
        public int JobId { get; set; }

        private int revisionId;
        public int RevisionId
        {
            get
            {
                return 0;
            }
            set
            {
                revisionId = value;
            }
        }


        public int RiskTypeId { get; set; }

        public int ProductCode { get; set; }
    }
}