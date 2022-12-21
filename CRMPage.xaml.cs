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
    /// Interaction logic for CRMPage.xaml
    /// </summary>
    public partial class CRMPage : Page
    {
        TNSEntities cont = new TNSEntities();
        public CRMPage()
        {
            InitializeComponent();
        }

        private void btnCreateRequest_Click(object sender, RoutedEventArgs e)
        {
            List<Abonent> SelectedAbonent = cont.Abonent.Where(a => a.Surname == tbxSurname.Text && a.Phone == tbxPhone.Text).ToList();
            if (SelectedAbonent.Count == 1)
            {
                var NewRequest = new CRMRequest();
                cont.CRMRequest.Add(NewRequest);
                var w = new RequestCreationWindow(cont, SelectedAbonent[0], NewRequest);
                w.ShowDialog();
            }
            else
            {
                MessageBox.Show("Абонент не найден", "Ошибка");
            }
        }
    }
}
