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
    /// Interaction logic for StaffAssigmentWindow.xaml
    /// </summary>
    public partial class StaffAssigmentWindow : Window
    {
        TNSEntities cont = new TNSEntities();
        RequestToEmployee requestToEmployee = new RequestToEmployee();
        DateTime date = new DateTime(2022, 08, 28);
        public StaffAssigmentWindow(int SelectedRequest)
        {
            InitializeComponent();

            cbxEngineer.ItemsSource = cont.Employee.Where(emp => emp.Position == 4).ToList();

            var RequestInfo = (from Request in cont.CRMRequest
                               join Service in cont.Service on Request.Service equals Service.ServiceID
                               join ServiceType in cont.ServiceType on Request.ServiceType equals ServiceType.ID
                               join ServiceKind in cont.ServiceKind on ServiceType.ServiceKind equals ServiceKind.ID
                               where Request.ID == SelectedRequest
                               select new { Request.ID, Request.AccountNumber, Service.ServiceName, ServiceKind.Name, Request.CreationDate }).Single();
            tbAccountNum.Text += RequestInfo.AccountNumber;
            tbService.Text += RequestInfo.ServiceName;
            tbSerKind.Text += RequestInfo.Name;
            tbDate.Text += RequestInfo.CreationDate;

            DataContext = requestToEmployee;
            requestToEmployee.ReqID = SelectedRequest;
            requestToEmployee.isHoliday = false;
        }

        private void cbxEngineer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var Schedule = EngineerSchedule().Where(item => item.DateTime.Date == date).ToList();
            Console.WriteLine(Schedule.Count);
            if (Schedule.Count == 0)
            {
                dtgEngineerSheldue.ItemsSource = Schedule;
            }
            else
            {
                if (Schedule[0].isHoliday == true)
                {
                    MessageBox.Show("У инженера выходной");
                }
                else
                {
                    dtgEngineerSheldue.ItemsSource = Schedule;
                }
            }

        }

        private void btnAssign_Click(object sender, RoutedEventArgs e)
        {
            DateTime DateTimeStart = date.AddHours(Convert.ToDouble(tbxHours.Text)).AddMinutes(Convert.ToDouble(tbxMinutes.Text));
            Console.WriteLine(DateTimeStart);
            requestToEmployee.DateTime = DateTimeStart;
            requestToEmployee.TimeEnd = DateTimeStart.AddMinutes(30).TimeOfDay;
            cont.RequestToEmployee.Add(requestToEmployee);
            cont.SaveChanges();

            dtgEngineerSheldue.ItemsSource = EngineerSchedule().Where(item => item.DateTime.Date == date).ToList();
        }

        IEnumerable<dynamic> EngineerSchedule()
        {
            int SelectedEngineer = (int)cbxEngineer.SelectedValue;
            var Sheld1 = from Schedule in cont.RequestToEmployee
                         join Engineer in cont.Employee on Schedule.EmpID equals Engineer.ID
                         select new { Engineer.ID, Schedule.ReqID, Schedule.DateTime, Schedule.TimeEnd, Schedule.isHoliday };

            var Sheld2 = from Request in cont.CRMRequest
                         join Service in cont.Service on Request.Service equals Service.ServiceID
                         select new { Request.ID, Request.RequestNum, Service.ServiceName };

            var Sheld3 =  from S1 in Sheld1
                          join S2 in Sheld2 on S1.ReqID equals S2.ID into joinsheld
                          where S1.ID == SelectedEngineer
                          from sheld in joinsheld.DefaultIfEmpty()
                          select new { sheld.ServiceName, sheld.RequestNum, S1.DateTime, S1.TimeEnd, S1.isHoliday };

            return Sheld3;
        }
    }
}
