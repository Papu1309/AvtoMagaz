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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            lvProducts.ItemsSource = Connection.entities.Products.ToList();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int productId = (int)btn.Tag;

            // Находим TextBox в том же контейнере
            StackPanel panel = btn.Parent as StackPanel;
            TextBox txtQuantity = panel?.Children[0] as TextBox;

            if (txtQuantity != null && int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                var product = Connection.entities.Products.Find(productId);
                if (product != null)
                {
                    Cart.AddItem(product.Id, product.Name, product.Price, quantity);
                    MessageBox.Show($"Товар \"{product.Name}\" добавлен в корзину.");
                }
            }
            else
            {
                MessageBox.Show("Введите корректное количество.");
            }
        }

        private void GoToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
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
