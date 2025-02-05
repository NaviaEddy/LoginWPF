using Register_Login.Models;
using Register_Login.Views.Login_Form;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Register_Login.Views.Recovery_Password
{
    /// <summary>
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : UserControl
    {
        public Email()
        {
            InitializeComponent();
        }

        private void ButtonSendEmail_Click(object sender, RoutedEventArgs e)
        {
            string rec_email = EmailBoxRecovery.Text;
            if (!User.FindUserEmail(rec_email))
            {
                txtErrorMessage.Text = "Email not found.";
                txtPanelMessage.Visibility = Visibility.Visible;
                return;
            }
            Login.ChangeView(new Password(rec_email));

        }

        private void ReturnLoginClick(object sender, RoutedEventArgs e)
        {
            Login.ChangeView(new Form());
        }
    }
}
