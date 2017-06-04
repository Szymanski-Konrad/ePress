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
        Dzial start;

        public Umowy(int x, Dzial w)
        {
            start = w;
            InitializeComponent();
            Ustaw();
            switch (x)
            {
                case 1:
                    usunumowe.Visibility = Visibility.Hidden;
                    dodajumowe.Visibility = Visibility.Hidden;
                    usunautora.Visibility = Visibility.Hidden;
                    dodajautora.Visibility = Visibility.Visible;
                    pisze.Items.Add("Tygodnik");
                    pisze.Items.Add("Miesiecznik");
                    pisze.Items.Add("Album");
                    break;
                case 2:
                    usunumowe.Visibility = Visibility.Hidden;
                    dodajumowe.Visibility = Visibility.Hidden;
                    dodajautora.Visibility = Visibility.Hidden;
                    usunautora.Visibility = Visibility.Visible;
                    break;
                case 3:
                    usunumowe.Visibility = Visibility.Hidden;
                    dodajautora.Visibility = Visibility.Hidden;
                    usunautora.Visibility = Visibility.Hidden;
                    dodajumowe.Visibility = Visibility.Visible;
                    Typ.Items.Add("O Prace");
                    Typ.Items.Add("O Dzielo");
                    break;
                case 4:
                    dodajumowe.Visibility = Visibility.Hidden;
                    dodajautora.Visibility = Visibility.Hidden;
                    usunautora.Visibility = Visibility.Hidden;
                    usunumowe.Visibility = Visibility.Visible;
                    break;
                default:

                    break;
            }
        }

        void Ustaw()
        {
            foreach (Autor item in start.GetAutorzy())
            {
                autorzy.Items.Add(item.Imie + " " + item.Nazwisko);
                JakiAutor.Items.Add(item.Imie + " " + item.Nazwisko);
            }
        }

        private void DodajAutora_Click(object sender, RoutedEventArgs e)
        {
            Autor a = new Autor() { Imie = dodajimie.Text, konto = 0, Nazwisko = dodajnazwisko.Text, sprzedaz = 0, coPisze = pisze.SelectedItem.ToString() };
            start.DodajAutora(a);
            this.Close();
        }

        private void UsunAutora_Click(object sender, RoutedEventArgs e)
        {
            start.GetAutorzy().Remove(start.GetAutorzy().Find(x => x.Imie + " " + x.Nazwisko == autorzy.SelectedItem.ToString()));
            start.Autors.Items.Clear();
            foreach (Autor item in start.GetAutorzy())
            {
                start.UstawDataContext(item);
            }
            this.Close();
        }

        private void Zawrzyj_Click(object sender, RoutedEventArgs e)
        {
            if (Typ.SelectedIndex == 0)
            {
                OPrace u = new OPrace() { stawka = Int32.Parse(Stawka.Text) };
                Autor a = start.GetAutorzy().Find(x => x.Imie + " " + x.Nazwisko == JakiAutor.SelectedItem.ToString());
                a.DodajUmowe(u);

                start.GetAutorzy().Remove(start.GetAutorzy().Find(x => x.Imie + " " + x.Nazwisko == JakiAutor.SelectedItem.ToString()));
                start.GetAutorzy().Add(a);
                start.Autors.Items.Clear();
                foreach (Autor item in start.GetAutorzy())
                {
                    start.UstawDataContext(item);
                }
            }
            else
            {
                ODzielo u = new ODzielo() { stawka = Int32.Parse(Stawka.Text) };
                Autor a = start.GetAutorzy().Find(x => x.Imie + " " + x.Nazwisko == JakiAutor.SelectedItem.ToString());
                a.DodajUmowe(u);

                start.GetAutorzy().Remove(start.GetAutorzy().Find(x => x.Imie + " " + x.Nazwisko == JakiAutor.SelectedItem.ToString()));
                start.GetAutorzy().Add(a);
                start.Autors.Items.Clear();
                foreach (Autor item in start.GetAutorzy())
                {
                    start.UstawDataContext(item);
                }
            }
            
        }

        private void Rowiaz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Typ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Typ.SelectedIndex == 0) Ukryty.Text = "Ile $$$: ";
            else Ukryty.Text = "Ile %: ";
            Stawka.Visibility = Visibility.Visible;
            Stawka.Text = "0";
        }
    }
}
