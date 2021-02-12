using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.BuilderCjenik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    class CjenikTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke cjenik
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeCjenik(string[] args)
        {
            string putanja = SaznajPutanjuDatCjenik(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke cjenik.");
            string cjenikDatotekaText = "";
            try
            {
                cjenikDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke cjenik.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke cjenik." +
                "\nZapočinjem učitavanje podataka datoteke cjenik.");
            string[] nizRedakaCjenik = Datoteka.PrepoznajRetkeIzDatoteke(cjenikDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaCjenika = DohvatiIspravanCjenik(nizRedakaCjenik, Datoteka.ATTR_SPLIT);
            Tvrtka.SetMyFolderLocation(Datoteka.DohvatiLokacijuMjestaDatoteke(putanja));
            if (Tvrtka.ListaCjenika.Count > 0)
            {
                Console.WriteLine("Završeno učitavanje podataka cjenika. " +
                    "(Ispravnih zapisa: " + Tvrtka.ListaCjenika.Count + ")");
            }
            else
            {
                Console.WriteLine("Datoteka cjenik ne sadrži niti jedan ispravan zapis " +
                    ".\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje putanje datoteke cjenik
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatCjenik(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-c")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "cjenik")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ispravnog cjenika, redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa ispravnim elementima</returns>
        private static List<Cjenik> DohvatiIspravanCjenik(string[] nizRedaka, char splitter)
        {
            TvrtkaSingleton tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            List<Cjenik> izlaznaLista = new List<Cjenik>();
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
                else if (!PostojiVozilo(privremeniObjekt[0]))
                {
                    brojGresaka++;
                    Console.Write("Uneseno vozilo ne postoji! --> ");
                }
                else if (!Datoteka.IspravanDouble(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Cijena najma mora biti broj! --> ");
                }
                else if (!Datoteka.IspravanDouble(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Cijena po satu mora biti broj! --> ");
                }
                else if (!Datoteka.IspravanDouble(privremeniObjekt[3]))
                {
                    brojGresaka++;
                    Console.Write("Cijena po km mora biti broj! --> ");
                }
                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiCjenik(privremeniObjekt));

                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o cjeniku, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static Cjenik IzradiCjenik(string[] redak)
        {
            List<int> tempId = IzradiListuCjenik(redak[0].Trim());
            double tempNajam = double.Parse(redak[1].Trim());
            double tempPoSatu = double.Parse(redak[2].Trim());
            double tempPoKm = double.Parse(redak[3].Trim());
            ICjenikBuilder builder = new CjenikConcreteBuilder();
            CjenikBuildDirector director = new CjenikBuildDirector(builder);
            return director.Construct(tempId, tempNajam, tempPoSatu, tempPoKm);
        }

        private static List<int> IzradiListuCjenik(string zapis)
        {
            List<int> izlaznaLista = new List<int>();
            if (zapis == "")
            {
                return izlaznaLista;
            }
            string[] tempLista = zapis.Split(',');
            if (tempLista.Length == 0)
            {
                return izlaznaLista;
            }
            for (int i = 0; i < tempLista.Length; i++)
            {
                if (tempLista[i].Trim() == "")
                {

                }
                if (!int.TryParse(tempLista[0].Trim(), out int tempId)) 
                {
                    
                }
                else
                {
                    izlaznaLista.Add(tempId);
                }
            }
            return izlaznaLista;
        }

        private static bool PostojiVozilo(string idVrsta)
        {
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            int intVrsta = int.Parse(idVrsta.Trim());
            Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == intVrsta);
            if (vozilo == null)
            {
                return false;
            }
            return true;
        }
    }
}
