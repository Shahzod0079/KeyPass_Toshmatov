using AppKeyPass.Models;
using System.Windows;
using System.Windows.Controls;
using AppKeyPass.Context;

namespace AppKeyPass.Pages
{
    public partial class Add : Page
    {
        Storage ChangeStorage;

        public Add(Storage storage = null)
        {
            InitializeComponent();
            ChangeStorage = storage;

            if (ChangeStorage != null)
            {
                tbName.Text = ChangeStorage.Name;
                tbUrl.Text = ChangeStorage.Url;
                tbLogin.Text = ChangeStorage.Login;
                tbPassword.Text = ChangeStorage.Password;
            }
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token: {MainWindow.Token}");

            if (ChangeStorage == null)
            {
                Storage storage = new Storage()
                {
                    Name = tbName.Text,
                    Url = tbUrl.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                };

                var result = await StorageContext.Add(storage);  // ← добавить await
                System.Diagnostics.Debug.WriteLine($"Add result: {result != null}");
            }
            else
            {
                ChangeStorage.Name = tbName.Text;
                ChangeStorage.Url = tbUrl.Text;
                ChangeStorage.Login = tbLogin.Text;
                ChangeStorage.Password = tbPassword.Text;

                var result = await StorageContext.Update(ChangeStorage);  // ← добавить await
                System.Diagnostics.Debug.WriteLine($"Update result: {result != null}");
            }

            MessageBox.Show("Данные сохранены");
            MainWindow.Init.OpenPages(new Main());
        }
        private void Back(object sender, RoutedEventArgs e) =>
            MainWindow.Init.OpenPages(new Pages.Main());
    }
}