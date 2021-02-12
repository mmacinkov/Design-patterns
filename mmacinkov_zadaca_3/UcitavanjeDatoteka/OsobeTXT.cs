using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    class OsobeTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke osoba
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeOsoba(string[] args)
        {
            string putanja = SaznajPutanjuDatOsoba(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke osoba.");
            string osobeDatotekaText = "";
            try
            {
                osobeDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke osoba.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke osoba." +
                "\nZapočinjem učitavanje podataka datoteke osoba.");
            string[] nizRedakaOsobe = Datoteka.PrepoznajRetkeIzDatoteke(osobeDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaOsoba = DohvatiIspravneOsobe(nizRedakaOsobe, Datoteka.ATTR_SPLIT);
            if (Tvrtka.ListaOsoba.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke osoba. " +
                    "(Ispravnih zapisa: " + Tvrtka.ListaOsoba.Count + ")");
            else
            {
                Console.WriteLine("Datoteka osoba ne sadrži niti jedan ispravan zapis za osobu." +
                    "\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda koja služi za dohvat putanje datoteke osoba
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatOsoba(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-o")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "osobe")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ispravno unesenih osoba, redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu ispravnih podataka</returns>
        private static List<Osoba> DohvatiIspravneOsobe(string[] nizRedaka, char splitter)
        {
            List<Osoba> izlaznaLista = new List<Osoba>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (!Datoteka.ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                {
                    brojGresaka++;
                    Console.Write("Neispravan broj atributa! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                {
                    brojGresaka++;
                    Console.Write("ID osobe mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Neispravno ime i prezime osobe! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Ugovor mora biti cijeli broj! --> ");
                }
                else if (int.Parse(privremeniObjekt[2].Trim()) != 0 && int.Parse(privremeniObjekt[2].Trim()) != 1)
                {
                    brojGresaka++;
                    Console.Write("Ugovor mora biti 0 ili 1! --> ");
                }
                

                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiOsobu(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o osobi, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static Osoba IzradiOsobu(string[] redak)
        {
            Osoba tempOsoba = new Osoba();
            tempOsoba.SetId(int.Parse(redak[0].Trim()));
            tempOsoba.SetImePrezime(redak[1].Trim());
            tempOsoba.SetUgovor(int.Parse(redak[2].Trim()));
            return tempOsoba;
        }
    }
}
