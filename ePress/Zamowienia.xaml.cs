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
    /// Interaction logic for Zamowienia.xaml
    /// </summary>
    public partial class Zamowienia : Window
    {
        public Zamowienia()
        {
            InitializeComponent();
            for(int i = 1; i < 5; i++)
            {
                Ilu.Items.Add(i);
            }
        }

        private void Ilu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox t, t1;

            for (int i = 1; i < 9; i += 2)
            {
                t = (TextBox)Panel.FindName("autor" + i.ToString());
                int x = i + 1;
                t1 = (TextBox)Panel.FindName("autor" + x.ToString());

                if (i <= Int32.Parse(Ilu.SelectedItem.ToString()) * 2)
                {
                    t.Visibility = Visibility.Visible;
                    t1.Visibility = Visibility.Visible;
                }
                else
                {
                    t.Visibility = Visibility.Hidden;
                    t1.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
