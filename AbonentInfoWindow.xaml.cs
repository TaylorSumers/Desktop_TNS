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
    /// Interaction logic for AbonentInfoWindow.xaml
    /// </summary>
    public partial class AbonentInfoWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        public AbonentInfoWindow(Abonent ab)
        {
            InitializeComponent();

            tbNumber.Text = ab.Number;
            tbSurname.Text = ab.Surname;
            tbName.Text = ab.Name;
            tbParamonic.Text = ab.Paramonic;
            tbPassportSeries.Text = ab.PassportSeries;
            tbPassportNumber.Text = ab.PassportNumber;
            tbPassportIssueDate.Text = ab.PassportIssueDate;
            tbAccountNumber.Text = ab.AccountNumber;
            tbContractNumber.Text = ab.ContractNumber;
            tbContractConclusionDate.Text = ab.ContractConclusionDate;
            ContractType ct = cont.ContractType.Single(c => c.ID == ab.ContractType);
            tbContractType.Text = ct.Name;
            tbContractTerminationDate.Text = ab.ContractTerminationDate;
            tbContractTerminationReason.Text = ab.ContractTerminationReason;
            AbonentNetworkEquipment ne = cont.AbonentNetworkEquipment.Single(e => e.SerialNumber == ab.EquipmentSerialNumber);
            tbSerialNumber.Text = ab.EquipmentSerialNumber;
            tbEquipmentName.Text = ne.Name;
            tbRentLeasing.Text = ab.RentLeasingInfo;

            var ServicesList = (from Abonent_Service in cont.Abonent_Service
                        join Service in cont.Service
                        on Abonent_Service.asService equals Service.ServiceID
                        where Abonent_Service.asAbonent == ab.ID
                        select new { Service.ServiceName }).ToList();
            foreach (var Service in ServicesList)
            {
                tbServices.Text += $"{Service.ServiceName}, ";
            }
        }
    }
}
