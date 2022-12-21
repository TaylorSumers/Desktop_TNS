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
    /// Interaction logic for UserSupportPage.xaml
    /// </summary>
    public partial class UserSupportPage : Page
    {
        TNSEntities cont = new TNSEntities();
        public UserSupportPage()
        {
            InitializeComponent();
            cbxUser.ItemsSource = cont.Abonent.ToList();
            dtgRequests.ItemsSource = (from CRMRequest in cont.CRMRequest
                                      join Service in cont.Service on CRMRequest.Service equals Service.ServiceID
                                      join ProblemType in cont.ProblemType on CRMRequest.ProblemType equals ProblemType.ProblemTypeID
                                      join Status in cont.RequestStatus on CRMRequest.Status equals Status.RequestStatusID
                                      select new { CRMRequest.ID, CRMRequest.RequestNum, CRMRequest.CreationDate, Service.ServiceName, Status.RequestStatusName, ProblemType.ProblemTypeName }).ToList();
        }

        private void dtgRequests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StaffAssigmentWindow w = new StaffAssigmentWindow((int)dtgRequests.SelectedValue);
            w.ShowDialog();
        }

        private void cbxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgRequests.ItemsSource = (from CRMRequest in cont.CRMRequest
                                       join Service in cont.Service on CRMRequest.Service equals Service.ServiceID
                                       join ProblemType in cont.ProblemType on CRMRequest.ProblemType equals ProblemType.ProblemTypeID
                                       join Status in cont.RequestStatus on CRMRequest.Status equals Status.RequestStatusID
                                       where CRMRequest.AccountNumber == cbxUser.SelectedValue
                                       select new { CRMRequest.ID, CRMRequest.RequestNum, CRMRequest.CreationDate, Service.ServiceName, Status.RequestStatusName, ProblemType.ProblemTypeName }).ToList();
        }
    }
}
