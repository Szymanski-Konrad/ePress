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
        public int konto { get; set; }                          // get i set to są akcesorami, i dzięki nim można znacząco skrócić długość kodu
        public int sprzedaz { get; set; }                       // kompilator i tak ustala te pola jako prywatne oraz dodaje metody dostępowe
        public string coPisze { get; set; }
        public string dzielo { get; set; }
        Umowa umowa { get; set; }

        public Autor()
        {
            umowa = new Umowa();
            dzielo = " ";
        }

        // podpisanie umowy przez autora
        public void DodajUmowe(Umowa u)
        {
            umowa = u;
        }

        //wgląd w umowę autora
        public Umowa PokazUmowe()
        {
            return umowa;
        }
     }
}
