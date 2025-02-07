using Register_Login.Models;
using Register_Login.Views.Home_View;
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
using System.Windows.Shapes;

namespace Register_Login.Views.Register_Form
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
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

        private void SignInClick(object sender, MouseButtonEventArgs e) { 
            Login login = new Login();
            login.Show();
            Window.GetWindow(this)?.Close();
        }

        private async void ButtonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string firstname = InputFirstName.Text;
            string lastname = InputLastName.Text;
            string address = InputAddress.Text;
            string phone = InputPhone.Text;
            string email = InputEmail.Text;
            string password = InputPsw.Password;

            try
            {
                string message = User.CreateUser(firstname, lastname, address, phone, email, password);

                switch (message)
                {
                    case "M1":
                        ShowMessage("The email is already registered.", Colors.OrangeRed, FontAwesome.Sharp.IconChar.ExclamationTriangle);
                        break;

                    case "M2":
                        ShowMessage("User and credentials successfully created, Logging in...", Colors.Green, FontAwesome.Sharp.IconChar.CheckCircle);
                        await Task.Delay(2000);
                        new Home().Show();
                        Window.GetWindow(this)?.Close();
                        break;

                    default:
                        ShowMessage("There was an error, please try again.", Colors.Red, FontAwesome.Sharp.IconChar.TimesCircle);
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"An unexpected error occurred in register: {ex.Message}");
                ShowMessage("There was an error, please try again.", Colors.Red, FontAwesome.Sharp.IconChar.TimesCircle);
            }


        }

        private void ShowMessage(string text, Color color, FontAwesome.Sharp.IconChar icon)
        {
            txtErrorMessage.Text = text;
            txtErrorMessage.Foreground = new SolidColorBrush(color);
            IconMessage.Icon = icon;
            IconMessage.Foreground = new SolidColorBrush(color);
            txtPanelMessage.Visibility = Visibility;
        }

    }   
}
