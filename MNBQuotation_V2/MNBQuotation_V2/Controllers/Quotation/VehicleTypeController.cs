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
    public class VehicleTypeController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        public IEnumerable<VehicleType> Get()
        {
            List<VehicleType> vehicleTypeList = new List<VehicleType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.VEHICLE_TYPE_ID,t.VEHICLE_TYPE   FROM Mnb_Vehicle_Type t order by t.VEHICLE_TYPE ";
            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                vehicleTypeList = (from DataRow drow in dt.Rows
                                   select new VehicleType()
                                   {
                                       VehicleTypeId = Convert.ToInt32(drow["VEHICLE_TYPE_ID"]),
                                       VehicleTypeName = drow["VEHICLE_TYPE"].ToString()
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
            return vehicleTypeList;
        }


        [HttpGet]
        [ActionName("GetVehicleTypeByID")]
        public VehicleType Get(int id)
        {
            VehicleType vehicleType = new VehicleType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.VEHICLE_TYPE_ID,t.VEHICLE_TYPE   FROM Mnb_Vehicle_Type t WHERE t.VEHICLE_TYPE_ID=:V_VEHICLE_TYPE_ID ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_VEHICLE_TYPE_ID", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    vehicleType.VehicleTypeId = Convert.ToInt32(dr["VEHICLE_TYPE_ID"]);
                    vehicleType.VehicleTypeName = dr["VEHICLE_TYPE"].ToString();

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

            return vehicleType;
        }

        [HttpGet]
        [ActionName("GetVehicleTypeByRiskTypeId")]
        public IEnumerable<VehicleType> GetVehicleTypeByRiskTypeId(int id)
        {
            List<VehicleType> vehicleTypeList = new List<VehicleType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "select distinct mbr.vehicle_type_id,mvt.vehicle_type from mnb_basic_rate mbr " +
                      " inner join MNB_VEHICLE_TYPE mvt on mvt.vehicle_type_id=mbr.vehicle_type_id " +
                      " where mbr.risk_type_id=:V_RISK_TYPE_ID " +
                      " ORDER BY mvt.vehicle_type ASC";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_RISK_TYPE_ID", id));
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                vehicleTypeList = (from DataRow drow in dt.Rows
                                   select new VehicleType()
                                   {
                                       VehicleTypeId = Convert.ToInt32(drow["VEHICLE_TYPE_ID"]),
                                       VehicleTypeName = drow["VEHICLE_TYPE"].ToString()
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
            return vehicleTypeList;
        }




    }
}
