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
        List<Autor> ListaAutorow;
        Dane dane;

        public MainWindow(int x)
        {
            InitializeComponent();
            ListaAutorow = new List<Autor>();
            w = new Wydawnictwo();
            dane = new Dane();
            if (x == 1)
            {
                SetLista(dane.WczytajAutorow());
                foreach (Drukarnia d in dane.WczytajDrukarnie())
                {
                    w.GetDrukarnie().Add(d);
                }
                dane.WczytajStanWydawnictwa(w);
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    w.KupDrukarnie();
                }
                w.CoDrukujeDrukarnia();
            }
            DataContext = w;
        }

        public void DodajAutoraDoListy(Autor a)
        {
            ListaAutorow.Add(a);
        }

        public void SetLista(List<Autor> l)
        {
            ListaAutorow = l;
        }

        public List<Autor> GetLista()
        {
            return ListaAutorow;
        }

        public Wydawnictwo GetWydawnictwo()
        { 
            return w;
        }

        private void DzialP_Click(object sender, RoutedEventArgs e)
        {
            Dzial d = new Dzial(this, ListaAutorow);
            d.ShowDialog();
        }

        private void Druk_Click(object sender, RoutedEventArgs e)
        {
            Drukarnie d = new Drukarnie(this);
            d.ShowDialog();
        }

        private void zamowienia_Click(object sender, RoutedEventArgs e)
        {
            Zamowienia z = new Zamowienia(this);
            z.ShowDialog();
        }

        private void Zapis_Click(object sender, RoutedEventArgs e)
        {
            dane.ZapiszAutorow(ListaAutorow);
            dane.ZapiszDrukarnie(w.GetDrukarnie());
            dane.ZapiszStanWydawnictwa(w);
        }

        private void Nowy_Click(object sender, RoutedEventArgs e)
        {
            w.Dzien += 1;
            w.PrzydzielZlecenia();
            w.Sprzedaz();
            foreach (Drukarnia d in w.GetDrukarnie())
            {
                d.AktualizacjaDrukarni();
                foreach (Zlecenie z in d.GetGotowe())
                {
                    w.DodajGotowe(z);
                }
            }
            w.UsunSprzedane();
            if (w.Dzien % 30 == 0) w.Pensja(ListaAutorow);
        }
    }
}