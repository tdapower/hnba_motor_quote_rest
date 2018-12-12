using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class PostQuotationCoverRequest
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

        public List<QuotationCover> QuotationCovers { get; set; }
    }
}