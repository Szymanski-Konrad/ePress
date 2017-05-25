using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    class Wydawnictwo
    {
        List<Drukarnia> drukarnie;
        List<Zlecenie> zlecenia;
        List<DateTime> terminy;
        double saldo;
        int dzien;

        public Wydawnictwo()
        {
            drukarnie = new List<Drukarnia>();
            zlecenia = new List<Zlecenie>();
            terminy = new List<DateTime>();
            saldo = 10000;
            dzien = 0;
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
            double cena = 2000 * (d.jakosc / 5) * (d.wydajnosc / 3500);
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

        //sprzedawanie produktu jeśli jest gotowa codziennie aż do wyczerpania nakładu
        public void SprzedazKsiazki(Zlecenie z)
        {
            if (z.stan == "wykonane" && z.GetProdukt().naklad > 0)
            {
                if (z.GetProdukt().naklad < 1000)
                {
                    saldo += z.GetProdukt().cena * z.GetProdukt().naklad;
                    z.GetProdukt().naklad = 0;
                }
                else
                {
                    int ilosc = (Int32)Math.Round(z.GetProdukt().naklad * 0.6);
                    saldo += z.GetProdukt().cena * ilosc;
                    z.GetProdukt().naklad -= ilosc;
                }
            }
        }
    }
}