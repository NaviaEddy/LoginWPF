using Register_Login.Models;
using Register_Login.Views.Login_Form;
using Register_Login.Views.Recovery_Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Register_Login.Views.Login_Form
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private static Login? _instance;

        public Login()
        {
            InitializeComponent();
            _instance = this;
            CurrentView.Content = new Form();
        }

        public static void ChangeView(UserControl new_view)
        {
            if (_instance != null) {
                _instance.CurrentView.Content = new_view;
            }
        }

    }
}
