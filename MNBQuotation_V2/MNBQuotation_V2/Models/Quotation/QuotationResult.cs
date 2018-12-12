using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNBQuotation_V2.Models.Quotation
{
    public class QuotationResult
    {
        public int JOB_ID { get; set; }
        public int REVISION_NO { get; set; }
        public string QUOTATION_NO { get; set; }
        public double SUM_INSURED { get; set; }
        public double BASIC_PREMIUM { get; set; }
        public double CRSF { get; set; }
        public double CRSF_WITH_VAT { get; set; }
        public double MULTIPLE_REBATE { get; set; }
        public double MUL_REBATE_VAL { get; set; }
        public double NCB_TYPE { get; set; }
        public double NCB_PERC { get; set; }
        public double DEPRECIATION_CVR { get; set; }
        public double VAP { get; set; }
        public double SUB_TOTAL_A { get; set; }
        public double SUB_TOTAL_B { get; set; }
        public double SUB_TOTAL_C { get; set; }
        public double SUB_TOTAL_D { get; set; }
        public double SUB_TOTAL_E { get; set; }
        public double SUB_TOTAL_BEFORE_VAP { get; set; }
        public double SUB_TOTAL_MAN_TOT_PREM { get; set; }
        public double SUB_TOTAL_MAN_NET_PREM { get; set; }
        public double SUB_TOTAL_F { get; set; }
        public double SUB_TOTAL_F_WITH_VAT { get; set; }
        public double SRCC_FOR_VEHICLE { get; set; }
        public double SRCC_FOR_GOODS { get; set; }
        public double SRCC_FOR_PAB { get; set; }
        public double SRCC_FOR_WCI { get; set; }
        public double TOTAL_SRCC { get; set; }
        public double TOTAL_SRCC_WITH_VAT { get; set; }
        public double TC_FOR_VEHICLE { get; set; }
        public double TC_FOR_GOODS { get; set; }
        public double TC_FOR_PAB { get; set; }
        public double TC_FOR_WCI { get; set; }
        public double TOTAL_TC { get; set; }
        public double TOTAL_TC_WITH_VAT { get; set; }
        public double GROSS_PREMIUM { get; set; }
        public double ADMIN_FEE { get; set; }
        public double ADMIN_FEE_WITH_VAT { get; set; }
        public double POLICY_FEE { get; set; }
        public double POLICY_FEE_WITH_VAT { get; set; }
        public double TOTAL_FOR_STAMP { get; set; }
        public double STAMP_DUTY { get; set; }
        public double STAMP_DUTY_WITH_VAT { get; set; }
        public double NBT { get; set; }
        public double NBT_WITH_VAT { get; set; }
        public double TAXES { get; set; }
        public double PREMIUM { get; set; }
        public double PREMIUM_WITH_VAT { get; set; }
        public double multiple_rebate { get; set; }
        public double hire_purchase { get; set; }
        public double voluntary_excess { get; set; }
        public double aac_membership { get; set; }
        public double pab_for_driver { get; set; }
        public double pab_for_passenger { get; set; }
        public double good_in_transit { get; set; }
        public double legal_liability { get; set; }
        public double towing_charge { get; set; }
        public double ncb { get; set; }
        public double up_front_ncb { get; set; }
        public double windscreen { get; set; }
        public double tppd { get; set; }
        public double wci { get; set; }
        public double inclusion_of_excluded_items { get; set; }
        public double learner_rider_driver { get; set; }
        public double ctb { get; set; }
        public double rent_a_car { get; set; }
        public double flood { get; set; }
        public double driving_tuition { get; set; }
        public double duty_free_duty_concession { get; set; }
        public double adjustment_fee { get; set; }
        public double theft_of_parts { get; set; }
        public double srcc_for_vehicles { get; set; }
        public double srcc_for_goods { get; set; }
        public double srcc_for_pab { get; set; }
        public double tc_for_pab { get; set; }
        public double srcc_for_wci { get; set; }
        public double tc_for_vehicles { get; set; }
        public double tc_for_goods { get; set; }
        public double tc_for_wci { get; set; }
        public double air_bag_replacement { get; set; }

    }
}