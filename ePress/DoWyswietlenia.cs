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
        public class DlaDrukarni
        {
            public string tytul { get; set; }
            public int cena { get; set; }
            public int naklad { get; set; }
            public int strony { get; set; }
            public string stan { get; set; }
            public int iledni { get; set; }
            public string typ { get; set; }
        }

        public class DlaAutorow
        {
            public string imie { get; set; }
            public string nazwisko { get; set; }
            public string umowa { get; set; }
            public string wynagrodzenie { get; set; }
            public int sprzedaz { get; set; }
            public int zysk { get; set; }
            public string copisze { get; set; }
            public string dzielo { get; set; }
        }

    }
}
