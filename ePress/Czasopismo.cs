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
        int datarozpoczecia;

        public Czasopismo(int i, int d)
        {
            czestotliwosc = i;
            datarozpoczecia = d;
        }

        public int Czestotliwosc()
        {
            return czestotliwosc;
        }

        public int DataRozpoczecia()
        {
            return datarozpoczecia;
        }
    }
}
