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
    /// Interaction logic for DzialPodglad.xaml
    /// </summary>
    public partial class DzialPodglad : Window
    {
        MainWindow page;

        public DzialPodglad(int x, MainWindow start)
        {
            page = start;
            InitializeComponent();
            if (x == 1)
            {
                A.Visibility = Visibility.Visible;
                U.Visibility = Visibility.Hidden;
            }
            else
            {
                A.Visibility = Visibility.Hidden;
                U.Visibility = Visibility.Visible;
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Umowy u = new Umowy(1);
            u.ShowDialog();
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            Umowy u = new Umowy(2);
            u.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Umowy u = new Umowy(3);
            u.ShowDialog();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Umowy u = new Umowy(4);
            u.ShowDialog();
        }
    }
}