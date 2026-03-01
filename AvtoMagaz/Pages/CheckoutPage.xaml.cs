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
    /// Логика взаимодействия для CheckoutPage.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {
        public CheckoutPage()
        {
            InitializeComponent();
            LoadCartPreview();
        }

        private void LoadCartPreview()
        {
            var items = Cart.Items.Select(i => new
            {
                i.ProductName,
                i.Quantity,
                i.Price,
                Total = i.Price * i.Quantity
            }).ToList();
            lvOrderItems.ItemsSource = items;
            txtTotal.Text = $"Итого: {Cart.TotalAmount():C}";
        }

        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }

            // Создаём заказ
            var order = new Orders
            {
                UserId = CurrentUser.Id,
                OrderDate = DateTime.Now,
                TotalAmount = Cart.TotalAmount(),
                Status = "Новый"
            };
            Connection.entities.Orders.Add(order);
            Connection.entities.SaveChanges(); // чтобы получить Id заказа

            // Добавляем позиции
            foreach (var item in Cart.Items)
            {
                var orderItem = new OrderItems
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                Connection.entities.OrderItems.Add(orderItem);

                // Уменьшаем остаток на складе (необязательно, но логично)
                var product = Connection.entities.Products.Find(item.ProductId);
                if (product != null)
                    product.StockQuantity -= item.Quantity;
            }
            Connection.entities.SaveChanges();

            // Очищаем корзину
            Cart.Clear();

            // Переходим к оплате
            NavigationService.Navigate(new PaymentPage(order.Id, order.TotalAmount));
        }

        private void BackToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
