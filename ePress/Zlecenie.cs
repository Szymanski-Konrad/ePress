using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    class Zlecenie
    {
        Drukarnia drukarnia;
        Produkt produkt;
        public string stan { get; set; }
        public int ileDni { get; set; }

        public Zlecenie()
        {
            drukarnia = new Drukarnia();
            produkt = new Produkt();
        }

        public Produkt GetProdukt()
        {
            stan = "czeka";
            return produkt;
        }
    }
}
