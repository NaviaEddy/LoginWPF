using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DotNetEnv;
using MySql.Data.MySqlClient;

namespace Register_Login.Models
{
    class DatabaseService
    {
        private static MySqlConnection? _conn;
        private static string host = Env.GetString("WPF_DB_HOST");
        private static string port = Env.GetString("WPF_DB_PORT");
        private static string dbName = Env.GetString("WPF_DB_NAME");
        private static string user = Env.GetString("WPF_DB_USER");
        private static string pwd = Env.GetString("WPF_DB_PSWD");

        private static string credentials = $"Server={host};Port={port};Database={dbName};User Id={user};Password={pwd};";

        private DatabaseService() { }

        public static MySqlConnection GetConnection()
        {
            if(_conn == null)
            {
                _conn = new MySqlConnection(credentials);
            }

            return _conn;
        }

        public static void Connection()
        {
            if (_conn == null) { 
                GetConnection();
            }

            if(_conn?.State != System.Data.ConnectionState.Open)
            {
                _conn?.Open();
            }
        }

        public static void close_connection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }
    }
}
