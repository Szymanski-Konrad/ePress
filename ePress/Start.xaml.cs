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
using System.Windows.Shapes;

namespace ePress
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
            if (SprawdzPliki() == true)
            {
                MainWindow mw = new MainWindow(1);
                this.Close();
                mw.ShowDialog();
            }
        }

        //metoda sprawdzająca czy na dysku znajdują się wszelkie potrzebne dane
        public bool SprawdzPliki()
        {
            if (!File.Exists("autorzy.txt")) return false;
            if (!File.Exists("drukarnia1.txt")) return false;
            if (!File.Exists("drukarnia2.txt")) return false;
            if (!File.Exists("drukarnia3.txt")) return false;
            if (!File.Exists("info.txt")) return false;
            return true;
        }

        //zakup pierwszej drukarki
        private void Pierwsza_Click(object sender, RoutedEventArgs e)
        {
            Druga.Visibility = Visibility.Visible;
            text2.Visibility = Visibility.Visible;
            Pierwsza.Visibility = Visibility.Hidden;
            text1.Visibility = Visibility.Hidden;
        }

        //zakup drugiej drukarki
        private void Druga_Click(object sender, RoutedEventArgs e)
        {
            Trzecia.Visibility = Visibility.Visible;
            text3.Visibility = Visibility.Visible;
            Druga.Visibility = Visibility.Hidden;
            text2.Visibility = Visibility.Hidden;
        }

        //zakup trzeciej drukarki
        private void Trzecia_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(0);
            this.Close();
            mw.ShowDialog();
        }
    }
}
