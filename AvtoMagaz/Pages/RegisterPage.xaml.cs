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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != txtConfirm.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (Connection.entities.Users.Any(u => u.Username == txtLogin.Text))
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
                return;
            }

            // Роль по умолчанию - Пользователь (Id = 1)
            var newUser = new Users
            {
                Username = txtLogin.Text,
                Password = txtPassword.Password,
                RoleId = 1
            };

            Connection.entities.Users.Add(newUser);
            Connection.entities.SaveChanges();

            MessageBox.Show("Регистрация успешна! Войдите в систему.");
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
