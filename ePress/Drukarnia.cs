using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    class Drukarnia
    {
        public int wydajnosc { get; set; }
        public int jakosc { get; set; }
        Queue<Zlecenie> kolejka;
        List<Zlecenie> gotowe;
        List<string> coDrukuje;

        public Drukarnia()
        {
            Random r = new Random();
            wydajnosc = r.Next(3000, 4000);
            jakosc = r.Next(1, 10);
            kolejka = new Queue<Zlecenie>();
            gotowe = new List<Zlecenie>();
            coDrukuje = new List<string>();
        }

        public void DodajZlecenie(Zlecenie z)
        {
            z.stan = "w kolejce";
            kolejka.Enqueue(z);
        }

        public void DodajCoDrukuje(string s)
        {
            coDrukuje.Add(s);
        }

        public void ZglaszanieWydruku()
        {
            if (kolejka.Peek().ileDni == 0)
            {
                kolejka.Peek().stan = "wykonane";
                gotowe.Add(kolejka.Dequeue());
            }
        }

        public void CzasWydruku(Zlecenie z)
        {
            double x = z.GetProdukt().strony * z.GetProdukt().naklad / wydajnosc;
            z.ileDni = (Int16)Math.Round(x);
        }
    }
}
