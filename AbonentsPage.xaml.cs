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
    /// Interaction logic for AbonentsPage.xaml
    /// </summary>
    public partial class AbonentsPage : Page
    {
        TNSEntities cont = new TNSEntities();
        bool Actives = true;
        bool Inactives = false;
        public AbonentsPage(Employee emp)
        {
            InitializeComponent();
            string CurrentDate = Convert.ToString(DateTime.Today);
            CurrentDate = CurrentDate.Substring(0, 10);
            dtgAbonents.ItemsSource = cont.Abonent.Where(a => a.ContractTerminationDate == "").ToList();
            dtgEvents.ItemsSource = cont.Events.Where(e => e.TargetPosition == emp.Position/*&& e.Date == CurrentDate*/).ToList();
        }
        private void chbxInactive_Checked(object sender, RoutedEventArgs e)
        {
            Inactives = true;
            dtgAbonentsFilling();
        }

        private void chbxInactive_Unchecked(object sender, RoutedEventArgs e)
        {
            Inactives = false;
            dtgAbonentsFilling();
        }

        private void chbxActive_Checked(object sender, RoutedEventArgs e)
        {
            Actives = true;
            dtgAbonentsFilling();
        }

        private void chbxActive_Unchecked(object sender, RoutedEventArgs e)
        {
            Actives = false;
            dtgAbonentsFilling();
        }

        private void dtgAbonentsFilling()
        {
            if (Actives && Inactives)
            {
                dtgAbonents.ItemsSource = cont.Abonent.ToList();
            }
            else if (Actives && !Inactives)
            {
                dtgAbonents.ItemsSource = cont.Abonent.Where(a => a.ContractTerminationDate == "").ToList();
            }
            else if (!Actives && Inactives)
            {
                dtgAbonents.ItemsSource = cont.Abonent.Where(a => a.ContractTerminationDate != "").ToList();
            }
            else
            {
                dtgAbonents.ItemsSource = null;
            }
        }

        private void dtgAbonents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Abonent SelectedAbonent = dtgAbonents.SelectedItem as Abonent;
            var w = new AbonentInfoWindow(SelectedAbonent);
            w.ShowDialog();
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtgAbonents.ItemsSource = cont.Abonent.Where(a => String.Concat(a.Surname, a.Name, a.Paramonic, a.AccountNumber).Contains(tbxSearch.Text)).ToList();
        }
    }
}
