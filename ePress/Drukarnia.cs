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
        List<Zlecenie> kolejka;
        List<Produkty> coDrukuje;

        public Drukarnia()
        {
            Random r = new Random();
            wydajnosc = r.Next(3000, 4000);
            jakosc = r.Next(1, 10);
            kolejka = new List<Zlecenie>();
            coDrukuje = new List<Produkty>();
        }

        public void DodajZlecenie(Zlecenie z)
        {
            kolejka.Add(z);
        }

        public void ZglaszanieWydruku()
        {

        }

        public void CzasWydruku()
        {

        }


    }
}
