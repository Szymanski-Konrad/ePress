using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    //klasa pomocnicza służąca do generowania widoku zlecenia
    class DoWyswietlenia
    {
        public string tytul { get; set; }
        public int cena { get; set; }
        public int naklad { get; set; }
        public int strony { get; set; }
        public string stan { get; set; }
        public int iledni { get; set; }
        //public List<Autor> autorzy { get; set; }
    }
}
