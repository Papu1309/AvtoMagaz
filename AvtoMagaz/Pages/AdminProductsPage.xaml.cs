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
using AvtoMagaz.Windows;

namespace AvtoMagaz.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminProductsPage.xaml
    /// </summary>
    public partial class AdminProductsPage : Page
    {
        public AdminProductsPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            lvProducts.ItemsSource = Connection.entities.Products.ToList();
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // можно ничего не делать
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалог добавления (можно использовать отдельное окно или эту же страницу с формой)
            var dialog = new ProductEditWindow(); // предположим, что создадим отдельное окно
            if (dialog.ShowDialog() == true)
                LoadProducts();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvProducts.SelectedItem as Products;
            if (selected == null)
            {
                MessageBox.Show("Выберите товар для редактирования.");
                return;
            }
            var dialog = new ProductEditWindow(selected);
            if (dialog.ShowDialog() == true)
                LoadProducts();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvProducts.SelectedItem as Products;
            if (selected == null)
            {
                MessageBox.Show("Выберите товар для удаления.");
                return;
            }

            if (MessageBox.Show($"Удалить товар \"{selected.Name}\"?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Connection.entities.Products.Remove(selected);
                Connection.entities.SaveChanges();
                LoadProducts();
            }
        }

        private void GoToMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminMainPage());
        }
    }
}
