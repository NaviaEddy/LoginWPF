using MySql.Data.MySqlClient;
using Register_Login.Helpers;
using Register_Login.Views.Recovery_Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Register_Login.Models
{
    internal class Credentials
    {
        public static bool ChangePassword(string user_email, string password)
        {
            string hash_new_password = Hasher.HashPassword(password);

            try
            {
                MySqlConnection conn = DatabaseService.GetConnection();
                DatabaseService.Connection();

                string query = @"
                UPDATE credentials 
                SET PASSWORD = @password 
                WHERE ID_USER = (SELECT ID_USER FROM users WHERE EMAIL = @email)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@password", hash_new_password);
                    cmd.Parameters.AddWithValue("@email", user_email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false; 
            }
            finally
            {
                DatabaseService.close_connection();
            }
        }

    }
}
