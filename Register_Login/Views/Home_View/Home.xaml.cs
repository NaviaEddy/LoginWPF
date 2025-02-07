using MySql.Data.MySqlClient;
using Register_Login.Models;
using Register_Login.Views.Login_Form;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Register_Login.Views.Home_View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        public Home()
        {
            InitializeComponent();
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            try
            {
                MySqlConnection conn = DatabaseService.GetConnection();
                DatabaseService.Connection();
                DataTable dt1 = new DataTable();
                string query = "SELECT FIRSTNAME, LASTNAME, ADDRESS, PHONE, EMAIL FROM users";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt1);
                }

                dt1.Columns.Add("Index", typeof(int));
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dt1.Rows[i]["Index"] = i + 1;
                }

                userData.ItemsSource = dt1.DefaultView;
                //System.Diagnostics.Debug.WriteLine("Filas: " + userData.Items.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Information", MessageBoxButton.OK);
            }
            finally {
                DatabaseService.close_connection();
            }
        }

        private void SignOutButton(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Window.GetWindow(this)?.Close();
        }

    }
}
