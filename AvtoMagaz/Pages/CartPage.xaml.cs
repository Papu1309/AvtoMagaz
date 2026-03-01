using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public class CartItemViewModel : CartItem
        {
            public decimal Total => Price * Quantity;
        }

        private ObservableCollection<CartItemViewModel> cartItems;

        public CartPage()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            cartItems = new ObservableCollection<CartItemViewModel>(
                Cart.Items.Select(i => new CartItemViewModel
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }));
            lvCart.ItemsSource = cartItems;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            txtTotal.Text = $"Итого: {Cart.TotalAmount():C}";
        }

        private void UpdateQty_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int productId = (int)btn.Tag;

            StackPanel panel = btn.Parent as StackPanel;
            TextBox txtQty = panel?.Children[0] as TextBox;

            if (txtQty != null && int.TryParse(txtQty.Text, out int qty) && qty > 0)
            {
                Cart.UpdateQuantity(productId, qty);
                LoadCart(); // перезагружаем для обновления сумм
            }
            else
            {
                MessageBox.Show("Введите корректное количество.");
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int productId = (int)btn.Tag;
            Cart.RemoveItem(productId);
            LoadCart();
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.Items.Count == 0)
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }
            NavigationService.Navigate(new CheckoutPage());
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatalogPage());
        }
    }
}
