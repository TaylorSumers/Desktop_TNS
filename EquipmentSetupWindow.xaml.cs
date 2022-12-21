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

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for EquipmentSetupWindow.xaml
    /// </summary>
    public partial class EquipmentSetupWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        public EquipmentSetupWindow(MainNetworkEquipment SelectedEquipment, TNSEntities cont)
        {
            InitializeComponent();
            DataContext = SelectedEquipment;
            this.cont = cont;

            cbxTechnology.ItemsSource = cont.DataTransmissionTechnology.ToList();
            tbStandart.Visibility = Visibility.Collapsed;
            cbxStandart.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(cbxStandart);
            tbPortAmount.Visibility = Visibility.Collapsed;
            tbxPortAmount.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(tbxPortAmount);
            tbTransmissionSpeed.Visibility = Visibility.Collapsed;
            tbxTransmissionSpeed.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(tbxTransmissionSpeed);
        }

        public EquipmentSetupWindow(AccessNetworkEquipment SelectedEquipment, TNSEntities cont)
        {
            InitializeComponent();
            DataContext = SelectedEquipment;
            this.cont = cont;

            cbxStandart.ItemsSource = cont.DataTransmissionStandart.ToList();
            tbTechnology.Visibility = Visibility.Collapsed;
            cbxTechnology.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(cbxTechnology);
        }

        public EquipmentSetupWindow(AbonentNetworkEquipment SelectedEquipment, TNSEntities cont)
        {
            InitializeComponent();
            DataContext = SelectedEquipment;
            this.cont = cont;

            cbxStandart.ItemsSource = cont.DataTransmissionStandart.ToList();
            tbFrequency.Visibility = Visibility.Collapsed;
            tbxFrequency.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(tbxFrequency);
            tbTechnology.Visibility = Visibility.Collapsed;
            cbxTechnology.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(cbxTechnology);
            tbPortAmount.Visibility = Visibility.Collapsed;
            tbxPortAmount.Visibility = Visibility.Collapsed;
            BindingOperations.ClearAllBindings(tbxPortAmount);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cont.SaveChanges();
                this.Close();
                MessageBox.Show("Данные успешно сохранены");
            }
            catch (Exception)
            {
                MessageBox.Show("Неверно введены данные", "Ошибка");
            }
        }
    }
}
