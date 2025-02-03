using DotNetEnv;
using MySql.Data.MySqlClient;
using Register_Login.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Register_Login
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                Env.Load();
            }
            catch (Exception ex) {
                MessageBox.Show($"Error loadign .env file: {ex.Message}");
            }

            try
            {
                DatabaseService.Connection();
                System.Diagnostics.Debug.WriteLine("✅ Database connected successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the database:  {ex.Message} ");
            }
            
        }
    }

}
