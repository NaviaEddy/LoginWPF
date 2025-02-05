using Register_Login.Models;
using Register_Login.Views.Login_Form;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Register_Login.Views.Recovery_Password
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : UserControl
    {
        string _email;

        public Password(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void Psw_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(InputPsw, TextBoxPassword, IconEyePsw, IconEyeSlashPsw);
        }

        private void ConfPsw_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(InputConfPsw, TextBoxPasswordConf, IconEyeConfPsw, IconEyeSlashConfPsw);
        }


        private void TogglePasswordVisibility(PasswordBox passwordBox, TextBox textBox, UIElement iconVisible, UIElement iconHidden)
        {
            if (passwordBox.Tag.ToString() == "Hidden")
            {
                // Cambiar a mostrar el texto
                passwordBox.Tag = "Visible";
                textBox.Text = passwordBox.Password;
                passwordBox.Visibility = Visibility.Collapsed;
                textBox.Visibility = Visibility.Visible;
                iconVisible.Visibility = Visibility.Hidden;
                iconHidden.Visibility = Visibility.Visible;
                textBox.Focus();
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
            {
                // Cambiar a ocultar el texto
                passwordBox.Tag = "Hidden";
                passwordBox.Password = textBox.Text;
                passwordBox.Visibility = Visibility.Visible;
                textBox.Visibility = Visibility.Collapsed;
                iconVisible.Visibility = Visibility.Visible;
                iconHidden.Visibility = Visibility.Hidden;
                passwordBox.Focus();
                passwordBox.GetType()
                           .GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
                           ?.Invoke(passwordBox, new object[] { passwordBox.Password.Length, 0 });
            }
        }

        private void InputPSWChanged(object sender, RoutedEventArgs e)
        {
            TogglePlaceholderVisibility(InputPsw, Placeholder_psw);
            ValidatePassword();
        }

        private void InputCnfPSWChanged(object sender, RoutedEventArgs e)
        {
            TogglePlaceholderVisibility(InputConfPsw, Placeholder_pswcnf);
            ValidatePassword();
        }

        private void TogglePlaceholderVisibility(PasswordBox passwordBox, UIElement placeholder)
        {
            placeholder.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ValidatePassword()
        {
            if (InputPsw.Password != InputConfPsw.Password)
            {
                txtErrorMessage.Text = "Passwords do not match.";
                txtPanelMessage.Visibility = Visibility.Visible;
            }
            else
            {
                txtPanelMessage.Visibility = Visibility.Collapsed;
            }
        }

        private async void ButtonResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string password = InputPsw.Password;
            //MessageBox.Show(password + _email);
            if(!Credentials.ChangePassword(_email, password))
            {
                txtErrorMessage.Text = "The password was not updated, please try again.";
                txtPanelMessage.Visibility = Visibility.Visible;
            }

            txtErrorMessage.Text = "Password updated correctly, redirecting to login...";
            txtErrorMessage.Foreground = new SolidColorBrush(Colors.Green);
            IconMessage.Icon = FontAwesome.Sharp.IconChar.CheckCircle;
            IconMessage.Foreground = new SolidColorBrush(Colors.Green);
            txtPanelMessage.Visibility = Visibility.Visible;

            await Task.Delay(2000);
            Login.ChangeView(new Form());
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Login.ChangeView(new Email());
        }
    }
}
