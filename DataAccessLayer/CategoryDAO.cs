using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        private static SqlConnection connection;
        private static SqlCommand command;
        private static string connectionString = "Server=GENTLEMEN-TUNGP\\SQLEXPRESS;UID=sa;PWD=123456789t;Database=MyStore;Encrypt=True;TrustServerCertificate=True";

        public static List<Category> GetCategories()
        {
            List<Category> listCategories = new List<Category>();
            connection = new SqlConnection(connectionString);
            string SQL = "select * from Categories";
            command = new SqlCommand(SQL, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if(reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        listCategories.Add(new Category
                        {
                            CategoryId = reader.GetInt32("CategoryID"),
                            CategoryName = reader.GetString("CategoryName")
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
            return listCategories;
        }
    }
}
