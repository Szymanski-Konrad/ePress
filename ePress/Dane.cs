using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePress
{
    //klasa do zapisywania danych na dysk oraz wczytywanie danych przy uruchomieniu programu
    class Dane
    {
        public Dane()
        {

        }

        //zapisywanie wszystkich autorów do pliku autorzy.txt
        public void ZapiszAutorow(List<Autor> autorzy) 
        {
            using (StreamWriter sw = File.CreateText("autorzy.txt"))
            {
                foreach (Autor a in autorzy)
                {
                    sw.WriteLine(a.coPisze + ";" + a.dzielo + ";" + a.Imie + ";" + a.konto + ";" + a.Nazwisko + ";" + a.sprzedaz + ";" + a.PokazUmowe().stawka + ";" + a.PokazUmowe().GetType().Name);
                }
            }
        }

        //wczytywanie wszystkich autorów z pliku autorzy.txt
        public List<Autor> WczytajAutorow()
        {
            List<Autor> autorzy = new List<Autor>();
            string[] lines = File.ReadAllLines("autorzy.txt");
            foreach (string s in lines)
            {
                string[] tmp = s.Split(';');
                Autor a = new Autor() { coPisze = tmp[0], dzielo = tmp[1], Imie = tmp[2], konto = Int32.Parse(tmp[3]), Nazwisko = tmp[4], sprzedaz = Int32.Parse(tmp[5]) };
                if (tmp[7] == "ODZielo") a.DodajUmowe(new ODzielo() { stawka = Int32.Parse(tmp[6]) });
                else 
                    if (tmp[7] == "OPrace") a.DodajUmowe(new OPrace() { stawka = Int32.Parse(tmp[6]) });
                    else a.DodajUmowe(new Umowa() { stawka = Int32.Parse(tmp[6]) });
                autorzy.Add(a);
            }
            return autorzy;
        }

        //zapisywanie stanu drukarni do osobnych plików
        public void ZapiszDrukarnie(List<Drukarnia> drukarnie)
        {
            int x = 1;
            foreach (Drukarnia d in drukarnie)
            {
                using (StreamWriter sw = File.CreateText("drukarnia" + x + ".txt"))
                {
                    sw.WriteLine("Codrukuje");
                    foreach (string s in d.GetCoDrukuje())
                    {
                        sw.Write(s + ";");
                    }
                    sw.WriteLine();
                    sw.WriteLine("Zlecenia");
                    foreach (Zlecenie z in d.GetZlecenia())
                    {
                        sw.WriteLine(z.GetProdukt().cena + ";" + z.GetProdukt().naklad + ";" + z.GetProdukt().strony + ";" + z.GetProdukt().tytul + ";" + z.ileDni + ";" + z.stan + ";" + z.GetProdukt().GetType().Name);
                    }
                    sw.WriteLine("Staty");
                    sw.WriteLine(d.jakosc + ";" + d.wydajnosc + ";" + d.zajeta + ";");
                }
                x++;
            }
        }

        //wczytywanie stanu drukarni
        public List<Drukarnia> WczytajDrukarnie()
        {
            List<Drukarnia> drukarnie = new List<Drukarnia>();
            for(int i = 1; i <= 3; i++)
            {
                if (File.Exists("drukarnia" + i.ToString() + ".txt"))
                {
                    Drukarnia d = new Drukarnia();
                    string[] lines = File.ReadAllLines("drukarnia" + i.ToString() + ".txt");
                    int x = 1, l = lines.Length;
                    string[] tmp = lines[x].Split(';');
                    foreach (string s in tmp) d.DodajCoDrukuje(s);
                    while (lines[x] != "Zlecenia") x++;
                    x++;
                    while (lines[x] != "Staty")
                    {
                        tmp = lines[x].Split(';');
                        Zlecenie z = new Zlecenie() { ileDni = Int32.Parse(tmp[4]), stan = tmp[5] };
                        z.UstawDrukarnie(d);
                        if (tmp[6] == "Tygodnik") z.UstawProdukt(new Tygodnik(7) { cena = Int32.Parse(tmp[0]), naklad = Int32.Parse(tmp[1]), strony = Int32.Parse(tmp[2]), tytul = tmp[3] });
                           else if (tmp[6] == "Miesiecznik")
                                    z.UstawProdukt(new Miesiecznik(30) { cena = Int32.Parse(tmp[0]), naklad = Int32.Parse(tmp[1]), strony = Int32.Parse(tmp[2]), tytul = tmp[3] });
                                else if (tmp[6] == "Romans")
                                        z.UstawProdukt(new Romans() { cena = Int32.Parse(tmp[0]), naklad = Int32.Parse(tmp[1]), strony = Int32.Parse(tmp[2]), tytul = tmp[3] });
                                    else if (tmp[6] == "Sensacja")
                                            z.UstawProdukt(new Sensacja() { cena = Int32.Parse(tmp[0]), naklad = Int32.Parse(tmp[1]), strony = Int32.Parse(tmp[2]), tytul = tmp[3] });
                                        else z.UstawProdukt(new Album() { cena = Int32.Parse(tmp[0]), naklad = Int32.Parse(tmp[1]), strony = Int32.Parse(tmp[2]), tytul = tmp[3] });
                        d.DodajZlecenie(z);
                        x++;
                    }
                    x++;
                    tmp = lines[x].Split(';');
                    d.jakosc = Int32.Parse(tmp[0]);
                    d.wydajnosc = Int32.Parse(tmp[1]);
                    d.zajeta = Int32.Parse(tmp[2]);
                    drukarnie.Add(d);
                }
            }
            return drukarnie;
        }

        //zapisywanie postępu działania wydawnictwa
        public void ZapiszStanWydawnictwa(Wydawnictwo w)
        {
            using (StreamWriter sw = File.CreateText("info.txt"))
            {
                sw.WriteLine(w.Dzien + ";" + w.Saldo + ";");
            }
        }

        //wczytywanie postępu działania wydawnictwa
        public void WczytajStanWydawnictwa(Wydawnictwo w)
        {
            if (File.Exists("info.txt"))
            {
                string line = File.ReadAllText("info.txt");
                string[] tmp = line.Split(';');
                w.Dzien = Int32.Parse(tmp[0]);
                w.Saldo = Int32.Parse(tmp[1]);
            }
        }
    }
}