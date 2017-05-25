using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    class Produkt
    {
        public int strony { get; set; }
        public int cena { get; set; }
        public int ocena { get; set; }
        public int naklad { get; set; }
        public string tytul { get; set; }
        
        List<Autor> autorzy;

        public Produkt()
        {
            autorzy = new List<Autor>();
        }
    }
}
