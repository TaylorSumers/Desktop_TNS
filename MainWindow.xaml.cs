using System;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        public MainWindow()
        {
            InitializeComponent();
            cbxUser.ItemsSource = cont.Employee.ToList();
        }


        private void CRMMenuItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new CRMPage();
        }

        private void AbonentsMenuItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new AbonentsPage(cbxUser.SelectedItem as Employee);
        }

        private void ECMenuItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new EquipmentControlPage();
        }

        private void cbxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AbonentsMenuItem.IsEnabled = true;
            AbonentsMenuItem.IsSelected = true;
            int UserPos = Convert.ToInt32(cbxUser.SelectedValue);
            MainFrame.Content = new AbonentsPage(cbxUser.SelectedItem as Employee);
            if (UserPos == 1)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Collapsed;
                AssetsMenuItem.Visibility = Visibility.Collapsed;
                BillingMenuItem.Visibility = Visibility.Visible;
                USMenuItem.Visibility = Visibility.Collapsed;
                CRMMenuItem.Visibility = Visibility.Visible;
            }
            else if (UserPos == 2)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Collapsed;
                AssetsMenuItem.Visibility = Visibility.Collapsed;
                BillingMenuItem.Visibility = Visibility.Collapsed;
                USMenuItem.Visibility = Visibility.Collapsed;
                CRMMenuItem.Visibility = Visibility.Visible;
            }
            else if (UserPos == 3 || UserPos == 4)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Visible;
                AssetsMenuItem.Visibility = Visibility.Collapsed;
                BillingMenuItem.Visibility = Visibility.Collapsed;
                USMenuItem.Visibility = Visibility.Visible;
                CRMMenuItem.Visibility = Visibility.Visible;
            }
            else if (UserPos == 5)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Collapsed;
                AssetsMenuItem.Visibility = Visibility.Visible;
                BillingMenuItem.Visibility = Visibility.Visible;
                USMenuItem.Visibility = Visibility.Collapsed;
                CRMMenuItem.Visibility = Visibility.Collapsed;
            }
            else if (UserPos == 6)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Visible;
                AssetsMenuItem.Visibility = Visibility.Visible;
                BillingMenuItem.Visibility = Visibility.Visible;
                USMenuItem.Visibility = Visibility.Visible;
                CRMMenuItem.Visibility = Visibility.Visible;
            }
            else if (UserPos == 7)
            {
                AvatarInsertion();
                ECMenuItem.Visibility = Visibility.Visible;
                AssetsMenuItem.Visibility = Visibility.Visible;
                BillingMenuItem.Visibility = Visibility.Collapsed;
                USMenuItem.Visibility = Visibility.Collapsed;
                CRMMenuItem.Visibility = Visibility.Visible;
            }
        }

        private void AvatarInsertion()
        {
            Employee emp = cbxUser.SelectedItem as Employee;
            byte[] pic = emp.Photo;
            MemoryStream ms = new MemoryStream(pic);
            var newpic = new BitmapImage();
            newpic.BeginInit();
            newpic.StreamSource = ms;
            newpic.EndInit();
            Avatar.Source = newpic;
        }

        private void USMenuItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new UserSupportPage();
        }

        private void BillingMenuItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Content = new BillingPage();
        }
    }
}
