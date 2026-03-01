using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PaymentCardWindow.xaml
    /// </summary>
    public partial class PaymentCardWindow : Window
    {
        public bool PaymentSuccess { get; private set; }

        public PaymentCardWindow()
        {
            InitializeComponent();
        }

        private void ValidateInput(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Простейшая валидация: номер карты 16 цифр, месяц 1-12, год (текущий+10), CVV 3 цифры
            bool isValid = true;

            // Номер карты
            string card = txtCardNumber.Text.Replace(" ", "").Replace("-", "");
            if (!Regex.IsMatch(card, @"^\d{16}$"))
                isValid = false;

            // Месяц
            if (!int.TryParse(txtMonth.Text, out int month) || month < 1 || month > 12)
                isValid = false;

            // Год
            int currentYear = DateTime.Now.Year % 100; // последние две цифры
            if (!int.TryParse(txtYear.Text, out int year) || year < currentYear || year > currentYear + 10)
                isValid = false;

            // CVV
            if (!Regex.IsMatch(txtCvv.Text, @"^\d{3}$"))
                isValid = false;

            btnPay.IsEnabled = isValid;
            txtError.Text = isValid ? "" : "Проверьте правильность введённых данных";
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            // Здесь можно было бы интегрироваться с платёжным шлюзом, но мы просто имитируем успех
            PaymentSuccess = true;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            PaymentSuccess = false;
            DialogResult = false;
            Close();
        }
    }
}
