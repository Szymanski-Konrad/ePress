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
        MainWindow page;

        public Zamowienia(MainWindow start)
        {
            page = start;
            InitializeComponent();
            for(int i = 1; i < 5; i++)
            {
                Ilu.Items.Add(i);
            }
            Typy();
        }

        void Typy()
        {
            typy.Items.Add("Tygodnik");
            typy.Items.Add("Miesiecznik");
            typy.Items.Add("Album");
            typy.Items.Add("Romans");
            typy.Items.Add("Sensacja");
        }

        void WyczyscCombo()
        {
            for (int i = 1; i <= 4; i++)
            {
                ComboBox c = (ComboBox)Wybor.FindName("combo" + i.ToString());
                c.Items.Clear();
            }
        }

        void UzupelnijAutorow(string s)
        {
            foreach (Autor a in page.GetLista())
            {
                if (a.coPisze == s)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        ComboBox c = (ComboBox)Wybor.FindName("combo" + i.ToString());
                        c.Items.Add(a.Imie + " " + a.Nazwisko);
                    }
                }
            }
        } 

        private void Ilu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox t, t1;
            ComboBox c;

            if (typy.SelectedIndex == 0 || typy.SelectedIndex == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    c = (ComboBox)Wybor.FindName("combo" + i.ToString());
                    if (i <= Int32.Parse(Ilu.SelectedItem.ToString())) c.Visibility = Visibility.Visible;
                    else c.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                i.Visibility = Visibility.Visible;
                n.Visibility = Visibility.Visible;

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

        private void typy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typy.SelectedIndex == 0 || typy.SelectedIndex == 1)
            {
                WyczyscCombo();
                Panel.Visibility = Visibility.Hidden;
                Wybor.Visibility = Visibility.Visible;
                if (typy.SelectedIndex == 0) UzupelnijAutorow("Tygodnik");
                else UzupelnijAutorow("Miesiecznik");
            }
            else
            {
                Panel.Visibility = Visibility.Visible;
                Wybor.Visibility = Visibility.Hidden;
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            TextBox t1, t2;
            ComboBox c;
            Zlecenie z = new Zlecenie() { stan = "czeka" };
            if (typy.SelectedItem.ToString() == "Miesiecznik")
            {
                z.UstawProdukt(new Miesiecznik(30, page.GetWydawnictwo().Dzien) { tytul = t.Text, strony = Int32.Parse(str.Text), naklad = 8000, cena = 10 });
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem; i ++)
                {
                    c = (ComboBox)Panel.FindName("combo" + i.ToString());
                    Autor a = page.GetLista().Find(x => x.Imie + " " + x.Nazwisko == c.SelectedItem.ToString());
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Tygodnik")
            {
                z.UstawProdukt(new Tygodnik(7, page.GetWydawnictwo().Dzien) { tytul = t.Text, strony = Int32.Parse(str.Text), naklad = 2000, cena = 2 });
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem; i++)
                {
                    c = (ComboBox)Panel.FindName("combo" + i.ToString());
                    Autor a = page.GetLista().Find(x => x.Imie + " " + x.Nazwisko == c.SelectedItem.ToString());
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Romans")
            {
                z.UstawProdukt(new Romans() { tytul = t.Text, dataWydania = Int32.Parse(page.Dzien.Text), strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Sensacja")
            {
                z.UstawProdukt(new Sensacja() { tytul = t.Text, dataWydania = Int32.Parse(page.Dzien.Text), strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Album")
            {
                z.UstawProdukt(new Album() { tytul = t.Text, dataWydania = Int32.Parse(page.Dzien.Text), strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            this.Close();
        }
    }
}
