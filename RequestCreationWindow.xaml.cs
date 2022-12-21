using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for RequestCreationWindow.xaml
    /// </summary>
    public partial class RequestCreationWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        static string SerialNumber;
        public RequestCreationWindow(TNSEntities cont, Abonent SelectedAbonent, CRMRequest NewRequest)
        {
            InitializeComponent();
            DataContext = NewRequest;
            this.cont = cont;

            cbxService.ItemsSource = cont.Service.ToList();
            cbxServiceKind.ItemsSource = cont.ServiceKind.ToList();
            cbxServiceType.ItemsSource = cont.ServiceType.ToList();
            cbxStatus.ItemsSource = cont.RequestStatus.ToList();
            cbxProblemType.ItemsSource = cont.ProblemType.ToList();

            string CurrentDate = Convert.ToString(DateTime.Today);
            CurrentDate = CurrentDate.Substring(0, 10);
            NewRequest.CreationDate = CurrentDate;
            CurrentDate = CurrentDate.Replace('.', '/');
            NewRequest.RequestNum = String.Concat(SelectedAbonent.AccountNumber, "/", CurrentDate);
            NewRequest.AccountNumber = SelectedAbonent.AccountNumber;
            NewRequest.Status = 1;
            SerialNumber = SelectedAbonent.EquipmentSerialNumber;
        }

        private void cbxServiceKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceKind SelecetedSK = cbxServiceKind.SelectedItem as ServiceKind;
            cbxServiceType.ItemsSource = cont.ServiceType.Where(type => type.ServiceKind == SelecetedSK.ID).ToList();
        }

        private void btnCreateRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cont.SaveChanges();
                this.Close();
                MessageBox.Show("Заявка успешно создана");
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введены данные", "Ошибка");
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:62727/api/Equipment/State?SerialNumber=" + SerialNumber);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string sReadData = sr.ReadToEnd();
            response.Close();

            if (Convert.ToInt32(sReadData) == 1)
            {
                MessageBox.Show("Оборудование исправно");
            }
            else
            {
                MessageBox.Show("Обнаружена неисправность");
            }

        }
    }
}
