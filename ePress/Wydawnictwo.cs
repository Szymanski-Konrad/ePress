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
        private int dzien;

        public Wydawnictwo()
        {
            drukarnie = new List<Drukarnia>();
            zlecenia = new List<Zlecenie>();
            nasprzedanie = new List<Zlecenie>();
            saldo = 15000;
            dzien = 0;
        }

        public double Saldo
        {
            get { return saldo; }
            set
            {
                saldo = value;
                OnPropertyChanged(nameof(Saldo));
            }
        }

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

        //wypłacanie pensji autorom zatrudnionym na umowę o pracę na koniec miesiąca
        public void Pensja(List<Autor> l)
        {
            foreach (Autor a in l)
            {
                if (a.PokazUmowe().GetType() == typeof(OPrace))
                {
                    a.konto += a.PokazUmowe().stawka;
                    Saldo -= a.PokazUmowe().stawka;
                }
            }
        }

        //zbieranie wszystkich zleceń
        public List<Zlecenie> ZbierzZlecenia()
        {
            List<Zlecenie> z = new List<Zlecenie>();
            foreach (Drukarnia d in GetDrukarnie())
            {
                foreach (Zlecenie zl in d.GetZlecenia())
                {
                    z.Add(zl);
                }
            }
            return z;
        }

        //wystawienie gotowych zleceń na sprzedaż
        public void DodajGotowe(Zlecenie z)
        {
            nasprzedanie.Add(z);
        } 

        //usuwanie sprzedanych pozycji
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

        //znajdowanie najlepszej jakościowo drukarni, aby umożliwić jej drukowanie albumów
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

        //kupowanie drukarni
        public void KupDrukarnie()
        {
            Random r = new Random();
            Drukarnia d = new Drukarnia();
            double cena = 2000 * ((float)d.jakosc / 5.0) * ((float)d.wydajnosc / 35000.0);
            if (cena < 2500) cena = 2500;
            cena = Math.Round(cena);
            Saldo -= cena;
            drukarnie.Add(d);
        }

        //wydawnictwo czyta książkę, aby ocenić pozycję
        public void CzytajKsiazke(Zlecenie z)
        {
            Random r = new Random();
            Ksiazka k = (Ksiazka)z.GetProdukt();
            k.ocena = r.Next(1, 10);
            k.naklad = 1000 * k.ocena;
        }

        //ustalanie ceny sprzedaży
        public void UstalCene(Zlecenie z)
        {
            Ksiazka k = (Ksiazka)z.GetProdukt();
            double x = ((float)k.strony / 30) * ((float)k.ocena / 5);
            if (z.GetProdukt().GetType() == typeof(Album)) x = x * 1.5;
            k.cena = 30 * (Int32)Math.Round(x);
            if (k.cena < 3) k.cena = 3;
        }
        
        //przyjmowanie zamównień od klientów
        public void PrzyjmijZamowienie(Zlecenie z) 
        {
            zlecenia.Add(z);
        }

        //sprawdzenie, która drukarnia jest w danym momencie najmniej zajęta, aby drukowanie kolejnych zleceń przebiegało możliwie szybko
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

        //  metoda przedłużania tygodników oraz miesięczników, jednak nie działa do końca jak trzeba, więc nie ma faktycznej realizacji w kodzie
        //public void Przedluzanie()
        //{
        //    foreach (Drukarnia d in drukarnie)
        //    {
        //        foreach (Zlecenie z in d.GetZlecenia())
        //        {
        //            if (z.GetProdukt().GetType().Name == "Tygodnik")
        //            {
        //                Tygodnik t = (Tygodnik)z.GetProdukt();
        //                if ( (Dzien - t.DataRozpoczecia()) % t.Czestotliwosc()  == 0)
        //                {
        //                    Zlecenie zl = new Zlecenie() { stan = "czeka" };
        //                    PrzyjmijZamowienie(zl);
        //                }
        //            }
        //            if (z.GetProdukt().GetType().Name == "Miesiecznik")
        //            {
        //                Miesiecznik m = (Miesiecznik)z.GetProdukt();
        //                if ((Dzien - m.DataRozpoczecia()) % m.Czestotliwosc() == 0)
        //                {
        //                    Zlecenie zl = new Zlecenie() { stan = "czeka" };
        //                    zl.UstawProdukt(new Miesiecznik(30, Dzien) { tytul = m.tytul + (Dzien / m.Czestotliwosc()), cena = m.cena, naklad = m.naklad, strony = m.strony });
        //                    foreach (Autor a in m.GetAutorzy()) zl.GetProdukt().DodajAutora(a);
        //                    PrzyjmijZamowienie(zl);
        //                }
        //            }
        //        }
        //    }
        //}

        //przydzielanie zleceń dla drukarni na koniec każdego dnia
        public void PrzydzielZlecenia()
        {
            foreach (Zlecenie z in zlecenia)
            {
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
                        // oddawanie części zarobków dla autorów danej książki
                        foreach (Autor item in z.GetProdukt().GetAutorzy())
                        {
                            if (item.PokazUmowe().GetType() == typeof(ODzielo))
                            {
                                item.sprzedaz += z.GetProdukt().naklad;
                                item.konto += z.GetProdukt().cena * z.GetProdukt().naklad;
                            }
                        }
                        z.GetProdukt().naklad = 0;
                    }
                    else
                    {
                        int ilosc = (Int32)Math.Round(z.GetProdukt().naklad * 0.3);
                        // oddawanie części zarobków dla autorów danej książki
                        foreach (Autor item in z.GetProdukt().GetAutorzy())
                        {
                            if (item.PokazUmowe().GetType() == typeof(ODzielo))
                            {
                                item.sprzedaz += ilosc;
                                item.konto += z.GetProdukt().cena * ilosc;
                            }
                        }
                        Saldo += z.GetProdukt().cena * ilosc;
                        z.GetProdukt().naklad -= ilosc;
                    }
                }
            }
        }
    }
}