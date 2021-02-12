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
    class VozilaTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke vozila
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeVozila(string[] args)
        {
            string putanja = SaznajPutanjuDatVozila(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke vozila.");
            string vozilaDatotekaText = "";
            try
            {
                vozilaDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke vozila.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke vozila." +
                "\nZapočinjem učitavanje podataka datoteke vozila.");
            string[] nizRedakaVozila = Datoteka.PrepoznajRetkeIzDatoteke(vozilaDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaVozila = DohvatiIspravnaVozila(nizRedakaVozila, Datoteka.ATTR_SPLIT);
            Tvrtka.SetMyFolderLocation(Datoteka.DohvatiLokacijuMjestaDatoteke(putanja));
            if (Tvrtka.ListaVozila.Count > 0)
            {
                Console.WriteLine("Završeno učitavanje podataka vozila. " +
                    "(Ispravnih zapisa: " + Tvrtka.ListaVozila.Count + ")");
            }
            else
            {
                Console.WriteLine("Datoteka vozila ne sadrži niti jedan ispravan zapis " +
                    ".\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda koja služi da bi saznali putanju datoteke vozila
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatVozila(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-v")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "vozila")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja vraća retke odnosno ispravno unesena vozila
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa ispravnim zapisima</returns>
        private static List<Vozilo> DohvatiIspravnaVozila(string[] nizRedaka, char splitter)
        {
            TvrtkaSingleton tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            List<Vozilo> izlaznaLista = new List<Vozilo>();
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
                    Console.Write("ID vozila mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Neispravan naziv vozila! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Vrijeme punjenja baterije mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                {
                    brojGresaka++;
                    Console.Write("Domet vozila mora biti cijeli broj! --> ");
                }
                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiVozilo(privremeniObjekt));

                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o vozilu, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static Vozilo IzradiVozilo(string[] redak)
        {
            Vozilo vozilo = new Vozilo();
            vozilo.SetId(int.Parse(redak[0].Trim()));
            vozilo.SetNazivVozila(redak[1].Trim());
            vozilo.SetVrijemePunjenjaBaterije(int.Parse(redak[2].Trim()));
            vozilo.SetDomet(int.Parse(redak[3].Trim()));
            return vozilo;
        }
    }
}
