using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.BuilderLokacijeVozila;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    public class LokacijeVozilaTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke lokacija vozila
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeLokacijeVozila(string[] args)
        {
            string putanja = SaznajPutanjuDatLokacijeVozila(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke lokacije vozila.");
            string lokacijeVozilaDatotekaText = "";
            try
            {
                lokacijeVozilaDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke lokacije vozila.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke lokacija vozila." +
                "\nZapočinjem učitavanje podataka datoteke lokacija vozila.");
            string[] nizRedakaLokacijeVozila = Datoteka.PrepoznajRetkeIzDatoteke(lokacijeVozilaDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaLokacijaVozila = DohvatiIspravneLokacijeVozila(nizRedakaLokacijeVozila, Datoteka.ATTR_SPLIT);
            Tvrtka.SetMyFolderLocation(Datoteka.DohvatiLokacijuMjestaDatoteke(putanja));
            if (Tvrtka.ListaLokacijaVozila.Count > 0)
            {
                Console.WriteLine("Završeno učitavanje podataka lokacije vozila. " +
                    "(Ispravnih zapisa: " + Tvrtka.ListaLokacijaVozila.Count + ")");
            }
            else
            {
                Console.WriteLine("Datoteka lokacije vozila ne sadrži niti jedan ispravan zapis " +
                    ".\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// Metoda koja služi za saznavanje putanje datoteke lokacija vozila
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatLokacijeVozila(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-k")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "kapaciteti")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvat ispravnih lokacija vozila, redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa ispravnim podacima</returns>
        private static List<LokacijeVozila> DohvatiIspravneLokacijeVozila(string[] nizRedaka, char splitter)
        {
            TvrtkaSingleton tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            List<LokacijeVozila> izlaznaLista = new List<LokacijeVozila>();
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
                else if (!PostojiLokacija(privremeniObjekt[0]))
                {
                    brojGresaka++;
                    Console.Write("Unesena loakcija ne postoji! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("ID vozila mora biti cijeli broj! --> ");
                }
                else if (!PostojiVozilo(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Uneseno vozilo ne postoji! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Broj mjesta mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                {
                    brojGresaka++;
                    Console.Write("Broj raspoloživih mjesta mora biti cijeli broj! --> ");
                }
                else if (int.Parse(privremeniObjekt[3]) > int.Parse(privremeniObjekt[2]))
                {
                    brojGresaka++;
                    Console.Write("Nemoguće je da postoji više raspoloživih mjesta nego što je ukupno mjesta! --> ");
                }
                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiLokacijuVozila(privremeniObjekt));

                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o lokaciji vozila, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static LokacijeVozila IzradiLokacijuVozila(string[] redak)
        {
            List<int> Id = IzradiListuLokacijaVozila(redak[0].Trim());
            List<int> Id2 = IzradiListuLokacijaVozila(redak[1].Trim());
            int brojMjesta = int.Parse(redak[2].Trim());
            int raspolozivihMjesta = int.Parse(redak[3].Trim());
            ILokacijeVozilaBuilder builder = new LokacijeVozilaConcreteBuilder();
            LokacijeVozilaBuildDirector director = new LokacijeVozilaBuildDirector(builder);
            return director.Construct(Id, Id2, brojMjesta, raspolozivihMjesta);
        }

        private static List<int> IzradiListuLokacijaVozila(string zapis)
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

        private static bool PostojiLokacija(string idVrsta)
        {
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            int intVrsta = int.Parse(idVrsta.Trim());
            Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == intVrsta);
            if (lokacija == null)
            {
                return false;
            }
            return true;
        }
    }
}
