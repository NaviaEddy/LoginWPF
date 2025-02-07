using MySql.Data.MySqlClient;
using Register_Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Register_Login.Helpers;

namespace Register_Login.Services
{
    class Auth
    {
        public static bool VerifyCredentials(string user_email, string password)
        {
            try
            {

                MySqlConnection conn = DatabaseService.GetConnection();
                DatabaseService.Connection();

                string query = "SELECT u.ID_USER, c.PASSWORD FROM users u " +
                                   "JOIN credentials c ON u.ID_USER = c.ID_USER " +
                                   "WHERE u.EMAIL = @user_email";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user_email", user_email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                        {
                            string storedPasswordHash = reader.GetString("PASSWORD");
                            return Hasher.VerifyPassword(password, storedPasswordHash);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                DatabaseService.close_connection();
            }

            return false;
        }
    }
}
