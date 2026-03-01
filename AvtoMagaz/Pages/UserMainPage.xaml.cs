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
    /// Логика взаимодействия для UserMainPage.xaml
    /// </summary>
    public partial class UserMainPage : Page
    {
        public UserMainPage()
        {
            InitializeComponent();
            txtUser.Text = $"Вы вошли как: {CurrentUser.Username} (пользователь)";
        }

        private void Catalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatalogPage());
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // Сброс текущего пользователя и очистка корзины (опционально)
            CurrentUser.Id = 0;
            CurrentUser.Username = null;
            CurrentUser.RoleId = 0;
            Cart.Clear();
            NavigationService.Navigate(new LoginPage());
        }
    }
}
