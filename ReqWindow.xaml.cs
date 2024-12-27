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
    /// Логика взаимодействия для ReqWindow.xaml
    /// </summary>
    public partial class ReqWindow : Window
    {
        private readonly Database.RequisitionsEntities entities;
        public ObservableCollection<Database.Performer> Performer { get; set; }
        public ObservableCollection<Database.Req> Reqs { get; set; }
        public ObservableCollection<Database.StatusType> StatusType { get; set; }


        public ReqWindow(Database.RequisitionsEntities entities1, Database.User user)
        {
            InitializeComponent();
            entities = entities1;
           
            Reqs = new ObservableCollection<Database.Req>(entities.Reqs);
            Performer = new ObservableCollection<Database.Performer> (entities.Performers);
            StatusType = new ObservableCollection<Database.StatusType>(entities.StatusTypes);
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Selectedreq = lvReq.SelectedItem as Database.Req;
            if (Selectedreq != null)
            {

                ReqInfo reqinfo = new ReqInfo(this.entities, lvReq.SelectedItem as Database.Req);
                reqinfo.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Выберите заявку для получения информации");
            }

        }
    }
}
