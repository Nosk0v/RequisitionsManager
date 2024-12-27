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

namespace RequisitionsManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Database.RequisitionsEntities entities;
        private Database.User user;

        public MainWindow()
        {
            InitializeComponent();
            this.entities = new Database.RequisitionsEntities();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();
            if (login.Length < 1 || password.Length < 1)
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }
            user = entities.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            switch (user.UserRole)
            {
                case 1: // Bcgjkm
                    break;
                case 2: // Менеджер
                    break;
                case 3: // Администратор

                    ReqWindow reqWindow = new ReqWindow(entities, user);
                    reqWindow.Owner = this;
                    reqWindow.Show();
                    Hide();
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateReq createReq = new CreateReq(entities);
            createReq.Owner = this;
            createReq.Show();
            Hide();
        }
    }
}
