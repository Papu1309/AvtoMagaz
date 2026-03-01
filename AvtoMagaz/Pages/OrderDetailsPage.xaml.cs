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
    /// Логика взаимодействия для OrderDetailsPage.xaml
    /// </summary>
    public partial class OrderDetailsPage : Page
    {
        private int _orderId;

        public OrderDetailsPage(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadOrder();
        }

        private void LoadOrder()
        {
            var order = Connection.entities.Orders.Find(_orderId);
            if (order == null) return;

            txtOrderInfo.Text = $"Заказ №{order.Id} от {order.OrderDate:d} (статус: {order.Status})";

            // Загрузка позиций
            var items = Connection.entities.OrderItems
                .Where(oi => oi.OrderId == _orderId)
                .Select(oi => new
                {
                    oi.Products.Name,
                    oi.Quantity,
                    oi.Price,
                    Sum = oi.Quantity * oi.Price
                }).ToList();
            lvItems.ItemsSource = items;

            // Загрузка платежей
            var payments = Connection.entities.Payments
                .Where(p => p.OrderId == _orderId)
                .Select(p => new
                {
                    p.PaymentMethod,
                    p.Amount,
                    p.PaymentDate
                }).ToList();
            lvPayments.ItemsSource = payments;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
