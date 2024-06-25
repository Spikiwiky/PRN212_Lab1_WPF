using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static SqlConnection connection;
        private static SqlCommand command;
        private static string connectionString = "Server=GENTLEMEN-TUNGP\\SQLEXPRESS;UID=sa;PWD=123456789t;Database=MyStore;Encrypt=True;TrustServerCertificate=True";

        public static List<Product> GetProducts()
        {
            List<Product> listProducts = new List<Product>();
            connection = new SqlConnection(connectionString);
            string SQL = "select * from Products";
            command = new SqlCommand(SQL, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        listProducts.Add(new Product
                        {
                            ProductId = reader.GetInt32("ProductID"),
                            ProductName = reader.GetString("ProductName"),
                            CategoryId = reader.GetInt32("CategoryID"),
                            UnitsInStock = reader.GetInt16("UnitsInStock"),
                            UnitPrice = reader.GetDecimal("UnitPrice")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return listProducts;
        }

        public static void SaveProduct(Product product)
        {
            connection = new SqlConnection(connectionString);
            string SQL = "INSERT INTO Products ([ProductName],[CategoryID],[UnitsInStock],[UnitPrice])" +
                                            " VALUES (@ProductName,@CategoryID,@UnitsInStock,@UnitPrice)";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryId);
            command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void UpdateProduct(Product product)
        {
            connection = new SqlConnection(connectionString);
            string SQL = "UPDATE Products SET ProductName = @ProductName,CategoryID = @CategoryID," +
                "UnitsInStock = @UnitsInStock,UnitPrice = @UnitPrice WHERE ProductID = @ProductID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryId);
            command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@ProductID", product.ProductId);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        public static void DeleteProduct(Product product)
        {
            connection = new SqlConnection(connectionString);
            string SQL = "DELETE FROM Products WHERE ProductID = @ProductID";
            command = new SqlCommand(SQL,connection);
            command.Parameters.AddWithValue("@ProductID", product.ProductId);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static Product GetProductById(int id)
        {
            Product product = null;
            connection = new SqlConnection(connectionString);
            string SQL = "SELECT * FROM Products WHERE ProductID = @ProductID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@ProductID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = reader.GetInt32("ProductID"),
                            ProductName = reader.GetString("ProductName"),
                            CategoryId = reader.GetInt32("CategoryID"),
                            UnitsInStock = reader.GetInt16("UnitsInStock"),
                            UnitPrice = reader.GetDecimal("UnitPrice")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return product;
        }
    }
}
