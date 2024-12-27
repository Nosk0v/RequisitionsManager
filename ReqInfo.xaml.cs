using RequisitionsManager.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для ReqInfo.xaml
    /// </summary>
    public partial class ReqInfo : Window
    {
        private readonly Database.RequisitionsEntities entities;
        public ObservableCollection<Database.Performer> Performers { get; set; }
        public ObservableCollection<Database.Req> Reqs { get; set; }
        public ObservableCollection<Database.StatusType> StatusTypes { get; set; }
        private readonly Database.User currentUser;
        public Database.Req Selectedreq { get; set; }
        public ReqInfo(Database.RequisitionsEntities entities ,Req selectedreq)
        {
            InitializeComponent();
            this.entities = entities;
            Reqs = new ObservableCollection<Database.Req>(entities.Reqs);
            Performers = new ObservableCollection<Database.Performer>(entities.Performers);
            StatusTypes = new ObservableCollection<Database.StatusType>(entities.StatusTypes);

            Selectedreq = selectedreq;
            tbReqName.Text = Selectedreq.ReqName;
            tbDescription.Text = Selectedreq.Description;
            dpCreate.SelectedDate = Selectedreq.CreateDate;
            dpDeadline.SelectedDate = Selectedreq.Deadline;
            cbType.ItemsSource = StatusTypes;
            cbType.SelectedValue = Selectedreq.Status;

            cbPerformer.ItemsSource = Performers;
            cbPerformer.SelectedValue = Selectedreq.PerformerReq;
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

                // Обновляем данные выбранной заявки
                Selectedreq.ReqName = tbReqName.Text;
                Selectedreq.Description = tbDescription.Text;
                Selectedreq.CreateDate = dpCreate.SelectedDate.Value;
                Selectedreq.Deadline = dpDeadline.SelectedDate.Value;

                // Обновляем статус заявки, если выбран
                var selectedStatus = cbType.SelectedItem as Database.StatusType;
                if (selectedStatus != null)
                {
                    Selectedreq.StatusType = selectedStatus;
                    Selectedreq.Status = selectedStatus.StatusID;
                }

                // Обновляем исполнителя, если выбран
                var selectedPerformer = cbPerformer.SelectedItem as Database.Performer;
                if (selectedPerformer != null)
                {
                    Selectedreq.Performer = selectedPerformer;
                    Selectedreq.PerformerReq = selectedPerformer.PerformerId;
                }

                // Сохраняем изменения в базе данных
                entities.Entry(Selectedreq).State = EntityState.Modified; // Обозначаем запись как изменённую
                entities.SaveChanges(); // Сохраняем изменения

                MessageBox.Show("Изменения сохранены успешно!");

                // Возвращаемся к предыдущему окну
                ReqWindow reqWindow = new ReqWindow(entities, currentUser);
                reqWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}");
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReqWindow reqWindow = new ReqWindow(this.entities, currentUser);
            reqWindow.Show();
            Close();
        }
    }
}
