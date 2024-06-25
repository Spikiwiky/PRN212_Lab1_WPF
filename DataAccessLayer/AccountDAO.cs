using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        private static SqlConnection connection;
        private static SqlCommand command;
        private static string connectionString = "Server=GENTLEMEN-TUNGP\\SQLEXPRESS;UID=sa;PWD=123456789t;Database=MyStore;Encrypt=True;TrustServerCertificate=True";
        public static AccountMember GetAccountById(string accountID)
        {
            AccountMember member = null;
            connection = new SqlConnection(connectionString);
            string SQL = "SELECT * FROM AccountMember WHERE MemberID = @MemberID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@MemberID", accountID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        member = new AccountMember
                        {
                            MemberId = reader.GetString("MemberID"),
                            MemberPassword = reader.GetString("MemberPassword"),
                            FullName = reader.GetString("FullName"),
                            EmailAddress = reader.GetString("EmailAddress"),
                            MemberRole = reader.GetInt32("MemberRole")
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

            return member;
        }
    }
}
