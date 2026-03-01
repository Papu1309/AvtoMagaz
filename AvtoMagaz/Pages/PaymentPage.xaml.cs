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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        private int _orderId;
        private decimal _amount;

        public PaymentPage(int orderId, decimal amount)
        {
            InitializeComponent();
            _orderId = orderId;
            _amount = amount;
            txtAmount.Text = amount.ToString("C");
            txtOrderInfo.Text = $"Заказ №{orderId}";
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            string method = rbCard.IsChecked == true ? "Карта" : "Наличные";

            var payment = new Payments
            {
                OrderId = _orderId,
                PaymentMethod = method,
                Amount = _amount,
                PaymentDate = DateTime.Now
            };

            Connection.entities.Payments.Add(payment);

            // Обновляем статус заказа
            var order = Connection.entities.Orders.Find(_orderId);
            if (order != null)
                order.Status = "Оплачен";

            Connection.entities.SaveChanges();

            MessageBox.Show("Оплата прошла успешно!");

            // Возврат на главную страницу пользователя
            NavigationService.Navigate(new UserMainPage());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
