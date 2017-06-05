using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    public class Produkt
    {
        public string tytul { get; set; }
        public int cena { get; set; }
        public int naklad { get; set; }
        public int strony { get; set; }

        List<Autor> autorzy;

        public Produkt()
        {
            autorzy = new List<Autor>();
        }

        public List<Autor> GetAutorzy()
        {
            return autorzy;
        }

        public void DodajAutora(Autor a)
        {
            autorzy.Add(a);
        } 
    }
}
