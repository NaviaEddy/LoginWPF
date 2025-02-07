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
using Register_Login.Views;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Register_Login.Views.Recovery_Password;
using Register_Login.Views.Register_Form;
using Register_Login.Views.Home_View;
using Register_Login.Services;

namespace Register_Login.Views.Login_Form
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class Form : UserControl
    {
        public Form()
        {
            InitializeComponent();
        }

        private void TogglePassword_Click(object sender, MouseButtonEventArgs e)
        {

            if (PasswordBox.Tag.ToString() == "Hidden")
            {
                // Cambiar a mostrar el texto
                PasswordBox.Tag = "Visible";
                TextBoxPassword.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                TextBoxPassword.Visibility = Visibility.Visible;
                IconEye.Visibility = Visibility.Hidden;
                IconEyeSlash.Visibility = Visibility.Visible;
                TextBoxPassword.Focus();
                TextBoxPassword.CaretIndex = TextBoxPassword.Text.Length;
            }
            else
            {
                // Cambiar a ocultar el texto
                PasswordBox.Tag = "Hidden";
                PasswordBox.Password = TextBoxPassword.Text;
                PasswordBox.Visibility = Visibility.Visible;
                TextBoxPassword.Visibility = Visibility.Collapsed;
                IconEye.Visibility = Visibility.Visible;
                IconEyeSlash.Visibility = Visibility.Hidden;
                PasswordBox.Focus();
                PasswordBox.GetType()
                            .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                            ?.Invoke(PasswordBox, new object[] { PasswordBox.Password.Length, 0 });

            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Placeholder_password.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {

            Register register = new Register();
            register.Show();
            Window.GetWindow(this)?.Close();
        }

        private void ForgotPasswordClick(object sender, MouseButtonEventArgs e)
        {
            Login.ChangeView(new Email()); 
        }

        private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text;
            string password = PasswordBox.Password;
            if (!Auth.VerifyCredentials(email, password))
            {
                txtErrorMessage.Text = "Invalid access. Please try again.";
                txtPanelMessage.Visibility = Visibility.Visible;
                return;
            }

            txtErrorMessage.Text = "Access granted. Logging in...";
            txtErrorMessage.Foreground = new SolidColorBrush(Colors.Green);
            IconMessage.Icon = FontAwesome.Sharp.IconChar.CheckCircle;
            IconMessage.Foreground = new SolidColorBrush(Colors.Green);
            txtPanelMessage.Visibility = Visibility.Visible;

            await Task.Delay(2000);
            Home home = new Home();
            home.Show();
            Window.GetWindow(this)?.Close();
        }
    }
}
