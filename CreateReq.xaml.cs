using RequisitionsManager.Database;
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
using System.Windows.Shapes;

namespace RequisitionsManager
{
    /// <summary>
    /// Логика взаимодействия для CreateReq.xaml
    /// </summary>
    public partial class CreateReq : Window
    {
        private readonly Database.RequisitionsEntities entities;
        public ObservableCollection<Database.Performer> Performers { get; set; }
        public CreateReq(Database.RequisitionsEntities entities)
        {
            InitializeComponent();
            
            this.entities = entities;
            

            Performers = new ObservableCollection<Database.Performer>(entities.Performers);

            cbPerformer.ItemsSource = Performers;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем обязательные поля
                if (string.IsNullOrWhiteSpace(tbReqName.Text) ||
                    string.IsNullOrWhiteSpace(tbDescription.Text) ||
                    !dpCreate.SelectedDate.HasValue ||
                    !dpDeadline.SelectedDate.HasValue)
                {
                    MessageBox.Show("Заполните все обязательные поля.");
                    return;
                }

                // Создаем новую заявку
                var newReq = new Database.Req
                {
                    ReqName = tbReqName.Text,
                    Description = tbDescription.Text,
                    CreateDate = dpCreate.SelectedDate.Value,
                    Deadline = dpDeadline.SelectedDate.Value,
                    Status = 1 // Устанавливаем статус по умолчанию
                };

                // Добавляем исполнителя, если выбран
                var selectedPerformer = cbPerformer.SelectedItem as Database.Performer;
                if (selectedPerformer != null)
                {
                    newReq.Performer = selectedPerformer;
                    newReq.PerformerReq = selectedPerformer.PerformerId;
                }

                // Добавляем заявку в базу данных
                entities.Reqs.Add(newReq);
                entities.SaveChanges();

                MessageBox.Show("Заявка успешно создана!");

                // Возвращаемся в предыдущее окно

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}

