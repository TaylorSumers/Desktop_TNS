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
using System.Net;
using System.IO;

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for EquipmentInfo.xaml
    /// </summary>
    public partial class EquipmentInfo : Window
    {
        TNSEntities cont = new TNSEntities();
        public EquipmentInfo(int SelectedEquipment, int EquipmentType)
        {
            InitializeComponent();
            if (EquipmentType == 0)
            {
                var Eq = (from Equipment in cont.MainNetworkEquipment
                          join Technology in cont.DataTransmissionTechnology
                          on Equipment.DataTransmissionTechnology equals Technology.ID
                          where Equipment.ID == SelectedEquipment
                          select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.Frequency, Technology.TechName }).Single();
                tbName2.Text = Eq.Name;
                tbSerialNumber2.Text = Eq.SerialNumber;
                tbFrequency2.Text = Convert.ToString(Eq.Frequency);
                tbTechnology2.Text = Eq.TechName;
                tbStandart1.Visibility = Visibility.Collapsed;
                tbPortAmount1.Visibility = Visibility.Collapsed;
                tbTransmissionSpeed1.Visibility = Visibility.Collapsed;
            }
            else if (EquipmentType == 1)
            {
                var Eq = (from Equipment in cont.AccessNetworkEquipment
                          join Standart in cont.DataTransmissionStandart
                          on Equipment.DataTransmissionStandart equals Standart.ID
                          where Equipment.ID == SelectedEquipment
                          select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.Frequency, Equipment.PortAmount, Equipment.TransmissionSpeed, Standart.StandName }).Single();
                tbName2.Text = Eq.Name;
                tbSerialNumber2.Text = Eq.SerialNumber;
                tbFrequency2.Text = Convert.ToString(Eq.Frequency);
                tbTechnology1.Visibility = Visibility.Collapsed;
                tbTechnology2.Visibility = Visibility.Collapsed;
                tbStandart2.Text = Eq.StandName;
                tbPortAmount2.Text = Convert.ToString(Eq.PortAmount);
                tbTransmissionSpeed2.Text = Eq.TransmissionSpeed;
            }
            else
            {
                var Eq = (from Equipment in cont.AbonentNetworkEquipment
                          join Standart in cont.DataTransmissionStandart
                          on Equipment.DataTransmissionStandart equals Standart.ID
                          where Equipment.ID == SelectedEquipment
                          select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.TransmissionSpeed, Standart.StandName }).Single();
                tbName2.Text = Eq.Name;
                tbSerialNumber2.Text = Eq.SerialNumber;
                tbFrequency1.Visibility = Visibility.Collapsed;
                tbFrequency2.Visibility = Visibility.Collapsed;
                tbTechnology1.Visibility = Visibility.Collapsed;
                tbTechnology2.Visibility = Visibility.Collapsed;
                tbStandart2.Text = Eq.StandName;
                tbPortAmount1.Visibility = Visibility.Collapsed;
                tbPortAmount2.Visibility = Visibility.Collapsed;
                tbTransmissionSpeed2.Text = Eq.TransmissionSpeed;
            }
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:62727/api/Equipment/State?SerialNumber=" + tbSerialNumber2.Text);
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
