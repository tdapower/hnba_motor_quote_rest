using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationResultForExt
    {
        public int JOB_ID { get; set; }
        public int REVISION_NO { get; set; }
        public string QUOTATION_NO { get; set; }
        public double SUM_INSURED { get; set; }
        public double BASIC_PREMIUM { get; set; }
        public double TOTAL_SRCC { get; set; }
        public double TOTAL_TC { get; set; }
        public double STAMP_DUTY { get; set; }
        public double TAXES { get; set; }
        public double PREMIUM_WITH_VAT { get; set; }
      
    }
}