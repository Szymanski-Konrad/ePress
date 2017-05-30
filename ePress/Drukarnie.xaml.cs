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
    /// Interaction logic for Drukarnie.xaml
    /// </summary>
    public partial class Drukarnie : Window
    {
        MainWindow page;

        public Drukarnie(MainWindow start)
        {
            page = start;
            InitializeComponent();

            //pokazanie zleceń realizowanych przez pierwszą drukarnię
            foreach (Zlecenie z in start.GetWydawnictwo().GetDrukarnie()[0].GetZlecenia())
            {
                DoWyswietlenia dw = new DoWyswietlenia();
                dw.cena = z.GetProdukt().cena;
                dw.iledni = z.ileDni;
                dw.naklad = z.GetProdukt().naklad;
                dw.stan = z.stan;
                dw.strony = z.GetProdukt().strony;
                dw.tytul = z.GetProdukt().tytul;

                pierwsza.Items.Add(dw);
            }

            //pokazanie zleceń realizowanych przez drugą drukarnię
            foreach (Zlecenie z in start.GetWydawnictwo().GetDrukarnie()[1].GetZlecenia())
            {
                DoWyswietlenia dw = new DoWyswietlenia();
                dw.cena = z.GetProdukt().cena;
                dw.iledni = z.ileDni;
                dw.naklad = z.GetProdukt().naklad;
                dw.stan = z.stan;
                dw.strony = z.GetProdukt().strony;
                dw.tytul = z.GetProdukt().tytul;

                druga.Items.Add(dw);
            }

            //pokazanie zleceń realizowanych przez trzecią drukarnię
            foreach (Zlecenie z in start.GetWydawnictwo().GetDrukarnie()[2].GetZlecenia())
            {
                DoWyswietlenia dw = new DoWyswietlenia();
                dw.cena = z.GetProdukt().cena;
                dw.iledni = z.ileDni;
                dw.naklad = z.GetProdukt().naklad;
                dw.stan = z.stan;
                dw.strony = z.GetProdukt().strony;
                dw.tytul = z.GetProdukt().tytul;

                trzecia.Items.Add(dw);
            }
        }
    }
}
