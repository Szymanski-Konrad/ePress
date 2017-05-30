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
    /// Interaction logic for Umowy.xaml
    /// </summary>
    public partial class Umowy : Window
    {
        public Umowy(int x)
        {
            InitializeComponent();
            switch(x)
            {
                case 1:
                    dodajautora.Visibility = Visibility.Visible;
                    break;
                case 2:
                    usunautora.Visibility = Visibility.Visible;
                    break;
                case 3:
                    dodajumowe.Visibility = Visibility.Visible;
                    break;
                case 4:
                    usunumowe.Visibility = Visibility.Visible;
                    break;
                default:

                    break;
            }
        }
    }
}
