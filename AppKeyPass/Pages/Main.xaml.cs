using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls;
using AppKeyPass.Models;



namespace AppKeyPass.Pages
{

    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            GetStorage;
        }

        public async Task GetStorage()
        {
            List<Storage> Storages = await StorageContext.Get();
            StorageList.Children.Clear();
            foreach(Storage Storage in Storages)
            {
                StorageList.Children.Add(new Elements.Item(Storage, this));
            }    
        }
        private void OpenPageAdd(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.Init.OpenPages(new Pages.Add());
    }
}
