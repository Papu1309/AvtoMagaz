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

namespace AvtoMagaz.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public AdminMainPage()
        {
            InitializeComponent();
            txtAdmin.Text = $"Администратор: {CurrentUser.Username}";
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminProductsPage());
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.Id = 0;
            CurrentUser.Username = null;
            CurrentUser.RoleId = 0;
            Cart.Clear();
            NavigationService.Navigate(new LoginPage());
        }
    }
}
