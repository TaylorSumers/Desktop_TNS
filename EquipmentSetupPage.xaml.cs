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

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for EquipmentSetupPage.xaml
    /// </summary>
    public partial class EquipmentSetupPage : Page
    {
        TNSEntities cont = new TNSEntities();
        public EquipmentSetupPage()
        {
            InitializeComponent();
        }

        private void MainNE_Selected(object sender, RoutedEventArgs e)
        {
            dtgEquipment.Columns.Clear();
            var EquipmentList = (from Equipment in cont.MainNetworkEquipment
                                 join Technology in cont.DataTransmissionTechnology
                                 on Equipment.DataTransmissionTechnology equals Technology.ID
                                 select new { Equipment.ID, Equipment.Name, Equipment.SerialNumber, Equipment.Frequency, Technology.TechName }).ToList();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int EquipmentType = cbxSelectEquipment.SelectedIndex;
            if (EquipmentType == 0)
            {
                var NewEq = new MainNetworkEquipment();
                cont.MainNetworkEquipment.Add(NewEq);
                var w = new EquipmentSetupWindow(NewEq, cont);
                w.ShowDialog();
            }
            else if (EquipmentType == 1)
            {
                var NewEq = new AccessNetworkEquipment();
                cont.AccessNetworkEquipment.Add(NewEq);
                var w = new EquipmentSetupWindow(NewEq, cont);
                w.ShowDialog();
            }
            else
            {
                var NewEq = new AbonentNetworkEquipment();
                cont.AbonentNetworkEquipment.Add(NewEq);
                var w = new EquipmentSetupWindow(NewEq, cont);
                w.ShowDialog();
            }
        }

        private void btnSetup_Click(object sender, RoutedEventArgs e)
        {
            int SelectedEquipment = Convert.ToInt32(dtgEquipment.SelectedValue);
            int EquipmentType = cbxSelectEquipment.SelectedIndex;
            if (EquipmentType == 0)
            {
                var Eq = cont.MainNetworkEquipment.Single(eq => eq.ID == SelectedEquipment);
                var w = new EquipmentSetupWindow(Eq, cont);
                w.ShowDialog();
            }
            else if (EquipmentType == 1)
            {
                var Eq = cont.AccessNetworkEquipment.Single(eq => eq.ID == SelectedEquipment);
                var w = new EquipmentSetupWindow(Eq, cont);
                w.ShowDialog();
            }
            else
            {
                var Eq = cont.AbonentNetworkEquipment.Single(eq => eq.ID == SelectedEquipment);
                var w = new EquipmentSetupWindow(Eq, cont);
                w.ShowDialog();
            }
        }
    }
}
