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
        List<Zlecenie> nasprzedanie;
        private double saldo;
        public double Saldo
        {
            get { return saldo; }
            set
            {
                saldo = value;
                OnPropertyChanged(nameof(Saldo));
            }
        }
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
            nasprzedanie = new List<Zlecenie>();
            saldo = 10000;
            dzien = 0;
        }

        public void DodajGotowe(Zlecenie z)
        {
            nasprzedanie.Add(z);
        } 

        public void UsunSprzedane()
        {
            for (int i = 0; i < nasprzedanie.Count; i++)
            {
                if (nasprzedanie[i].GetProdukt().naklad == 0)
                {
                    nasprzedanie.Remove(nasprzedanie[i]);
                }
            }
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
            double cena = 1000 * ((float)d.jakosc / 5.0) * ((float)d.wydajnosc / 3500.0);
            cena = Math.Round(cena);
            Saldo -= cena;
            drukarnie.Add(d);
        }

        public void CzytajKsiazke(Zlecenie z)
        {
            Random r = new Random();
            Ksiazka k = (Ksiazka)z.GetProdukt();
            k.ocena = r.Next(1, 10);
            k.naklad = 5000 * k.ocena;
        }

        public void UstalCene(Zlecenie z)
        {
            Ksiazka k = (Ksiazka)z.GetProdukt();
            double x = ((float)k.strony / 30) * ((float)k.ocena / 5);
            MessageBox.Show(x.ToString());
            if (z.GetProdukt().GetType() == typeof(Album)) x = x * 1.5;
            k.cena = 30 * (Int32)Math.Round(x);
            MessageBox.Show(k.cena.ToString());
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
                if (d.zajeta < dr.zajeta && d.CzyMozeDrukowac(typ) == true)
                {
                    dr = d;
                }
            }
            return dr;
        }

        public void PrzydzielZlecenia()
        {
            foreach (Zlecenie z in zlecenia)
            {
                MessageBox.Show(z.GetProdukt().GetType().Name);
                Drukarnia d = NajmniejZajeta(z.GetProdukt().GetType().Name);
                d.CzasWydruku(z);
                d.DodajZlecenie(z);
            }
            zlecenia.Clear();
        }

        //sprzedawanie produktu jeśli jest gotowa codziennie aż do wyczerpania nakładu
        public void Sprzedaz()
        {
            foreach (Zlecenie z in nasprzedanie)
            {
                if (z.GetProdukt().naklad > 0)
                {
                    if (z.GetProdukt().naklad < 1000)
                    {
                        Saldo += z.GetProdukt().cena * z.GetProdukt().naklad;
                        // oddawanie części kasy dla autorów danej książki
                        foreach (Autor item in z.GetProdukt().GetAutorzy())
                        {
                            item.sprzedaz += z.GetProdukt().naklad;
                            item.konto += z.GetProdukt().cena * z.GetProdukt().naklad;
                        }
                        z.GetProdukt().naklad = 0;
                    }
                    else
                    {
                        int ilosc = (Int32)Math.Round(z.GetProdukt().naklad * 0.6);
                        // oddawanie części kasy dla autorów danej książki
                        foreach (Autor item in z.GetProdukt().GetAutorzy())
                        {
                            item.sprzedaz += ilosc;
                            item.konto += z.GetProdukt().cena * ilosc;
                        }
                        Saldo += z.GetProdukt().cena * ilosc;
                        z.GetProdukt().naklad -= ilosc;
                    }
                }
            }
        }
    }
}