﻿using FontAwesome.WPF;
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
using System.Windows.Shapes;

namespace Register_Login.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
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
                PasswordBox.Focus();
                PasswordBox.TabIndex = TextBoxPassword.Text.Length;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Placeholder_password.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }



        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Registro en construccion, esta rico!!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ForgotPasswordClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Recuperar contraseña en construccion, RICO!!!!!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ingresaras al crud!", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TogglePassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
