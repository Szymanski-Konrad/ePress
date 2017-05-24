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
        double saldo;

        public Wydawnictwo()
        {
            drukarnie = new List<Drukarnia>();
            zlecenia = new List<Zlecenie>();
            saldo = 10000;
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
            
        }

        public void PrzyjmijZamowienie(Zlecenie z) 
        {
            zlecenia.Add(z);
        }
    }
}
