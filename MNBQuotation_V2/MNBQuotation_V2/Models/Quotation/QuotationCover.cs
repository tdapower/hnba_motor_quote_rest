using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationCover
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
        public string Cover { get; set; }

        private string type { get; set; }//for dropdown values for selection types
        public string Type
        {
            get
            {
                return type == "" ? "0" : type;
            }
            set
            {
                type = value;
            }
        }



        private string amount { get; set; }
        public string Amount
        {
            get
            {
                return amount == "" ? "0" : amount;
            }
            set
            {
                amount = value;
            }
        }





    }
}