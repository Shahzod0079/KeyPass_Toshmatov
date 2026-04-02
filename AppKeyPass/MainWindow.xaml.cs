using System.Windows;
using System.Windows.Controls;
using AppKeyPass.Pages;

namespace AppKeyPass
{

    public partial class MainWindow : Window
    {
        public static MainWindow Init;
        public static string Token;

        public MainWindow()
        {
            InitializeComponent();
            Init = this;
            OpenPages(new Pages.Login());
        }

        public void OpenPages(Page openPage)
        {
            frame.Navigate(openPage);
        }
    }
}
