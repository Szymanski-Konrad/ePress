using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    public class Zlecenie
    {
        Drukarnia drukarnia;
        Produkt produkt;
        public string stan { get; set; }
        public int ileDni { get; set; }

        public Zlecenie(Drukarnia d, Produkt p)
        {
            drukarnia = d;
            produkt = p;
        }

        public Produkt GetProdukt()
        {
            return produkt;
        }
    }
}
