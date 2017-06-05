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

        public Zlecenie()
        {
            produkt = new Produkt();
        }

        public void UstawDrukarnie(Drukarnia d)
        {
            drukarnia = d;
        }

        public void UstawProdukt(Produkt p)
        {
            produkt = p;
        }

        public Produkt GetProdukt()
        {
            return produkt;
        }
    }
}
