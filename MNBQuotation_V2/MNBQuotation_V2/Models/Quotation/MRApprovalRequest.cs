using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class MRApprovalRequest
    {
        public int SeqId { get; set; }
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
        public string RequestedBy { get; set; }
        public string RecomendedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
        public string ApproveBranchType { get; set; }
        public string RequestStatus { get; set; }
        public double RequestedMr { get; set; }
        public string ApproveRejectReason { get; set; }
        public string ApproveBranchCode { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }



    }
}