using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    class Czasopismo : Produkt
    {
        int czestotliwosc;

        public Czasopismo(int i)
        {
            czestotliwosc = i;
        }

        public int Czestotliwosc()
        {
            return czestotliwosc;
        }
    }
}
