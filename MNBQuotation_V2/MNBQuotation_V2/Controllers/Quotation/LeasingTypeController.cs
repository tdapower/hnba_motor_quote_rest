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
    public class LeasingTypeController : ApiController
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
        // GET api/leasingtype
        public IEnumerable<LeasingType> Get()
        {
            List<LeasingType> leasingTypeList = new List<LeasingType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.LEASE_TYPE_CODE,t.LEASE_TYPE_NAME   FROM MNBQ_LEASE_TYPE t order by t.LEASE_TYPE_NAME ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                leasingTypeList = (from DataRow drow in dt.Rows
                                   select new LeasingType()
                                   {
                                       LeasingTypeId = Convert.ToInt32(drow["LEASE_TYPE_CODE"]),
                                       LeasingTypeName = drow["LEASE_TYPE_NAME"].ToString()
                                   }).ToList();
            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }

            }
            return leasingTypeList;
        }

        [HttpGet]
        [ActionName("GetLeasingTypeByID")]
        public LeasingType Get(int id)
        {
            LeasingType leasingType = new LeasingType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.LEASE_TYPE_CODE,t.LEASE_TYPE_NAME     FROM MNBQ_LEASE_TYPE t WHERE t.LEASE_TYPE_CODE=:V_LEASE_TYPE_CODE ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_LEASE_TYPE_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    leasingType.LeasingTypeId = Convert.ToInt32(dr["LEASE_TYPE_CODE"]);
                    leasingType.LeasingTypeName = dr["LEASE_TYPE_NAME"].ToString();

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

            return leasingType;
        }




        //Takaful
        [HttpGet]
        [ActionName("GetTakafulLeasingType")]
        public IEnumerable<LeasingType> GetTakafulLeasingType()
        {
            List<LeasingType> leasingTypeList = new List<LeasingType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.LEASE_TYPE_CODE,t.LEASE_TYPE_NAME   FROM MNBQ_T_LEASE_TYPE t order by t.LEASE_TYPE_NAME ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                leasingTypeList = (from DataRow drow in dt.Rows
                                   select new LeasingType()
                                   {
                                       LeasingTypeId = Convert.ToInt32(drow["LEASE_TYPE_CODE"]),
                                       LeasingTypeName = drow["LEASE_TYPE_NAME"].ToString()
                                   }).ToList();
            }
            catch (Exception exception)
            {
                if (dr != null || con.State == ConnectionState.Open)
                {
                    dr.Close();
                    con.Close();
                }

            }
            return leasingTypeList;
        }

        [HttpGet]
        [ActionName("GetTakafulLeasingTypeByID")]
        public LeasingType GetTakafulLeasingTypeByID(int id)
        {
            LeasingType leasingType = new LeasingType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.LEASE_TYPE_CODE,t.LEASE_TYPE_NAME     FROM MNBQ_T_LEASE_TYPE t WHERE t.LEASE_TYPE_CODE=:V_LEASE_TYPE_CODE ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_LEASE_TYPE_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    leasingType.LeasingTypeId = Convert.ToInt32(dr["LEASE_TYPE_CODE"]);
                    leasingType.LeasingTypeName = dr["LEASE_TYPE_NAME"].ToString();

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

            return leasingType;
        }

    }
}
