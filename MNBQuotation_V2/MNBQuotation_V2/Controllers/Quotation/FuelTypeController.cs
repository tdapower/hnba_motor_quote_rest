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
    public class FuelTypeController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();
     
        // GET api/fueltype
        public IEnumerable<FuelType> Get()
        {
            List<FuelType> fuelTypeList = new List<FuelType>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.FUEL_TYPE_CODE,t.FUEL_TYPE_NAME   FROM MNBQ_FUEL_TYPE t order by t.fuel_type_order ";


            OracleCommand command = new OracleCommand(sql, con);
            try
            {
                con.Open();
                dr = command.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                fuelTypeList = (from DataRow drow in dt.Rows
                                select new FuelType()
                                {
                                    FuelTypeId = Convert.ToInt32(drow["FUEL_TYPE_CODE"]),
                                    FuelTypeName = drow["FUEL_TYPE_NAME"].ToString()
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
            return fuelTypeList;
        }

        [HttpGet]
        [ActionName("GetFuelTypeByID")]
        public FuelType Get(int id)
        {
            FuelType fuelType = new FuelType();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";



            sql = "SELECT t.FUEL_TYPE_CODE,t.FUEL_TYPE_NAME     FROM MNBQ_FUEL_TYPE t WHERE t.FUEL_TYPE_CODE=:V_FUEL_TYPE_CODE ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_FUEL_TYPE_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    fuelType.FuelTypeId = Convert.ToInt32(dr["FUEL_TYPE_CODE"]);
                    fuelType.FuelTypeName = dr["FUEL_TYPE_NAME"].ToString();

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

            return fuelType;
        }

        // POST api/fueltype
        public void Post([FromBody]string value)
        {
        }

        // PUT api/fueltype/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/fueltype/5
        public void Delete(int id)
        {
        }
    }
}
