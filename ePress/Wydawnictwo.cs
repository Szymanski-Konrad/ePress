using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    public class Wydawnictwo : INotifyPropertyChanged
    {
        List<Drukarnia> drukarnie;
        List<Zlecenie> zlecenia;
        List<DateTime> terminy;
        public double saldo { get; set; }
        private int dzien;
        public int Dzien
        {
            get { return dzien; }
            set
            {
                dzien = value;
                OnPropertyChanged(nameof(Dzien));
            }
        }

        //umożliwienie automatycznego aktualizowania się wyświetlanej wartości
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string number)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(number));
            }
        }

        public Wydawnictwo()
        {
            drukarnie = new List<Drukarnia>();
            zlecenia = new List<Zlecenie>();
            terminy = new List<DateTime>();
            saldo = 10000;
            dzien = 0;
        }

        public List<Drukarnia> GetDrukarnie()
        {
            return drukarnie;
        }

        public void CoDrukujeDrukarnia()
        {
            Drukarnia d = new Drukarnia();
            d.jakosc = 0;
            foreach (Drukarnia dr in drukarnie)
            {
                if (dr.jakosc > d.jakosc) d = dr;
            }
            d.DodajCoDrukuje("Album");
        }

        public void KupDrukarnie()
        {
            Random r = new Random();
            Drukarnia d = new Drukarnia();
            double cena = 2000 * (d.jakosc / 5.0) * (d.wydajnosc / 3500.0);
            cena = Math.Round(cena);
            MessageBox.Show(cena.ToString());
            saldo -= cena;
            drukarnie.Add(d);
        }

        public void CzytajKsiazke(Zlecenie z)
        {
            Random r = new Random();
            z.GetProdukt().ocena = r.Next(1, 10);
            z.GetProdukt().naklad = 5000 * z.GetProdukt().ocena;
        }

        public void UstalCene(Zlecenie z)
        {
            double x = (z.GetProdukt().strony / 300) * (z.GetProdukt().ocena / 5);
            if (z.GetProdukt().GetType() == typeof(Album)) x = x * 1.5;
            z.GetProdukt().cena = 30 * (Int32)Math.Round(x);
        }

        public void PrzyjmijZamowienie(Zlecenie z) 
        {
            zlecenia.Add(z);
        }

        public Drukarnia NajmniejZajeta(string typ)
        {
            Drukarnia dr = drukarnie[0];
            foreach (Drukarnia d in drukarnie)
            {
                if (d.zajeta < dr.zajeta && d.CzyMozeDrukowac(typ) == true) dr = d;
            }
            return dr;
        }

        public void PrzydzielZlecenie(Zlecenie z)
        {
            Drukarnia d = NajmniejZajeta(z.GetProdukt().GetType().ToString());
            d.CzasWydruku(z);
            d.DodajZlecenie(z);
        }

        //sprzedawanie produktu jeśli jest gotowa codziennie aż do wyczerpania nakładu
        public void SprzedazKsiazki(Zlecenie z)
        {
            if (z.stan == "wykonane" && z.GetProdukt().naklad > 0)
            {
                if (z.GetProdukt().naklad < 1000)
                {
                    saldo += z.GetProdukt().cena * z.GetProdukt().naklad;
                    // oddawanie części kasy dla autorów danej książki
                    foreach (var item in z.GetProdukt().tytul)
                    {

                    }
                    z.GetProdukt().naklad = 0;
                    
                }
                else
                {
                    int ilosc = (Int32)Math.Round(z.GetProdukt().naklad * 0.6);
                    // oddawanie części kasy dla autorów danej książki
                    foreach (var item in z.GetProdukt().tytul)
                    {

                    }
                    saldo += z.GetProdukt().cena * ilosc;
                    z.GetProdukt().naklad -= ilosc;
                }
            }
        }
    }
}