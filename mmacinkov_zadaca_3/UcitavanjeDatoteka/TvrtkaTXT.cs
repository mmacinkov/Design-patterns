using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.BuilderTvrtka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    class TvrtkaTXT
    {
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke tvrtka
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeTvrtka(string[] args)
        {
            string putanja = SaznajPutanjuDatTvrtka(args);
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke tvrtka.");
            string tvrtkaDatotekaText = "";
            try
            {
                tvrtkaDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke tvrtka.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            tvrtkaSingleton.SetCompositeTvrtka();

            Console.WriteLine("Završeno čitanje datoteke tvrtka." +
                "\nZapočinjem učitavanje podataka datoteke tvrtka.");
            string[] nizRedakaTvrtke = Datoteka.PrepoznajRetkeIzDatoteke(tvrtkaDatotekaText, Datoteka.LINE_SPLIT);
            tvrtkaSingleton.ListaTvrtki = DohvatiIspravneTvrtke(nizRedakaTvrtke, Datoteka.ATTR_SPLIT);
            if (tvrtkaSingleton.ListaTvrtki.Count > 0)
            {
                Console.WriteLine("Završeno učitavanje podataka datoteke tvrtka. " +
                    "(Ispravnih zapisa: " + tvrtkaSingleton.ListaTvrtki.Count + ")");
            }
            else
            {
                Console.WriteLine("Datoteka tvrtki ne sadrži niti jedan ispravan zapis za tvrtku." +
                    "\nGasim program.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda koja služi za dohvat putanje datoteke tvrtka
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatTvrtka(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-os")
                {
                    return args[i + 1].Trim();
                }
                else if (args[i].Trim() == "struktura")
                {
                    return args[i + 1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ispravno unesenih tvrtki, redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu ispravnih podataka</returns>
        private static List<Tvrtka> DohvatiIspravneTvrtke(string[] nizRedaka, char splitter)
        {
            List<Tvrtka> izlaznaLista = new List<Tvrtka>();
            string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                int brojGresaka = 0;
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                /*if (!Datoteka.ProvjeriBrojAtributa(privremeniObjekt, atributiZaglavlja.Length))
                {
                    brojGresaka++;
                    Console.Write("Neispravan broj atributa! --> ");
                }
                else*/ if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                {
                    brojGresaka++;
                    Console.Write("ID tvrtke mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[1]))
                {
                    brojGresaka++;
                    Console.Write("Neispravan naziv tvrtke! --> ");
                }
                else if (!Datoteka.IspravanInt(privremeniObjekt[2]) && privremeniObjekt[2] == null)
                {
                    brojGresaka++;
                    Console.Write("Nadređena jedinica mora biti cijeli broj! --> ");
                }
                else if (!Datoteka.IspravanString(privremeniObjekt[3]) && privremeniObjekt[3] == null)
                {
                    brojGresaka++;
                    Console.Write("Neispravno unesene lokacije! --> ");
                }
                
                
                /*else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                {
                    brojGresaka++;
                    Console.Write("Lokacija mora biti cijeli broj! --> ");
                }*/
                

                if (brojGresaka != 0)
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    izlaznaLista.Add(IzradiTvrtku(privremeniObjekt));
                }
            }
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o tvrtci, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        private static Tvrtka IzradiTvrtku(string[] redak)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();

            Tvrtka tvrtka = new Tvrtka();
            tvrtka.SetIDTvrtke(int.Parse(redak[0].Trim()));
            tvrtka.SetNaziv(redak[1].Trim());
            if (redak[3].Trim() != null && redak[3].Trim() != "")
            {
                string[] idLokacije = redak[3].Trim().Split(',');
                for (int i = 0; i < idLokacije.Length; i++)
                {
                    var lokacija = tvrtkaSingleton.PronadiLokaciju(int.Parse(idLokacije[i]));
                    if (lokacija != null)
                    {
                        tvrtka.Lokacije.Add(lokacija);
                    }
                }
            }
            if (redak[2].Trim() == "")
            {
                tvrtka.SetNadredena(null);
                tvrtkaSingleton.GetCompositeTvrtka().DodajDijete(tvrtka);
            }
            else
            {
                var nadredena = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(redak[2].Trim())) as Tvrtka;
                tvrtka.SetNadredena(nadredena);
                nadredena.DodajDijete(tvrtka);
            }
            return tvrtka;
        }

        /*private static List<int> IzradiListuLokacijaVozila(string zapis)
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
        }*/

        /*private static void IzradiListuLokacija(string zapis)
        {
            Tvrtka tvrtka = new Tvrtka();
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            //var izlaznaLista = tvrtka.Lokacije;
            if (zapis.Trim() != null && zapis.Trim() != "")
            {
                string[] idLokacije = zapis.Trim().Split(',');
                for (int i = 0; i < idLokacije.Length; i++)
                {
                    var lokacija = tvrtkaSingleton.PronadiLokaciju(int.Parse(idLokacije[i]));
                    if (lokacija != null)
                    {
                        tvrtka.Lokacije.Add(lokacija);
                    }
                }
            }*/

            
            /*if (zapis == "")
                return izlaznaLista;
            string[] tempObjektLokacije = zapis.Split(',');
            if (tempObjektLokacije.Length == 0)
                return izlaznaLista;
            for (int i = 0; i < tempObjektLokacije.Length; i++)
            {
                if (tempObjektLokacije[i].Trim() == "") { } 
                else
                {
                    string[] lokacija = tempObjektLokacije[i].Trim().Split(',');
                    if (lokacija.Length != 1) { } 
                    else
                    {
                        if (!int.TryParse(lokacija[0].Trim(), out int tempIdTvrtka)) { }
                            else
                            {
                                izlaznaLista.Add(
                                    (tempIdTvrtka)
                                );
                            }
                    }
                }
            }
            
            return izlaznaLista;*/
        }
    }
