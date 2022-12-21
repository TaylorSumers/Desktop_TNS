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
    /// Логика взаимодействия для BillingPage.xaml
    /// </summary>
    public partial class BillingPage : Page
    {
        TNSEntities cont = new TNSEntities();
        public BillingPage()
        {
            InitializeComponent();
            tbxDate.Text = Convert.ToString(DateTime.Now).Substring(0, 10);
        }

        private void tbxDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DateTime SelectedDate = Convert.ToDateTime(tbxDate.Text);
                var PaymentsList = (from Abonent in cont.Abonent
                                        join Payment in cont.AbonentPayment on Abonent.ID equals Payment.Abonent
                                        join Abonent_Tariff in cont.Abonent_Tariff on Abonent.ID equals Abonent_Tariff.Abonent
                                        where Payment.PaymentDate < SelectedDate
                                        join Tariff in cont.Tariff on Abonent_Tariff.Tariff equals Tariff.TariffID
                                        orderby Payment.PaymentDate
                                        select new { Abonent.ID, Abonent.AccountNumber, Tariff.TariffName, Tariff.TariffPrice, Payment.Debt, Payment.PaymentDate }).ToList();
                List<dynamic> todel = new List<dynamic>();
                foreach (var item in PaymentsList)
                {
                    if (PaymentsList.Where(p=>p.ID == item.ID && p.PaymentDate>=item.PaymentDate).Count()>=2)
                    {
                        todel.Add(item);
                    }
                }
                PaymentsList.RemoveAll(item=>todel.Contains(item) || item.Debt == null);
                dtgDebts.ItemsSource = PaymentsList;
            }
            catch
            { }

        }

        private void dtgDebts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int SelectedAbonent = (int)dtgDebts.SelectedValue;
                dtgPaymentHistory.ItemsSource = (from Abonent in cont.Abonent
                                                 join Payment in cont.AbonentPayment on Abonent.ID equals Payment.Abonent
                                                 where Abonent.ID == SelectedAbonent
                                                 orderby Payment.PaymentDate
                                                 select new { Abonent.ID, Payment.PaymentDate, Payment.PaymentAmount, Payment.Balance, Payment.Debt }).ToList();
            }
            catch (NullReferenceException)
            { }
        }
    }
    
}
