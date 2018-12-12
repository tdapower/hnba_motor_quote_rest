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


    public class VehicleUsageController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
        public IEnumerable<VehicleUsage> Get()
        {

            List<VehicleUsage> vehicleUsageList = new List<VehicleUsage>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.VEHICLE_CLASS_ID,t.VEHICLE_CLASS   FROM MNB_VEHICLE_CLASS t order by t.VEHICLE_CLASS ";
            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                vehicleUsageList = (from DataRow drow in dt.Rows
                                    select new VehicleUsage()
                                   {
                                       VehicleUsageId = Convert.ToInt32(drow["VEHICLE_CLASS_ID"]),
                                       VehicleUsageName = drow["VEHICLE_CLASS"].ToString()
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
            return vehicleUsageList;
        }

        
        public IHttpActionResult GetVehicleUsage(int id)
        {
            VehicleUsage vehicleUsage = new VehicleUsage();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";

            sql = "SELECT t.VEHICLE_CLASS_ID,t.VEHICLE_CLASS   FROM MNB_VEHICLE_CLASS t WHERE t.VEHICLE_CLASS_ID=:V_VEHICLE_CLASS_ID  ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_CLASS_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    vehicleUsage.VehicleUsageId = Convert.ToInt32(dr["VEHICLE_CLASS_ID"]);
                    vehicleUsage.VehicleUsageName = dr["VEHICLE_CLASS"].ToString();

                    dr.Close();
                    con.Close();

                }
                else
                {
                    return NotFound();
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

            return Ok(vehicleUsage);
        }

        [HttpGet]
        [ActionName("GetVehicleUsageByRiskTypeIDAndVehicleType")]
        public IEnumerable<VehicleUsage> GetVehicleUsageByRiskTypeIDAndVehicleType(int riskTypeId, int vehicleTypeId)
        {
            //api/VehicleUsage/GetVehicleUsageByRiskTypeIDAndVehicleType?riskTypeId=8&vehicleTypeId=9


            List<VehicleUsage> vehicleUsageList = new List<VehicleUsage>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);


            string sql = "select distinct mbr.VEHICLE_CLASS_ID,mvc.VEHICLE_CLASS from mnb_basic_rate mbr " +
            " inner join Mnb_Vehicle_Class mvc on mvc.VEHICLE_CLASS_ID=mbr.VEHICLE_CLASS_ID " +
            " where mbr.risk_type_id=:V_RISK_TYPE_ID and  mbr.vehicle_type_id=:V_VEHICLE_TYPE_ID " +
            " ORDER BY mvc.VEHICLE_CLASS ASC";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", riskTypeId));
            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_TYPE_ID", vehicleTypeId));


            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                vehicleUsageList = (from DataRow drow in dt.Rows
                                    select new VehicleUsage()
                                    {
                                        VehicleUsageId = Convert.ToInt32(drow[0]),
                                        VehicleUsageName = drow[1].ToString()
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
            return vehicleUsageList;
        }

    }
}
