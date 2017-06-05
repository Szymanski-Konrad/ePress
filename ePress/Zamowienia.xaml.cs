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

        //uzupełnianie kontrolek w okienku
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

        //metoda sprawdzająca, czy nie podano przypadkowo innych znaków niż cyfry przy wpisywaniu ilości stron
        void SprawdzStr()
        {
            string s = "";
            foreach (char ch in str.Text) if (Char.IsNumber(ch)) s += ch;
            str.Text = s;
        }

        //dostosowywanie widoku okna do wybranych opcji przez użytkownika
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

        //dostosowywanie widoku okna do wybranych opcji przez użytkownika
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

        //dodawanie zamówienie do kolejki
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            //sprawdzenie czy wszystkie dane zostały podane
            if (t.Text == "") { MessageBox.Show("Podaj tytuł"); return; }
            if (typy.SelectedItem == null) { MessageBox.Show("Wybierz typ produktu"); return; }
            if (str.Text == "") { MessageBox.Show("Podaj ilość stron"); return; }
            if (Ilu.SelectedItem == null) { MessageBox.Show("Wybierz ilość autorów"); return; }
            if (page.GetWydawnictwo().ZbierzZlecenia().Find(x => x.GetProdukt().tytul == t.Text) != null) { MessageBox.Show("Taki tytuł już istnieje w naszej bazie"); return; } 
            TextBox t1, t2;
            ComboBox c;
            Zlecenie z = new Zlecenie() { stan = "czeka" };
            //podział dodawania zlecenia zależnie od wybranego typu produktu
            if (typy.SelectedItem.ToString() == "Miesiecznik")
            {
                SprawdzStr();
                z.UstawProdukt(new Miesiecznik(30) { tytul = t.Text, strony = Int32.Parse(str.Text), naklad = 8000, cena = 10 });
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem; i ++)
                {
                    c = (ComboBox)Panel.FindName("combo" + i.ToString());
                    if (c.SelectedItem == null) { MessageBox.Show("Wybierz autora"); return; }
                    Autor a = page.GetLista().Find(x => x.Imie + " " + x.Nazwisko == c.SelectedItem.ToString());
                    a.dzielo = z.GetProdukt().tytul;
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Tygodnik")
            {
                SprawdzStr();
                z.UstawProdukt(new Tygodnik(7) { tytul = t.Text, strony = Int32.Parse(str.Text), naklad = 2000, cena = 2 });
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem; i++)
                {
                    c = (ComboBox)Panel.FindName("combo" + i.ToString());
                    if (c.SelectedItem == null) { MessageBox.Show("Wybierz autora"); return; }
                    Autor a = page.GetLista().Find(x => x.Imie + " " + x.Nazwisko == c.SelectedItem.ToString());
                    a.dzielo = z.GetProdukt().tytul;
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Romans")
            {
                SprawdzStr();
                z.UstawProdukt(new Romans() { tytul = t.Text, strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    if (t1.Text == "" || t2.Text == "") { MessageBox.Show("Uzupełnij dane autora"); return; }
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    if (page.GetLista().Find(y => y.Imie == a.Imie && y.Nazwisko == a.Nazwisko) != null) { MessageBox.Show("Taki autor już znajduje się w bazie"); return; }
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    a.dzielo = z.GetProdukt().tytul;
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Sensacja")
            {
                SprawdzStr();
                z.UstawProdukt(new Sensacja() { tytul = t.Text, strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    if (t1.Text == "" || t2.Text == "") { MessageBox.Show("Uzupełnij dane autora"); return; }
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    if (page.GetLista().Find(y => y.Imie == a.Imie && y.Nazwisko == a.Nazwisko) != null) { MessageBox.Show("Taki autor już znajduje się w bazie"); return; }
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    a.dzielo = z.GetProdukt().tytul;
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            if (typy.SelectedItem.ToString() == "Album")
            {
                SprawdzStr();
                z.UstawProdukt(new Album() { tytul = t.Text, strony = Int32.Parse(str.Text) });
                page.GetWydawnictwo().CzytajKsiazke(z);
                page.GetWydawnictwo().UstalCene(z);
                page.GetWydawnictwo().PrzyjmijZamowienie(z);
                for (int i = 1; i <= (Int32)Ilu.SelectedItem * 2; i += 2)
                {
                    int x = i + 1;
                    t1 = (TextBox)Panel.FindName("autor" + i.ToString()); t2 = (TextBox)Panel.FindName("autor" + x.ToString());
                    if (t1.Text == "" || t2.Text == "") { MessageBox.Show("Uzupełnij dane autora"); return; }
                    Autor a = new Autor() { Imie = t1.Text, Nazwisko = t2.Text, coPisze = typy.SelectedItem.ToString() };
                    if (page.GetLista().Find(y => y.Imie == a.Imie && y.Nazwisko == a.Nazwisko) != null) { MessageBox.Show("Taki autor już znajduje się w bazie"); return; }
                    a.DodajUmowe(new ODzielo() { stawka = 10 });
                    a.dzielo = z.GetProdukt().tytul;
                    page.DodajAutoraDoListy(a);
                    z.GetProdukt().DodajAutora(a);
                }
            }
            this.Close();
        }
    }
}
