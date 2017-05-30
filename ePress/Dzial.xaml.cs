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

namespace ePress
{
    /// <summary>
    /// Interaction logic for Dzial.xaml
    /// </summary>
    public partial class Dzial : Window
    {
        MainWindow page;

        public Dzial(MainWindow start)
        {
            page = start;
            InitializeComponent();
        }

        private void Autorzy_Click(object sender, RoutedEventArgs e)
        {
            DzialPodglad dp = new DzialPodglad(1, page);
            dp.ShowDialog();
        }

        private void Umowy_Click(object sender, RoutedEventArgs e)
        {
            DzialPodglad dp = new DzialPodglad(2, page);
            dp.ShowDialog();
        }
    }
}
