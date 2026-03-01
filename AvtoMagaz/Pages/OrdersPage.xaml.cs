using AvtoMagaz.Connect;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            IQueryable<Orders> query = Connection.entities.Orders;

            if (CurrentUser.RoleId == 1) // пользователь
            {
                query = query.Where(o => o.UserId == CurrentUser.Id);
                lblTitle.Text = "Мои заказы";
            }
            else // администратор
            {
                lblTitle.Text = "Все заказы";
            }

            lvOrders.ItemsSource = query.OrderByDescending(o => o.OrderDate).ToList();
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDetails.IsEnabled = lvOrders.SelectedItem != null;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvOrders.SelectedItem as Orders;
            if (selected != null)
                NavigationService.Navigate(new OrderDetailsPage(selected.Id));
        }

        private void GoToMain_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.RoleId == 2)
                NavigationService.Navigate(new AdminMainPage());
            else
                NavigationService.Navigate(new UserMainPage());
        }
    }
}
