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

namespace ePress
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Wydawnictwo w;

        public MainWindow()
        {
            w = new Wydawnictwo();
            for (int i = 1; i <= 3; i++)
            {
                w.KupDrukarnie();
                MessageBox.Show(w.saldo.ToString());
            }
            DataContext = w;
            InitializeComponent();
        }

        private void DzialP_Click(object sender, RoutedEventArgs e)
        {
            Dzial d = new Dzial();
            d.ShowDialog();
        }

        private void Druk_Click(object sender, RoutedEventArgs e)
        {
            Drukarnie d = new Drukarnie();
            d.ShowDialog();
        }

        private void zamowienia_Click(object sender, RoutedEventArgs e)
        {
            Zamowienia z = new Zamowienia();
            z.ShowDialog();
        }

        private void Zapis_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}