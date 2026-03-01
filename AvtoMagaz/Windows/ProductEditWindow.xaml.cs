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
using System.Windows.Shapes;

namespace AvtoMagaz.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        private Products _product;
        private bool _isNew;

        public ProductEditWindow(Products product = null)
        {
            InitializeComponent();
            if (product == null)
            {
                _product = new Products();
                _isNew = true;
                Title = "Добавление товара";
            }
            else
            {
                _product = product;
                _isNew = false;
                Title = "Редактирование товара";
                LoadData();
            }
        }

        private void LoadData()
        {
            txtName.Text = _product.Name;
            txtDescription.Text = _product.Description;
            txtPrice.Text = _product.Price.ToString();
            txtStock.Text = _product.StockQuantity.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Простейшая валидация
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Некорректная цена");
                return;
            }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Некорректное количество");
                return;
            }

            _product.Name = txtName.Text.Trim();
            _product.Description = txtDescription.Text.Trim();
            _product.Price = price;
            _product.StockQuantity = stock;

            if (_isNew)
                Connection.entities.Products.Add(_product);

            Connection.entities.SaveChanges();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
