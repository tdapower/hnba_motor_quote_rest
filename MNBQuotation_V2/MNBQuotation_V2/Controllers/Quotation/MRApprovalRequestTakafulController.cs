using MNBQuotation_V2.Models.Quotation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MNBQuotation_V2.Controllers.Quotation
{
    public class MRApprovalRequestTakafulController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        [HttpPost]
        [ActionName("AddMRRequest")]
        public HttpResponseMessage AddMRRequest(MRApprovalRequest mRApprovalRequest)
        {

            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
                OracleCommand cmd = null;

                cmd = new OracleCommand("INSERT_T_MR_APPROVAL");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add("V_JOB_ID", OracleType.Number).Value = mRApprovalRequest.JobId;
                cmd.Parameters.Add("V_REVISION_NO", OracleType.Number).Value = mRApprovalRequest.RevisionId;
                cmd.Parameters.Add("V_REQUESTED_BY", OracleType.VarChar).Value = mRApprovalRequest.RequestedBy;
                cmd.Parameters.Add("V_REMARKS", OracleType.VarChar).Value = mRApprovalRequest.Remarks;
                cmd.Parameters.Add("V_APPROVE_BRANCH_TYPE", OracleType.VarChar).Value = mRApprovalRequest.ApproveBranchCode;

                cmd.Parameters.Add("V_REQUESTED_MR", OracleType.Number).Value = mRApprovalRequest.RequestedMr;

                cmd.ExecuteNonQuery();
                con.Close();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }

        }


        [HttpGet]
        [ActionName("GetMRApprovalRequestDetails")]
        public MRApprovalRequest Get(int id)
        {
            MRApprovalRequest mRApprovalRequest = new MRApprovalRequest();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "   SELECT " +
                            "MMA.SEQ_ID             ," +//0
                               "CASE WHEN MMA.JOB_ID   IS NULL THEN 0  ELSE MMA.JOB_ID   END," + //1
                               "CASE WHEN MMA.REVISION_NO  IS NULL THEN 0  ELSE MMA.REVISION_NO   END," +//2
                            "CASE WHEN MMA.REQUESTED_BY  IS NULL THEN ''  ELSE MMA.REQUESTED_BY   END," +//3
                             "CASE WHEN MMA.RECOMENDED_BY  IS NULL THEN ''  ELSE MMA.RECOMENDED_BY   END," +//4
                             "CASE WHEN MMA.APPROVED_BY 		 IS NULL THEN ''  ELSE MMA.APPROVED_BY   END," +//5
                             "CASE WHEN MMA.REMARKS    IS NULL THEN ''  ELSE MMA.REMARKS   END," +//6
                             "CASE WHEN MMA.APPROVE_BRANCH_TYPE  IS NULL THEN ''  ELSE MMA.APPROVE_BRANCH_TYPE   END," +//7
                             "CASE WHEN MMA.REQUEST_STATUS  IS NULL THEN ''  ELSE MMA.REQUEST_STATUS   END," +//8
                             "CASE WHEN MMA.REQUESTED_MR   IS NULL THEN 0  ELSE MMA.REQUESTED_MR   END," +//9
                             "CASE WHEN MMA.APPROVE_REJECT_REASON  IS NULL THEN ''  ELSE MMA.APPROVE_REJECT_REASON     END," +//10
                             "CASE WHEN MMA.APPROVE_BRANCH_CODE  IS NULL THEN ''  ELSE MMA.APPROVE_BRANCH_CODE   END," +//11
                             "CASE WHEN WAU.USER_NAME  IS NULL THEN ''  ELSE WAU.USER_NAME   END, " +//13
                             "CASE WHEN WAU.USER_EMAIL  IS NULL THEN ''  ELSE WAU.USER_EMAIL   END " +//12
                             " FROM MNBQ_T_MR_APPROVAL MMA  " +
                             " INNER JOIN WF_ADMIN_USERS WAU ON MMA.REQUESTED_BY=WAU.USER_CODE   " +
                            " WHERE MMA.JOB_ID=:V_JOB_ID";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_JOB_ID", id));
            // cmd.Parameters.Add(new OracleParameter("V_REVISION_NO", id));


            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    mRApprovalRequest.SeqId = Convert.ToInt32(dr[0].ToString());
                    mRApprovalRequest.JobId = Convert.ToInt32(dr[1].ToString());
                    mRApprovalRequest.RequestedBy = dr[3].ToString();
                    mRApprovalRequest.RecomendedBy = dr[4].ToString();
                    mRApprovalRequest.ApprovedBy = dr[5].ToString();
                    mRApprovalRequest.Remarks = dr[6].ToString();
                    mRApprovalRequest.ApproveBranchType = dr[7].ToString();
                    mRApprovalRequest.RequestStatus = dr[8].ToString();
                    mRApprovalRequest.RequestedMr = Convert.ToDouble(dr[9].ToString());
                    mRApprovalRequest.ApproveRejectReason = dr[10].ToString();
                    mRApprovalRequest.ApproveBranchCode = dr[11].ToString();
                    mRApprovalRequest.UserName = dr[12].ToString();
                    mRApprovalRequest.UserEmail = dr[13].ToString();



                    dr.Close();
                    con.Close();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }
            }
            finally
            {
                con.Close();

            }

            return mRApprovalRequest;
        }

    }
}
