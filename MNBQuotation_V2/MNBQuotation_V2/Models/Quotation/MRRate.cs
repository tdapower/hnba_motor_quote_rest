using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class MRRate
    {
        public int JobId { get; set; }
        
        public int RevisionNo { get; set; }
        public string BranchCode { get; set; }
        public int RiskTypeId { get; set; }
        public int VehicleClassId { get; set; } //Usage
        public int FuelTypeCode { get; set; }
        public string AgentType { get; set; }

        public int ProductCode { get; set; }
        public string MakeModelCode { get; set; }
        public string YearOfManufactureValidationId { get; set; }

        public double SumInsured { get; set; }
        public double RequestedMR { get; set; }


    }
}