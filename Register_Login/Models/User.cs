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

        public static void CreateUser(string firstname, string lastname, string address, string phone, string email, string password)
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
                        MessageBox.Show("The email is alredy registered.");
                        return;
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

                        MessageBox.Show("✅ User and credentials successfully created.", "Information", MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"❌ Error al crear el usuario: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error creating user: {ex.Message}");
            }
            finally
            {
                DatabaseService.close_connection();
            }
        }
    }
}
