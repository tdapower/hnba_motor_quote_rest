using MNBQuotation_V2.Controllers.User;
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
    public class ProductController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["OracleConString"].ToString();

        [HttpGet]
        [ActionName("GetProducts")]
        public IEnumerable<Product> GetProducts()
        {

            int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;

            List<Product> productList = new List<Product>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.PRODUCT_CODE,t.PRODUCT   FROM MNBQ_PRODUCT t WHERE PRODUCT_CODE<>:V_PRODUCT_CODE AND   IS_ACTIVE=1  order by t.PRODUCT ";


            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PRODUCT_CODE", MNBQTakafulProductCode));


            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                productList = (from DataRow drow in dt.Rows
                               select new Product()
                               {
                                   ProductId = Convert.ToInt32(drow["PRODUCT_CODE"]),
                                   ProductName = drow["PRODUCT"].ToString()
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
            return productList;
        }


        [HttpGet]
        [ActionName("GetAllowedProducts")]
        public IEnumerable<Product> GetAllowedProducts(string vehicleTypeId, string usageId)
        {
            if (vehicleTypeId !=null && usageId != null)
            {


                int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;

                List<Product> productList = new List<Product>();
                DataTable dt = new DataTable();
                OracleDataReader dr = null;
                OracleConnection con = new OracleConnection(ConnectionString);
                string sql = "SELECT  t.PRODUCT_CODE,t.PRODUCT,t.ALLOWED_VEHICLE_TYPES,t.ALLOWED_VEHICLE_USAGES    FROM MNBQ_PRODUCT t WHERE PRODUCT_CODE<>:V_PRODUCT_CODE AND   IS_ACTIVE=1  order by t.PRODUCT ";


                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(new OracleParameter("V_PRODUCT_CODE", MNBQTakafulProductCode));


                try
                {
                    con.Open();
                    dr = cmd.ExecuteReader();

                    

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {


                            string[] allowedVehicleTypes = dr[2].ToString().Split(',');
                            foreach (string allowedVehicleType in allowedVehicleTypes)
                            {
                                if (allowedVehicleType == vehicleTypeId || allowedVehicleType == "ALL")
                                {
                                    string[] allowedVehicleUsages = dr[3].ToString().Split(',');
                                    foreach (string allowedVehicleUsage in allowedVehicleUsages)
                                    {
                                        if (allowedVehicleUsage == usageId || allowedVehicleUsage == "ALL")
                                        {
                                          
                                            productList.Add(new Product()
                                            {
                                                ProductId = Convert.ToInt32(dr["PRODUCT_CODE"]),
                                                ProductName = dr["PRODUCT"].ToString()
                                            });

                                        }
                                    }

                                }
                            }
                        }
                    }






                    dt.Load(dr);
                    dr.Close();
                    con.Close();
               
                }
                catch (Exception exception)
                {
                    if (dr != null || con.State == ConnectionState.Open)
                    {
                        dr.Close();
                        con.Close();
                    }

                }
                return productList;

            }else
            {
                return null;
            }
        }



        [HttpGet]
        [ActionName("GetTakafulProducts")]
        public IEnumerable<Product> GetTakafulProducts()
        {

            int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;

            List<Product> productList = new List<Product>();
            DataTable dt = new DataTable();
            OracleDataReader dr = null;
            OracleConnection con = new OracleConnection(ConnectionString);
            string sql = "SELECT t.PRODUCT_CODE,t.PRODUCT   FROM MNBQ_PRODUCT t   WHERE PRODUCT_CODE=:V_PRODUCT_CODE AND   IS_ACTIVE=1  order by t.PRODUCT ";



            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PRODUCT_CODE", MNBQTakafulProductCode));
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Close();
                con.Close();
                productList = (from DataRow drow in dt.Rows
                               select new Product()
                               {
                                   ProductId = Convert.ToInt32(drow["PRODUCT_CODE"]),
                                   ProductName = drow["PRODUCT"].ToString()
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
            return productList;
        }


        [HttpGet]
        [ActionName("GetAllowedTakafulProducts")]
        public IEnumerable<Product> GetAllowedTakafulProducts(string vehicleTypeId, string usageId)
        {
            if (vehicleTypeId != null && usageId != null)
            {


                int MNBQTakafulProductCode = Properties.Settings.Default.MNBQTakafulProductCode;

                List<Product> productList = new List<Product>();
                DataTable dt = new DataTable();
                OracleDataReader dr = null;
                OracleConnection con = new OracleConnection(ConnectionString);
                string sql = "SELECT  t.PRODUCT_CODE,t.PRODUCT,t.ALLOWED_VEHICLE_TYPES,t.ALLOWED_VEHICLE_USAGES    FROM MNBQ_PRODUCT t WHERE PRODUCT_CODE=:V_PRODUCT_CODE AND   IS_ACTIVE=1  order by t.PRODUCT ";


                OracleCommand cmd = new OracleCommand(sql, con);
                cmd.Parameters.Add(new OracleParameter("V_PRODUCT_CODE", MNBQTakafulProductCode));
                
                try
                {
                    con.Open();
                    dr = cmd.ExecuteReader();
                    

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string[] allowedVehicleTypes = dr[2].ToString().Split(',');
                            foreach (string allowedVehicleType in allowedVehicleTypes)
                            {
                                if (allowedVehicleType == vehicleTypeId || allowedVehicleType == "ALL")
                                {
                                    string[] allowedVehicleUsages = dr[3].ToString().Split(',');
                                    foreach (string allowedVehicleUsage in allowedVehicleUsages)
                                    {
                                        if (allowedVehicleUsage == usageId || allowedVehicleUsage == "ALL")
                                        {

                                            productList.Add(new Product()
                                            {
                                                ProductId = Convert.ToInt32(dr["PRODUCT_CODE"]),
                                                ProductName = dr["PRODUCT"].ToString()
                                            });

                                        }
                                    }

                                }
                            }
                        }
                    }
                    
                    dt.Load(dr);
                    dr.Close();
                    con.Close();

                }
                catch (Exception exception)
                {
                    if (dr != null || con.State == ConnectionState.Open)
                    {
                        dr.Close();
                        con.Close();
                    }
                }
                return productList;
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        [ActionName("GetProductById")]
        public Product Get(int id)
        {
            Product product = new Product();
            OracleConnection con = new OracleConnection(ConnectionString);
            OracleDataReader dr = null;
            string sql = "";


            sql = "SELECT t.PRODUCT_CODE,t.PRODUCT     FROM MNBQ_PRODUCT t WHERE t.PRODUCT_CODE=:V_PRODUCT_CODE ";

            OracleCommand cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("V_PRODUCT_CODE", id));

            con.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    product.ProductId = Convert.ToInt32(dr["PRODUCT_CODE"]);
                    product.ProductName = dr["PRODUCT"].ToString();

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

            return product;
        }


        // POST api/product
        public void Post([FromBody]string value)
        {
        }

        // PUT api/product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/product/5
        public void Delete(int id)
        {
        }
    }
}
