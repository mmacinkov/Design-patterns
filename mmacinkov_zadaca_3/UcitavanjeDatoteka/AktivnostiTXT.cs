using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    class AktivnostiTXT
    {
        public static List<Aktivnost> izlaznaLista = new List<Aktivnost>();
        /// <summary>
        /// Metoda koja služi za učitavanje podataka iz datoteke aktivnosti
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajPodatkeDatotekeAktivnosti(string[] args)
        {
            string putanja = SaznajPutanjuDatAktivnosti(args);
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            Console.WriteLine("\nZapočinjem čitanje datoteke aktivnosti.");
            string aktivnostiDatotekaText = "";
            try
            {
                aktivnostiDatotekaText = File.ReadAllText(putanja);
            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke aktivnosti.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Završeno čitanje datoteke aktivnosti." +
                "\nZapočinjem učitavanje podataka datoteke aktivnosti.");
            string[] nizRedakaAktivnosti = Datoteka.PrepoznajRetkeIzDatoteke(aktivnostiDatotekaText, Datoteka.LINE_SPLIT);
            Tvrtka.ListaAktivnosti = DohvatiIspravneAktivnosti(nizRedakaAktivnosti, Datoteka.ATTR_SPLIT, args);
            Tvrtka.SetMyFolderLocation(Datoteka.DohvatiLokacijuMjestaDatoteke(putanja));
            if (Tvrtka.ListaAktivnosti.Count > 0)
            {
                int broj = Tvrtka.ListaAktivnosti.Count - 1;
                Console.WriteLine("Završeno učitavanje podataka aktivnosti. " +
                    "(Ispravnih zapisa: " + broj + ")");
            }
            else
            {
                Console.WriteLine("Datoteka aktivnosti ne sadrži niti jedan ispravan zapis" + ".\nGasim program.");
                Console.ReadLine(); 
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda koja služi za saznavanje putanje datoteke aktivnosti
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća putanju</returns>
        private static string SaznajPutanjuDatAktivnosti(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Trim() == "-s")
                {
                    return args[i + 1].Trim();
                }
                else if (args[1].Trim() == "DZ_3_aktivnosti_1.txt")
                {
                    return args[1].Trim();
                }
                else if (args[i].Trim() == "aktivnosti")
                {
                    return args[i + 1].Trim();
                }
                else if (args[1].Trim().Contains("aktivnosti"))
                {
                    return args[1].Trim();
                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje ispravnih aktivnosti, redaka
        /// </summary>
        /// <param name="nizRedaka"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa ispravnim podacima</returns>
        public static List<Aktivnost> DohvatiIspravneAktivnosti(string[] nizRedaka, char splitter, string[] args)
        {
            
        TvrtkaSingleton tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
        
        string[] atributiZaglavlja = nizRedaka[0].Split(splitter);
            for (int i = 1; i < nizRedaka.Length; i++)
            {
                
                string[] privremeniObjekt = nizRedaka[i].Split(splitter);
                if (privremeniObjekt.Length == 6)
                {
                    int brojGresaka = 0;
                    if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                    {
                        brojGresaka++;
                        Console.Write("ID aktivnosti mora biti cijeli broj! --> ");
                    }
                    else if (!Datoteka.IspravanDatum(privremeniObjekt[1]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni datum nije ispravnog formata! --> ");
                    }
                    else if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(privremeniObjekt[1], izlaznaLista, args))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("ID korisnika mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiKorisnik(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni korisnik ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("ID lokacije mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiLokacija(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("Unesena lokacija ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("ID vozila mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiVozilo(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseno vozilo ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[5]))
                    {
                        brojGresaka++;
                        Console.Write("Broj kilometara mora biti cijeli broj! --> ");
                    }
                    else if (!Datoteka.ProvjeravaJeLiBrojKmVeciOdDometa(int.Parse(privremeniObjekt[4]), int.Parse(privremeniObjekt[5])))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.ProvjeraPostojiLiBrojMjesta(int.Parse(privremeniObjekt[3]), int.Parse(privremeniObjekt[4])))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.ProvjeraMozeLiKorisnikVratitiVoziloNaLokaciju(int.Parse(privremeniObjekt[3]), int.Parse(privremeniObjekt[4]), int.Parse(privremeniObjekt[2])))
                    {
                        brojGresaka++;
                    }

                    if (brojGresaka != 0)
                    {
                        Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                    }
                    else
                    {
                        izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                    }
                }
                else if (privremeniObjekt.Length == 2)
                {
                    int brojGresaka = 0;
                    if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                    {
                        brojGresaka++;
                        Console.Write("ID aktivnosti mora biti cijeli broj! --> ");
                    }
                    else if (int.Parse(privremeniObjekt[0]) == 0)
                    {
                        if (!Datoteka.IspravanDatum(privremeniObjekt[1]))
                        {
                            brojGresaka++;
                            Console.Write("Uneseni datum nije ispravnog formata! --> ");
                        }
                        else if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(privremeniObjekt[1], izlaznaLista, args))
                        {
                            brojGresaka++;
                            Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti! --> ");
                        }
                        if (brojGresaka != 0)
                        {
                            Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                        }
                        else
                        {
                            izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                        }
                    }
                    else if (int.Parse(privremeniObjekt[0]) == 6)
                    {
                        if (privremeniObjekt[1].Trim().Length != 9 && privremeniObjekt[1].Trim().Length != 11 && privremeniObjekt[1].Trim().Length != 16 && privremeniObjekt[1].Trim().Length != 18)
                        {
                            brojGresaka++;
                            Console.Write("Pogrešno unesene naredbe! --> ");
                        }
                        else
                        {

                        }

                    }
                    else if (int.Parse(privremeniObjekt[0]) == 7)
                    {
                        if (privremeniObjekt[1].Trim().Length != 31 && privremeniObjekt[1].Trim().Length != 33)
                        {
                            brojGresaka++;
                            Console.Write("Pogrešno unesene naredbe! --> ");
                        }
                        else
                        {

                        }
                    }
                    else if (int.Parse(privremeniObjekt[0]) == 8)
                    {
                        if (privremeniObjekt[1].Trim().Length != 31 && privremeniObjekt[1].Trim().Length != 33)
                        {
                            brojGresaka++;
                            Console.Write("Pogrešno unesene naredbe! --> ");
                        }
                        else
                        {
                            

                        }
                    }
                    else if (int.Parse(privremeniObjekt[0]) == 10)
                    {
                        
                    }
                            
                    else if (int.Parse(privremeniObjekt[0]) == 11)
                    {

                    }
                    else if (int.Parse(privremeniObjekt[0]) != 0 || int.Parse(privremeniObjekt[0]) != 6 || int.Parse(privremeniObjekt[0]) != 7 || int.Parse(privremeniObjekt[0]) != 8 || int.Parse(privremeniObjekt[0]) != 9 || int.Parse(privremeniObjekt[0]) != 10 || int.Parse(privremeniObjekt[0]) != 11)
                    {
                        brojGresaka++;
                        Console.Write("Unjeli ste pogrešan broj aktivnosti!");
                    }

                    if (brojGresaka != 0)
                    {
                        Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                    }
                    else
                    {
                        izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                    }

                }
                else if (privremeniObjekt.Length == 1)
                {
                    int brojGresaka = 0;
                    if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                    {
                        brojGresaka++;
                        Console.Write("ID aktivnosti mora biti cijeli broj! --> ");
                    }
                    else if (int.Parse(privremeniObjekt[0]) != 9)
                    {
                        brojGresaka++;
                        Console.Write("Pogrešno unesen broj aktivnosti! Mora iznositi 9! --> ");
                    }
                    if (brojGresaka != 0)
                    {
                        Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                    }
                    else
                    {
                        izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                    }
                }
                else if (privremeniObjekt.Length == 5)
                {
                    int brojGresaka = 0;
                    if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                    {
                        brojGresaka++;
                        Console.Write("ID aktivnosti mora biti cijeli broj! --> ");
                    }
                    else if (!Datoteka.IspravanDatum(privremeniObjekt[1]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni datum nije ispravnog formata! --> ");
                    }
                    else if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(privremeniObjekt[1], izlaznaLista, args))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("ID korisnika mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiKorisnik(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni korisnik ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("ID lokacije mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiLokacija(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("Unesena lokacija ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("ID vozila mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiVozilo(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseno vozilo ne postoji! --> ");
                    }
                    else if (int.Parse(privremeniObjekt[0]) == 2)
                    {
                        //Console.WriteLine(int.Parse(privremeniObjekt[0]));
                        if (!Datoteka.ProvjeriPostojiLiKorisnikSPosudbom(int.Parse(privremeniObjekt[2]), int.Parse(privremeniObjekt[4])))
                        {
                            brojGresaka++;
                        }
                        else
                        {

                        }
                    }
                    else if (!Datoteka.ProvjeraPostojeLiRaspolozivaVozila(int.Parse(privremeniObjekt[3]), int.Parse(privremeniObjekt[4])))
                    {
                        brojGresaka++;
                    }
                    

                    if (brojGresaka != 0)
                    {
                        Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                    }
                    else
                    {
                        izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                    }
                }
                else if (privremeniObjekt.Length == 7)
                {
                    int brojGresaka = 0;
                    if (!Datoteka.IspravanInt(privremeniObjekt[0]))
                    {
                        brojGresaka++;
                        Console.Write("ID aktivnosti mora biti cijeli broj! --> ");
                    }
                    else if (!Datoteka.IspravanDatum(privremeniObjekt[1]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni datum nije ispravnog formata! --> ");
                    }
                    else if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(privremeniObjekt[1], izlaznaLista, args))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("ID korisnika mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiKorisnik(privremeniObjekt[2]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseni korisnik ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("ID lokacije mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiLokacija(privremeniObjekt[3]))
                    {
                        brojGresaka++;
                        Console.Write("Unesena lokacija ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("ID vozila mora biti cijeli broj! --> ");
                    }
                    else if (!PostojiVozilo(privremeniObjekt[4]))
                    {
                        brojGresaka++;
                        Console.Write("Uneseno vozilo ne postoji! --> ");
                    }
                    else if (!Datoteka.IspravanInt(privremeniObjekt[5]))
                    {
                        brojGresaka++;
                        Console.Write("Broj kilometara mora biti cijeli broj! --> ");
                    }
                    else if (!Datoteka.IspravanString(privremeniObjekt[6]))
                    {
                        brojGresaka++;
                        Console.Write("Neispravno unesen opis problema! --> ");
                    }
                    else if (!Datoteka.ProvjeravaJeLiBrojKmVeciOdDometa(int.Parse(privremeniObjekt[4]), int.Parse(privremeniObjekt[5])))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.ProvjeraPostojiLiBrojMjesta(int.Parse(privremeniObjekt[3]), int.Parse(privremeniObjekt[4])))
                    {
                        brojGresaka++;
                    }
                    else if (!Datoteka.ProvjeraMozeLiKorisnikVratitiVoziloNaLokaciju(int.Parse(privremeniObjekt[3]), int.Parse(privremeniObjekt[4]), int.Parse(privremeniObjekt[2])))
                    {
                        brojGresaka++;
                    }

                    if (brojGresaka != 0)
                    {
                        Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                    }
                    else
                    {
                        izlaznaLista.Add(IzradiAktivnost(privremeniObjekt));

                    }
                }
                else if (privremeniObjekt.Length != 1 || privremeniObjekt.Length != 2 || privremeniObjekt.Length != 5 || privremeniObjekt.Length !=6 || privremeniObjekt.Length != 7)
                {
                    Console.Write("Neispravan broj atributa! --> ");
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }
                else
                {
                    Ispis.IspisGreskeRedak(i, nizRedaka[i]);
                }

            }
                
            return izlaznaLista;
        }

        /// <summary>
        /// Metoda koja služi za spremanje podataka o aktivnosti, iz redaka
        /// </summary>
        /// <param name="redak"></param>
        /// <returns>Vraća spremljeni redak</returns>
        public static Aktivnost IzradiAktivnost(string[] redak)
        {
            Aktivnost tempAktivnost = new Aktivnost();
            if (redak.Length == 6)
            {  
                int Id = int.Parse(redak[0].Trim());
                string Datum = redak[1].Trim();
                List<int> IdKorisnika = IzradiListuLokacijaVozila(redak[2].Trim());
                List<int> IdLokacije = IzradiListuLokacijaVozila(redak[3].Trim());
                List<int> IdVozila = IzradiListuLokacijaVozila(redak[4].Trim());
                int brojKm = int.Parse(redak[5].Trim());
                tempAktivnost.SetIdAktivnosti(Id);
                tempAktivnost.SetDatum(Datum);
                tempAktivnost.SetIdKorisnika(IdKorisnika);
                tempAktivnost.SetIdLokacije(IdLokacije);
                tempAktivnost.SetIdVrsteVozila(IdVozila);
                tempAktivnost.SetBrojKm(brojKm);
                return tempAktivnost;
            }
            else if (redak.Length == 1)
            {
                int Id = int.Parse(redak[0].Trim());
                tempAktivnost.SetIdAktivnosti(Id);
                return tempAktivnost;
            }
            else if (redak.Length == 2)
            {
                int Id = int.Parse(redak[0].Trim());
                if (Id == 0)
                {
                    string Datum = redak[1].Trim();
                    tempAktivnost.SetIdAktivnosti(Id);
                    tempAktivnost.SetDatum(Datum);
                    return tempAktivnost;
                }
                else if (Id == 6)
                {
                    string Opis = redak[1].Trim();
                    tempAktivnost.SetIdAktivnosti(Id);
                    tempAktivnost.SetOpisAktivnosti(Opis);
                    return tempAktivnost;
                }
                else if (Id == 7)
                {
                    string Opis = redak[1].Trim();
                    tempAktivnost.SetIdAktivnosti(Id);
                    tempAktivnost.SetOpisAktivnosti(Opis);
                    return tempAktivnost;
                }
                else if (Id == 8)
                {
                    string Opis = redak[1].Trim();
                    tempAktivnost.SetIdAktivnosti(Id);
                    tempAktivnost.SetOpisAktivnosti(Opis);
                    return tempAktivnost;
                }
                else if (Id == 10)
                {
                    string Opis = redak[1].Trim();
                    tempAktivnost.SetIdAktivnosti(Id);
                    tempAktivnost.SetOpisAktivnosti(Opis);
                    return tempAktivnost;
                }
                else
                {
                    return tempAktivnost;
                }

            }
            else if (redak.Length == 5)
            {
                int Id = int.Parse(redak[0].Trim());
                string Datum = redak[1].Trim();
                List<int> IdKorisnika = IzradiListuLokacijaVozila(redak[2].Trim());
                List<int> IdLokacije = IzradiListuLokacijaVozila(redak[3].Trim());
                List<int> IdVozila = IzradiListuLokacijaVozila(redak[4].Trim());
                tempAktivnost.SetIdAktivnosti(Id);
                tempAktivnost.SetDatum(Datum);
                tempAktivnost.SetIdKorisnika(IdKorisnika);
                tempAktivnost.SetIdLokacije(IdLokacije);
                tempAktivnost.SetIdVrsteVozila(IdVozila);
                return tempAktivnost;
            }
            else if (redak.Length == 7)
            {
                int Id = int.Parse(redak[0].Trim());
                string Datum = redak[1].Trim();
                List<int> IdKorisnika = IzradiListuLokacijaVozila(redak[2].Trim());
                List<int> IdLokacije = IzradiListuLokacijaVozila(redak[3].Trim());
                List<int> IdVozila = IzradiListuLokacijaVozila(redak[4].Trim());
                int brojKm = int.Parse(redak[5].Trim());
                string opisProblema = redak[6].Trim();
                tempAktivnost.SetIdAktivnosti(Id);
                tempAktivnost.SetDatum(Datum);
                tempAktivnost.SetIdKorisnika(IdKorisnika);
                tempAktivnost.SetIdLokacije(IdLokacije);
                tempAktivnost.SetIdVrsteVozila(IdVozila);
                tempAktivnost.SetBrojKm(brojKm);
                tempAktivnost.SetOpisProblema(opisProblema);
                return tempAktivnost;
            }
            else
            {
                return tempAktivnost;
            }
        }

        public static List<int> IzradiListuLokacijaVozila(string zapis)
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

        private static bool PostojiKorisnik(string idKorisnik)
        {
            TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            int intKorisnik = int.Parse(idKorisnik.Trim());
            Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == intKorisnik);
            if (osoba == null)
            {
                return false;
            }
            return true;
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
