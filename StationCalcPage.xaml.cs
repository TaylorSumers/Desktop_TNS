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
using StationCalulationLibrary;

namespace Desktop_TNS
{
    /// <summary>
    /// Interaction logic for StationCalcPage.xaml
    /// </summary>
    public partial class StationCalcPage : Page
    {
        public StationCalcPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double n = Calculation.Main(Convert.ToDouble(S.Text), Convert.ToDouble(Sb.Text), Convert.ToDouble(k.Text), Convert.ToDouble(S1.Text), Convert.ToDouble(S2.Text));

            tbn.Text += n;
        }
    }
}
