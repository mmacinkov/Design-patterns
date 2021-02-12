using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Helperi;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    class LokacijeTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke lokacija
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeLokacije(string[] args)
        {
            string putanja = SaznajPutanjuDatLokacije(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke lokacije.");
            string lokacijeDatotekaText = "";
            try
            {
                lokacijeDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke lokacije.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke lokacije." +
                "\nZapočinjem učitavanje podataka datoteke lokacije.");
            string[] nizRedakaLokacije = Datoteka.PrepoznajRetkeIzDatoteke(lokacijeDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaLokacija = DohvatiIspravneLokacije(nizRedakaLokacije, Datoteka.ATTR_SPLIT);
            if (Tvrtka.ListaLokacija.Count > 0)
                Console.WriteLine("Završeno učitavanje podataka datoteke lokacije. " +
                    "(Ispravnih zapisa: " + Tvrtka.ListaLokacija.Count + ")");
            else
            {
                Console.WriteLine("Datoteka lokacije ne sadrži niti jedan ispravan zapis za osobu." +
                    "\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Metoda koja služi da bi saznali putanju datoteke lokacije
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatLokacije(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-l")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "lokacije")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ispravnih lokacija, odnosno redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa ispravnim elementima</returns>
        private static List<Lokacija> DohvatiIspravneLokacije(string[] nizRedaka, char splitter)
        {
            List<Lokacija> izlaznaLista = new List<Lokacija>();
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
                    Console.Write("ID lokacije mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Neispravan naziv lokacije! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Neispravna adresa lokacije! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[3]))
                {
                    brojGresaka++;
                    Console.Write("Neispravan GPS lokacije! --> ");
                }

                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiLokaciju(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o lokaciji, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static Lokacija IzradiLokaciju(string[] redak)
        {
            Lokacija tempLokacija = new Lokacija();
            tempLokacija.SetId(int.Parse(redak[0].Trim()));
            tempLokacija.SetNazivLokacije(redak[1].Trim());
            tempLokacija.SetAdresaLokacije(redak[2].Trim());
            tempLokacija.SetGps(redak[3].Trim());
            return tempLokacija;
        }
    }
}
