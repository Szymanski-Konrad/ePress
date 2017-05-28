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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Pierwsza_Click(object sender, RoutedEventArgs e)
        {
            Druga.Visibility = Visibility.Visible;
            text2.Visibility = Visibility.Visible;
            Pierwsza.Visibility = Visibility.Hidden;
            text1.Visibility = Visibility.Hidden;
        }

        private void Druga_Click(object sender, RoutedEventArgs e)
        {
            Trzecia.Visibility = Visibility.Visible;
            text3.Visibility = Visibility.Visible;
            Druga.Visibility = Visibility.Hidden;
            text2.Visibility = Visibility.Hidden;
        }

        private void Trzecia_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }
    }
}
