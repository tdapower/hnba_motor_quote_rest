using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationMain
    {
        public string QuotationNo { get; set; }
        public int JobId  { get; set; }

        public string RequestBy { get; set; }
        [Required(ErrorMessage = "Client name is required")]

        public string ClientName { get; set; }

        public string VehicleChasisNo { get; set; }

        [Required(ErrorMessage = "Vehicle Risk Type is required")]
        [Range(1,9)]
        public int RiskTypeId { get; set; }

        [Required(ErrorMessage = "Vehicle Type is required")]
        [Range(1, 58)]
        public int VehicleTypeId { get; set; }

        [Required(ErrorMessage = "Vehicle Usage is required")]
        [Range(1, 5)]
        public int VehicleUsageId { get; set; } //VehicleClassId

        public double SumInsured { get; set; }

        [Required(ErrorMessage = "Period type of cover is required")]
        [Range(1, 3)]
        public int PeriodTypeCode { get; set; }

        [Required(ErrorMessage = "Period of cover is required")]
        public int PeriodOfCoverCode { get; set; }


        [Required(ErrorMessage = "Agent/Broker type is required")]
        public string AgentBroker { get; set; }


        [Required(ErrorMessage = "Agent/Broker code is required")]
        public string AgentBrokerCode { get; set; }


        [Required(ErrorMessage = "Leasing Type is required")]
        [Range(1, 3)]
        public int LeasingType { get; set; }

        [Required(ErrorMessage = "Fuel Type is required")]
        [Range(1, 5)]
        public int FuelTypeCode { get; set; }


        [Required(ErrorMessage = "Product is required")]
        [Range(1, 4)]
        public int ProductCode { get; set; }
        public string BranchId { get; set; }
        public string Remark { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public int RevisionNo { get; set; }
        public string QuotYear { get; set; }
        public string YearOfManu { get; set; }


        public string YearOfManufactureValidationId { get; set; }
        public string MakeModelCode { get; set; }
        //public string ArExcitationID { get; set; }
        //public string ArRebate { get; set; }

    }
}