using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    public class Autor
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int konto { get; set; }
        public int sprzedaz { get; set; }
        public string coPisze { get; set; }
        public string dzielo { get; set; }
        Umowa umowa { get; set; }

        public Autor()
        {
            umowa = new Umowa();
        }

        public void DodajUmowe(Umowa u)
        {
            umowa = u;
        }

        public Umowa PokazUmowe()
        {
            return umowa;
        }
     }
}
