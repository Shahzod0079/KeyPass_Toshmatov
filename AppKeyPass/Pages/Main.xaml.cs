using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using AppKeyPass.Context;
using AppKeyPass.Models;

namespace AppKeyPass.Pages
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            Loaded += async (s, e) => await GetStorage();
        }

        public async Task GetStorage()
        {
            List<Storage> Storages = await StorageContext.Get();

            if (Storages == null)
            {
                Storages = new List<Storage>();
            }

            if (StorageList != null)
            {
                StorageList.Children.Clear();
                foreach (Storage Storage in Storages)
                {
                    var item = new Elements.Item(Storage, this);
                    StorageList.Children.Add(item);
                }
            }
        }

        private void OpenPageAdd(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.Init.OpenPages(new Add());

        public void RefreshList()
        {
            _ = GetStorage();
        }

        private void BtnLogout(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.Token = null;
            MainWindow.Init.OpenPages(new Login());
        }
    }
    
}