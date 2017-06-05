using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    public class Drukarnia
    {
        public int wydajnosc { get; set; }
        public int jakosc { get; set; }
        public int zajeta { get; set; }
        Queue<Zlecenie> kolejka;
        List<Zlecenie> gotowe;
        List<string> coDrukuje;

        public Drukarnia()
        {
            Random r = new Random();
            wydajnosc = r.Next(30000, 50000);
            jakosc = r.Next(1, 10);
            System.Threading.Thread.Sleep(500);
            kolejka = new Queue<Zlecenie>();
            gotowe = new List<Zlecenie>();
            coDrukuje = new List<string>();
            zajeta = 0;
            if (coDrukuje.Count < 2)
            {
                DodajCoDrukuje("Miesiecznik");
                DodajCoDrukuje("Tygodnik");
                DodajCoDrukuje("Romans");
                DodajCoDrukuje("Sensacja");
            }
        }

        public void DodajZlecenie(Zlecenie z)
        {
            z.stan = "w kolejce";
            kolejka.Enqueue(z);
            zajeta += z.ileDni;
        }

        public void DodajCoDrukuje(string s)
        {
            coDrukuje.Add(s);
        }

        public bool CzyMozeDrukowac(string s)
        {
            if (coDrukuje.Contains(s)) return true;
            else return false;
        }

        public List<string> GetCoDrukuje()
        {
            return coDrukuje;
        }

        public Queue<Zlecenie> GetKolejka()
        {
            return kolejka;
        }

        public List<Zlecenie> GetGotowe()
        {
            return gotowe;
        }

        public List<Zlecenie> GetZlecenia()
        {
            List<Zlecenie> zlecenia = new List<Zlecenie>();
            foreach (Zlecenie z in kolejka) zlecenia.Add(z);
            foreach (Zlecenie z in gotowe) zlecenia.Add(z);
            return zlecenia;
        }

        public void ZglaszanieWydruku()
        {
            if (kolejka.Count > 0 && kolejka.Peek().ileDni == 0)
            {
                kolejka.Peek().stan = "wykonane";
                gotowe.Add(kolejka.Dequeue());
            }
            if (kolejka.Count > 0) kolejka.Peek().stan = "drukowanie";
        }

        public void CzasWydruku(Zlecenie z)
        {
            double x = z.GetProdukt().strony * z.GetProdukt().naklad / wydajnosc;
            z.ileDni = (Int16)Math.Round(x);
            foreach (Zlecenie item in kolejka)
            {
                z.ileDni += item.ileDni;
            }
        }

        public void RedukujCzasWydruku()
        {
            foreach (Zlecenie item in kolejka)
            {
                item.ileDni -= 1;
            }
        }

        public void AktualizacjaDrukarni()
        {
            ZglaszanieWydruku();
            RedukujCzasWydruku();
        }
    }
}
