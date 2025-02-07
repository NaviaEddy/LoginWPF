using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Register_Login.Helpers;

namespace Register_Login.Models
{
    internal class User
    {
        public static string CreateUser(string firstname, string lastname, string address, string phone, string email, string password)
        {
            try
            {
                MySqlConnection conn = DatabaseService.GetConnection();
                DatabaseService.Connection();

                string VQuery = "SELECT COUNT(*) FROM users WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(VQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return "M1";
                    }
                }

                string hashed_password = Hasher.HashPassword(password);

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string queryUsers = "INSERT INTO users (FIRSTNAME, LASTNAME, ADDRESS, PHONE, EMAIL) " +
                                            "VALUES (@firstname, @lastname, @address, @phone, @email);";

                        using (MySqlCommand cmdUsers = new MySqlCommand(queryUsers, conn, transaction))
                        {
                            cmdUsers.Parameters.AddWithValue("@firstname", firstname);
                            cmdUsers.Parameters.AddWithValue("@lastname", lastname);
                            cmdUsers.Parameters.AddWithValue("@address", address);
                            cmdUsers.Parameters.AddWithValue("@phone", phone);
                            cmdUsers.Parameters.AddWithValue("@email", email);

                            cmdUsers.ExecuteNonQuery();
                        }

                        string getLastIdQuery = "SELECT LAST_INSERT_ID();";
                        long userId;

                        using (MySqlCommand cmdGetLastId = new MySqlCommand(getLastIdQuery, conn, transaction))
                        {
                            userId = Convert.ToInt64(cmdGetLastId.ExecuteScalar());
                        }

                        string queryCredentials = "INSERT INTO credentials (ID_USER, PASSWORD) VALUES (@id_user, @password);";

                        using (MySqlCommand cmdCredentials = new MySqlCommand(queryCredentials, conn, transaction))
                        {
                            cmdCredentials.Parameters.AddWithValue("@id_user", userId);
                            cmdCredentials.Parameters.AddWithValue("@password", hashed_password);

                            cmdCredentials.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return "M2";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"❌ Error creating user in transaction: {ex.Message}");
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error creating user: {ex.Message}");
                return "";
            }
            finally
            {
                DatabaseService.close_connection();
            }
        }


        public static bool FindUserEmail(string email)
        {
            try
            {
                MySqlConnection conn = DatabaseService.GetConnection();
                DatabaseService.Connection();

                string query = "SELECT COUNT(*) FROM users WHERE email = @email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding user: {ex.Message}");
                return false;
            }
            finally
            {
                DatabaseService.close_connection();
            }
        }

    }
}
