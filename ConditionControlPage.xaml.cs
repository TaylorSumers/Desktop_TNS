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
using System.IO;
using System.Net;

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for ConditionControlPage.xaml
    /// </summary>

    public partial class ConditionControlPage : Page
    {

        TNSEntities cont = new TNSEntities();
        static int SelectedEquipment;
        static int EquipmentType;
        public ConditionControlPage()
        {
            InitializeComponent();
        }

        private void MainNE_Selected(object sender, RoutedEventArgs e)
        {
            dtgEquipment.Columns.Clear();
            var EquipmentList = (from Equipment in cont.MainNetworkEquipment
                                 join Technology in cont.DataTransmissionTechnology
                                 on Equipment.DataTransmissionTechnology equals Technology.ID
                                 select new {Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.Frequency, Technology.TechName}).ToList();
            dtgEquipment.ItemsSource = EquipmentList;
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Серийный номер",
                Binding = new Binding("SerialNumber")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Частота",
                Binding = new Binding("Frequency")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Технология передачи данных",
                Binding = new Binding("TechName")
            });
        }

        private void AccessNE_Selected(object sender, RoutedEventArgs e)
        {
            dtgEquipment.Columns.Clear();
            var EquipmentList = (from Equipment in cont.AccessNetworkEquipment
                                 join Standart in cont.DataTransmissionStandart
                                 on Equipment.DataTransmissionStandart equals Standart.ID
                                 select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.Frequency, Equipment.PortAmount, Equipment.TransmissionSpeed, Standart.StandName }).ToList();
            dtgEquipment.ItemsSource = EquipmentList;
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Серийный номер",
                Binding = new Binding("SerialNumber")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Частота",
                Binding = new Binding("Frequency")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Стандарт передачи данных",
                Binding = new Binding("StandName")
            });
        }

        private void AbonentNE_Selected(object sender, RoutedEventArgs e)
        {
            dtgEquipment.Columns.Clear();
            var EquipmentList = (from Equipment in cont.AbonentNetworkEquipment
                                 join Standart in cont.DataTransmissionStandart
                                 on Equipment.DataTransmissionStandart equals Standart.ID
                                 select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.TransmissionSpeed, Standart.StandName }).ToList();
            dtgEquipment.ItemsSource = EquipmentList;
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Серийный номер",
                Binding = new Binding("SerialNumber")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Название",
                Binding = new Binding("Name")
            });
            dtgEquipment.Columns.Add(new DataGridTextColumn
            {
                Header = "Стандарт передачи данных",
                Binding = new Binding("StandName")
            });
        }

        private void dtgEquipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedEquipment = Convert.ToInt32(dtgEquipment.SelectedValue);
            EquipmentType = cbxSelectEquipment.SelectedIndex;
            var w = new EquipmentInfo(SelectedEquipment, EquipmentType);
            w.ShowDialog();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            SelectedEquipment = Convert.ToInt32(dtgEquipment.SelectedValue);
            EquipmentType = cbxSelectEquipment.SelectedIndex;
            string SerialNumber;
            if (EquipmentType == 0)
            {
                SerialNumber = cont.MainNetworkEquipment.Where(Eq => Eq.ID == SelectedEquipment).Single().SerialNumber;
            }
            else if (EquipmentType == 1)
            {
                SerialNumber = cont.AccessNetworkEquipment.Where(Eq => Eq.ID == SelectedEquipment).Single().SerialNumber;
            }
            else
            {
                SerialNumber = cont.AbonentNetworkEquipment.Where(Eq => Eq.ID == SelectedEquipment).Single().SerialNumber;
            }



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
