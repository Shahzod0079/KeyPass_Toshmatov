using System.Windows;
using System.Windows.Controls;
using AppKeyPass.Context;

namespace AppKeyPass.Pages
{
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void BtnRegister(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
            if (tbPassword.Password != tbConfirmPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            string token = await UserContext.Register(tbLogin.Text, tbPassword.Password);
            if (token != null)
            {
                MainWindow.Token = token;
                MainWindow.Init.OpenPages(new Main());
            }
        }

        private void BtnBack(object sender, RoutedEventArgs e)
        {
            MainWindow.Init.OpenPages(new Login());
        }
    }
}