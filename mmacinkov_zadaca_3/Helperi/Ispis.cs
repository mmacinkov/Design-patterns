using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.UcitavanjeDatoteka;
using mmacinkov_zadaca_3.Uzorci.ChainOfResponsibility;
using mmacinkov_zadaca_3.Uzorci.Decorator;
using mmacinkov_zadaca_3.Uzorci.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Helperi
{
    public class Ispis
    {
        public static int Brojac = 0;
        public static int FormatTekst = 0;
        public static int FormatCijeli = 0;
        public static decimal Dugovanje = 0;
        public static string ispisDugovanja = "";
        public static int BrojRacuna = 0;
        public static List<Aktivnost> listaAktivnosti = new List<Aktivnost>();
        public static List<Tuple<int, double>> listaRacuna = new List<Tuple<int, double>>();
        public static List<Tuple<int, double, string, string, string, string>> listaRacunaZa10 = new List<Tuple<int, double, string, string, string, string>>();
        /// <summary>
        /// Metoda koja služi za prikaz glavnog izbornika
        /// </summary>
        public static void PrikazGlavnogIzbornika(string[] args)
        {
            bool izlaz = false;
            while (!izlaz)
            {
                Console.WriteLine("\n\n\n ------ GLAVNI IZBORNIK ------- ");
                Console.WriteLine("1. Pregled raspoloživih vozila odabrane vrste na odabranoj lokaciji");
                Console.WriteLine("2. Najam odabrane vrste vozila na odabranoj lokaciji");
                Console.WriteLine("3. Pregled raspoloživih mjesta odabrane vrste vozila za odabranu lokaciju");
                Console.WriteLine("4. Vraćanje (neispravnog/ispravnog) vozila na odabranu lokaciju uz unos ukupnog broj kilometara te ispis računa (opis problema)");
                Console.WriteLine("5. Prijelaz iz interaktivnog u skupni način");
                Console.WriteLine("6. Ispis podataka o stanju");
                Console.WriteLine("7. Ispis podataka o najmu i zaradi");
                Console.WriteLine("8. Ispis podataka o računima");
                Console.WriteLine("9. Ispis financijskog stanja korisnika koji su imali najam vozila");
                Console.WriteLine("10. Ispis podataka o računima korisnika");
                Console.WriteLine("11. Plaćanje dugovanja korisnika");
                Console.WriteLine("0. Kraj programa");

                Console.Write("\nOdabir opcije: ");
                string odabir = Console.ReadLine();
                TvrtkaSingleton Tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
                if (int.TryParse(odabir, out int izbor))
                {
                    ///FUNKCIONALNOST AKO JE IZBOR 1
                    if (izbor == 1)
                    {
                        Console.WriteLine("\nSintaksa: id_aktivnosti(1); vrijeme; id_korisnika; id_lokacije; id_vrste_vozila\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (AktivnostiTXT.izlaznaLista != null)
                        {
                            bool aktivnost = AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(4));
                            Vozilo vozilo1 = Tvrtka.ListaVozila.Find(x => x.GetId().Equals(int.Parse(red[4])));
                            TimeSpan razmakVozilo = vozilo1.DohvatVrijeme();
                            if (aktivnost == true)
                            {
                                string datumOvaj = red[1].Trim().Substring(1, 19);
                                DateTime datumUneseni = DateTime.Parse(datumOvaj);
                                Aktivnost aktivnost1 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(4));
                                string datumDrugi = aktivnost1.GetDatum().Trim().Substring(1, 19);
                                DateTime datumOnaj = DateTime.Parse(datumDrugi);
                                TimeSpan razmak = datumUneseni - datumOnaj;
                                if (razmak > razmakVozilo)
                                {
                                    LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(int.Parse(red[3])) && x.GetIdVrsteVozila().Contains(int.Parse(red[4])));

                                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                    int povecaj = raspolozivaMjesta + 1;
                                    lokacijeVozila.SetRaspolozivihMjesta(povecaj);
                                    int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                    int povecajMjesta = brojMjesta + 1;
                                    lokacijeVozila.SetBrojMjesta(povecajMjesta);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                        

                        if (!Datoteka.ProvjeriSamoBrojevi(red[0].Trim()) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 1)
                        {
                            if (red.Length != 5)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }
                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti!");

                                    }
                                    else
                                    {
                                        if (!Datoteka.ProvjeriSamoBrojevi(red[2].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[3].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[4].Trim()))
                                        {
                                            Console.WriteLine("Neispravno uneseni podaci!");
                                        }
                                        else
                                        {
                                            bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(red[2])));
                                            if (provjeraOsoba == false)
                                            {
                                                Console.WriteLine("Unesena osoba ne postoji!");
                                            }
                                            else
                                            {
                                                bool provjeraLokacija = Tvrtka.ListaLokacija.Exists(x => x.GetId().Equals(int.Parse(red[3])));
                                                if (provjeraLokacija == false)
                                                {
                                                    Console.WriteLine("Unesena lokacija ne postoji!");
                                                }
                                                else
                                                {
                                                    bool provjeraVozila = Tvrtka.ListaVozila.Exists(x => x.GetId().Equals(int.Parse(red[4])));
                                                    if (provjeraVozila == false)
                                                    {
                                                        Console.WriteLine("Uneseno vozilo ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == int.Parse(red[2]));
                                                        string imePrezime = osoba.GetImePrezime();
                                                        Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == int.Parse(red[3]));
                                                        string imeLokacije = lokacija.GetNazivLokacije();
                                                        Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == int.Parse(red[4]));
                                                        string nazivVozila = vozilo.GetNazivVozila();
                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                        tempAktivnost.SetIdAktivnosti(1);
                                                        tempAktivnost.SetDatum(red[1]);
                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                        Aktivnost prosla = AktivnostiTXT.izlaznaLista.Last<Aktivnost>();
                                                        int proslaLokacija = prosla.GetIdLokacije()[0];
                                                        int prosloVozilo = prosla.GetIdVrsteVozila()[0];
                                                        LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(proslaLokacija) && x.GetIdVrsteVozila().Contains(prosloVozilo));
                                                        
                                                        int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4]);
                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " traži na lokaciji " + imeLokacije + " broj raspoloživih " + nazivVozila + "a.");
                                                        Console.WriteLine("\nRaspoloživo je: " + raspolozivaMjesta + " " + nazivVozila + "a.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 1!");
                        }

                    }
                    else if (izbor == 2)
                    { ///FUNKCIONALNOST AKO JE IZBOR 2
                        Console.WriteLine("\nSintaksa: id_aktivnosti(2); vrijeme; id_korisnika; id_lokacije; id_vrste_vozila\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');

                        if (AktivnostiTXT.izlaznaLista != null)
                        {
                            bool aktivnost = AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(4));
                            Vozilo vozilo1 = Tvrtka.ListaVozila.Find(x => x.GetId().Equals(int.Parse(red[4])));
                            TimeSpan razmakVozilo = vozilo1.DohvatVrijeme();
                            if (aktivnost == true)
                            {
                                string datumOvaj = red[1].Trim().Substring(1, 19);
                                DateTime datumUneseni = DateTime.Parse(datumOvaj);
                                Aktivnost aktivnost1 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(4));
                                string datumDrugi = aktivnost1.GetDatum().Trim().Substring(1, 19);
                                DateTime datumOnaj = DateTime.Parse(datumDrugi);
                                TimeSpan razmak = datumUneseni - datumOnaj;
                                if (razmak > razmakVozilo)
                                {
                                    LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(int.Parse(red[3])) && x.GetIdVrsteVozila().Contains(int.Parse(red[4])));

                                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                    int povecaj = raspolozivaMjesta + 1;
                                    lokacijeVozila.SetRaspolozivihMjesta(povecaj);
                                    int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                    int povecajMjesta = brojMjesta + 1;
                                    lokacijeVozila.SetBrojMjesta(povecajMjesta);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        if (!Datoteka.ProvjeriSamoBrojevi(red[0].Trim()) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 2)
                        {
                            if (red.Length != 5)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }

                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti!");

                                    }
                                    else
                                    {
                                        if (!Datoteka.ProvjeriSamoBrojevi(red[2].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[3].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[4].Trim()))
                                        {
                                            Console.WriteLine("Neispravno uneseni podaci!");
                                        }
                                        else
                                        {
                                            bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(red[2])));
                                            if (provjeraOsoba == false)
                                            {
                                                Console.WriteLine("Unesena osoba ne postoji!");
                                            }
                                            else
                                            {
                                                bool provjeraLokacija = Tvrtka.ListaLokacija.Exists(x => x.GetId().Equals(int.Parse(red[3])));
                                                if (provjeraLokacija == false)
                                                {
                                                    Console.WriteLine("Unesena lokacija ne postoji!");
                                                }
                                                else
                                                {
                                                    bool provjeraVozila = Tvrtka.ListaVozila.Exists(x => x.GetId().Equals(int.Parse(red[4])));
                                                    if (provjeraVozila == false)
                                                    {
                                                        Console.WriteLine("Uneseno vozilo ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == int.Parse(red[2]));
                                                        string imePrezime = osoba.GetImePrezime();
                                                        int idKorisnika = osoba.GetId();
                                                        Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == int.Parse(red[3]));
                                                        string imeLokacije = lokacija.GetNazivLokacije();
                                                        int idLokacija = lokacija.GetId();
                                                        Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == int.Parse(red[4]));
                                                        string nazivVozila = vozilo.GetNazivVozila();
                                                        int idVozila = vozilo.GetId();
                                                        LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));
                                                        if (lokacijeVozila.GetRaspolozivihMjesta() <= 0)
                                                        {
                                                            Console.WriteLine("Na lokaciji " + imeLokacije + " ne postoje raspoloživi " + nazivVozila + "i!");
                                                        }
                                                        else
                                                        {
                                                            if (AktivnostiTXT.izlaznaLista.Count == 1)
                                                            {
                                                                Aktivnost tempAktivnost = new Aktivnost();
                                                                tempAktivnost.SetIdAktivnosti(2);
                                                                tempAktivnost.SetDatum(red[1]);
                                                                tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                Aktivnost prosla = AktivnostiTXT.izlaznaLista.Last<Aktivnost>();
                                                                int proslaLokacija = prosla.GetIdLokacije()[0];
                                                                int prosloVozilo = prosla.GetIdVrsteVozila()[0];
                                                                int brojMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                                                Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4]);
                                                                Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " traži na lokaciji " + imeLokacije + " najam " + nazivVozila + "a.");
                                                                Console.WriteLine("\n" + imePrezime + " unajmljuje " + nazivVozila + " na lokaciji " + imeLokacije + "!");
                                                                Vozilo vozilo1 = new Vozilo(new StateSlobodno());
                                                                vozilo1.ZahtjevZaPromjenuStanja();
                                                                int smanjiBroj = brojMjesta - 1;
                                                                lokacijeVozila.SetRaspolozivihMjesta(smanjiBroj);
                                                            }
                                                            else
                                                            {
                                                                if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Equals(idKorisnika) && x.GetIdVrsteVozila().Equals(idVozila) == false))
                                                                {
                                                                    /*for (int i = 0; i < lokacijeVozila.GetRaspolozivihMjesta(); i++)
                                                                    {
                                                                        Datoteka.DodjeliJednoznacniID(vozilo);
                                                                        //Console.WriteLine(vozilo.GetJednoznacniID());
                                                                    }*/

                                                                    Aktivnost tempAktivnost = new Aktivnost();
                                                                    tempAktivnost.SetIdAktivnosti(2);
                                                                    tempAktivnost.SetDatum(red[1]);
                                                                    tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                    tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                    tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                    AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                    Aktivnost prosla = AktivnostiTXT.izlaznaLista.Last<Aktivnost>();
                                                                    int proslaLokacija = prosla.GetIdLokacije()[0];
                                                                    int prosloVozilo = prosla.GetIdVrsteVozila()[0];
                                                                    int brojMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                                                    Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4]);
                                                                    Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " traži na lokaciji " + imeLokacije + " najam " + nazivVozila + "a.");
                                                                    Console.WriteLine("\n" + imePrezime + " unajmljuje " + nazivVozila + " na lokaciji " + imeLokacije + "!");
                                                                    Vozilo vozilo1 = new Vozilo(new StateSlobodno());
                                                                    vozilo1.ZahtjevZaPromjenuStanja();
                                                                    int smanjiBroj = brojMjesta - 1;
                                                                    lokacijeVozila.SetRaspolozivihMjesta(smanjiBroj);
                                                                }
                                                                else
                                                                {
                                                                    var aktivnost3 = AktivnostiTXT.izlaznaLista.Where(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                    if (aktivnost3.Count() != 0)
                                                                    {
                                                                        Console.WriteLine(imePrezime + " smije imati samo jedan aktivni najam " + nazivVozila + "a! Prvo vratite aktivni da možete iznajmiti novi ili odaberite drugu vrstu vozila!");
                                                                    }
                                                                    else
                                                                    {
                                                                        /*for (int i = 0; i < lokacijeVozila.GetRaspolozivihMjesta(); i++)
                                                                        {
                                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                                            //Console.WriteLine(vozilo.GetJednoznacniID());
                                                                        }*/

                                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                                        tempAktivnost.SetIdAktivnosti(2);
                                                                        tempAktivnost.SetDatum(red[1]);
                                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                        Aktivnost prosla = AktivnostiTXT.izlaznaLista.Last<Aktivnost>();
                                                                        int proslaLokacija = prosla.GetIdLokacije()[0];
                                                                        int prosloVozilo = prosla.GetIdVrsteVozila()[0];
                                                                        int brojMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4]);
                                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " traži na lokaciji " + imeLokacije + " najam " + nazivVozila + "a.");
                                                                        Console.WriteLine("\n" + imePrezime + " unajmljuje " + nazivVozila + " na lokaciji " + imeLokacije + "!");
                                                                        Vozilo vozilo1 = new Vozilo(new StateSlobodno());
                                                                        vozilo1.ZahtjevZaPromjenuStanja();
                                                                        int smanjiBroj = brojMjesta - 1;
                                                                        lokacijeVozila.SetRaspolozivihMjesta(smanjiBroj);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 2!");
                        }
                    }
                    else if (izbor == 3)
                    { ///FUNKCIONALNOST AKO JE IZBOR 3
                        Console.WriteLine("\nSintaksa: id_aktivnosti(3); vrijeme; id_korisnika; id_lokacije; id_vrste_vozila\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');

                        if (AktivnostiTXT.izlaznaLista != null)
                        {
                            bool aktivnost = AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(4));
                            Vozilo vozilo1 = Tvrtka.ListaVozila.Find(x => x.GetId().Equals(int.Parse(red[4])));
                            TimeSpan razmakVozilo = vozilo1.DohvatVrijeme();
                            if (aktivnost == true)
                            {
                                string datumOvaj = red[1].Trim().Substring(1, 19);
                                DateTime datumUneseni = DateTime.Parse(datumOvaj);
                                Aktivnost aktivnost1 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(4));
                                string datumDrugi = aktivnost1.GetDatum().Trim().Substring(1, 19);
                                DateTime datumOnaj = DateTime.Parse(datumDrugi);
                                TimeSpan razmak = datumUneseni - datumOnaj;
                                if (razmak > razmakVozilo)
                                {
                                    LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(int.Parse(red[3])) && x.GetIdVrsteVozila().Contains(int.Parse(red[4])));

                                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                    int povecaj = raspolozivaMjesta + 1;
                                    lokacijeVozila.SetRaspolozivihMjesta(povecaj);
                                    int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                    int povecajMjesta = brojMjesta + 1;
                                    lokacijeVozila.SetBrojMjesta(povecajMjesta);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        if (!Datoteka.ProvjeriSamoBrojevi(red[0].Trim()) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 3)
                        {
                            if (red.Length != 5)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }

                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti!");

                                    }
                                    else
                                    {
                                        if (!Datoteka.ProvjeriSamoBrojevi(red[2].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[3].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[4].Trim()))
                                        {
                                            Console.WriteLine("Neispravno uneseni podaci!");
                                        }
                                        else
                                        {
                                            bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(red[2])));
                                            if (provjeraOsoba == false)
                                            {
                                                Console.WriteLine("Unesena osoba ne postoji!");
                                            }
                                            else
                                            {
                                                bool provjeraLokacija = Tvrtka.ListaLokacija.Exists(x => x.GetId().Equals(int.Parse(red[3])));
                                                if (provjeraLokacija == false)
                                                {
                                                    Console.WriteLine("Unesena lokacija ne postoji!");
                                                }
                                                else
                                                {
                                                    bool provjeraVozila = Tvrtka.ListaVozila.Exists(x => x.GetId().Equals(int.Parse(red[4])));
                                                    if (provjeraVozila == false)
                                                    {
                                                        Console.WriteLine("Uneseno vozilo ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == int.Parse(red[2]));
                                                        string imePrezime = osoba.GetImePrezime();
                                                        Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == int.Parse(red[3]));
                                                        string imeLokacije = lokacija.GetNazivLokacije();
                                                        Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == int.Parse(red[4]));
                                                        string nazivVozila = vozilo.GetNazivVozila();
                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                        tempAktivnost.SetIdAktivnosti(3);
                                                        tempAktivnost.SetDatum(red[1]);
                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                        Aktivnost prosla = AktivnostiTXT.izlaznaLista.Last<Aktivnost>();
                                                        int proslaLokacija = prosla.GetIdLokacije()[0];
                                                        int prosloVozilo = prosla.GetIdVrsteVozila()[0];
                                                        LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(proslaLokacija) && x.GetIdVrsteVozila().Contains(prosloVozilo));
                                                        /*for (int i = 0; i < lokacijeVozila.GetRaspolozivihMjesta(); i++)
                                                        {
                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                            //Console.WriteLine(vozilo.GetJednoznacniID());
                                                        }*/
                                                        int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4]);
                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " traži na lokaciji " + imeLokacije + " broj raspoloživih mjesta za " + nazivVozila + "e.");
                                                        Console.WriteLine("\nRaspoloživo je: " + brojMjesta + " mjesta za " + nazivVozila + "e.");
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 3!");
                        }
                        
                    }
                    else if (izbor == 4)
                    { ///FUNKCIONALNOST AKO JE IZBOR 4
                        Console.WriteLine("\nSintaksa: id_aktivnosti(4); vrijeme; id_korisnika; id_lokacije; id_vrste_vozila; broj km (; opis_problema)\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');

                        if (AktivnostiTXT.izlaznaLista != null)
                        {
                            bool aktivnost = AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(4));
                            Vozilo vozilo1 = Tvrtka.ListaVozila.Find(x => x.GetId().Equals(int.Parse(red[4])));
                            TimeSpan razmakVozilo = vozilo1.DohvatVrijeme();
                            if (aktivnost == true)
                            {
                                string datumOvaj = red[1].Trim().Substring(1, 19);
                                DateTime datumUneseni = DateTime.Parse(datumOvaj);
                                Aktivnost aktivnost1 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(4));
                                string datumDrugi = aktivnost1.GetDatum().Trim().Substring(1, 19);
                                DateTime datumOnaj = DateTime.Parse(datumDrugi);
                                TimeSpan razmak = datumUneseni - datumOnaj;
                                if (razmak > razmakVozilo)
                                {
                                    LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(int.Parse(red[3])) && x.GetIdVrsteVozila().Contains(int.Parse(red[4])));

                                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                                    int povecaj = raspolozivaMjesta + 1;
                                    lokacijeVozila.SetRaspolozivihMjesta(povecaj);
                                    int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                    int povecajMjesta = brojMjesta + 1;
                                    lokacijeVozila.SetBrojMjesta(povecajMjesta);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        if (!Datoteka.ProvjeriSamoBrojevi(red[0].Trim()) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 4)
                        {
                            if (red.Length == 6)
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }

                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti!");

                                    }
                                    else
                                    {
                                        if (!Datoteka.ProvjeriSamoBrojevi(red[2].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[3].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[4].Trim()))
                                        {
                                            Console.WriteLine("Neispravno uneseni podaci!");
                                        }
                                        else
                                        {
                                            bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(red[2])));
                                            if (provjeraOsoba == false)
                                            {
                                                Console.WriteLine("Unesena osoba ne postoji!");
                                            }
                                            else
                                            {
                                                bool provjeraLokacija = Tvrtka.ListaLokacija.Exists(x => x.GetId().Equals(int.Parse(red[3])));
                                                if (provjeraLokacija == false)
                                                {
                                                    Console.WriteLine("Unesena lokacija ne postoji!");
                                                }
                                                else
                                                {
                                                    bool provjeraVozila = Tvrtka.ListaVozila.Exists(x => x.GetId().Equals(int.Parse(red[4])));
                                                    if (provjeraVozila == false)
                                                    {
                                                        Console.WriteLine("Uneseno vozilo ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == int.Parse(red[2]));
                                                        string imePrezime = osoba.GetImePrezime();
                                                        int idKorisnika = osoba.GetId();
                                                        Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == int.Parse(red[3]));
                                                        string imeLokacije = lokacija.GetNazivLokacije();
                                                        int idLokacija = lokacija.GetId();
                                                        Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == int.Parse(red[4]));
                                                        string nazivVozila = vozilo.GetNazivVozila();
                                                        int idVozila = vozilo.GetId();
                                                        int domet = vozilo.GetDomet();
                                                        LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));
                                                        if (int.Parse(red[5]) > domet)
                                                        {
                                                            Console.WriteLine("Unjeli ste krivi broj kilometara! Najveća kilometraža(domet) ovog vozila je: " + vozilo.GetDomet() + " km!");
                                                        }
                                                        else
                                                        {
                                                            if (lokacijeVozila.GetBrojMjesta() <= 0)
                                                            {
                                                                Console.WriteLine("Na lokaciji " + imeLokacije + " ne postoje mjesta za vratiti " + nazivVozila + "e!");
                                                            }
                                                            else
                                                            {
                                                                //bool aktivnost = AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila)) == false)
                                                                {
                                                                    Console.WriteLine(imePrezime + " ne može vratiti " + nazivVozila + " na lokaciju " + imeLokacije + " jer nema aktivni najam odabranog vozila!");
                                                                }
                                                                else
                                                                {
                                                                    var aktivnost3 = AktivnostiTXT.izlaznaLista.Where(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));

                                                                    if (aktivnost3.Count() != 0)
                                                                    {
                                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                                        tempAktivnost.SetIdAktivnosti(4);
                                                                        tempAktivnost.SetDatum(red[1]);
                                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                        tempAktivnost.SetBrojKm(int.Parse(red[5]));
                                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                        Cjenik cjenik = Tvrtka.ListaCjenika.Find(x => x.GetIdVrsteVozila().First() == idVozila);
                                                                        double cijenaNajma = cjenik.GetNajam();
                                                                        double cijenaPoSatu = cjenik.GetPoSatu();
                                                                        double cijenaPoKm = cjenik.GetPoKm();
                                                                        int brojKm = int.Parse(red[5]);
                                                                        int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                                                        int brojRaspolozivih = lokacijeVozila.GetRaspolozivihMjesta();
                                                                        Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                        
                                                                        DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1,19));
                                                                        DateTime uneseniDatum = DateTime.Parse(red[1].Trim().Substring(1,19));
                                                                        TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                                                                        int vrijeme = odrediDatum.Hours;
                                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4] + ";" + red[5]);
                                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " na lokaciji " + imeLokacije + " vraća unajmljeni " + nazivVozila + " koji ima ukupno " + red[5] + " km."
                                                                            + " Stavke računa su: 1 najam " + nazivVozila + "a - " + cijenaNajma + " kn, najam je bio: " + vrijeme + " sata  - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                                                        brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " + cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                                                                        listaRacuna.Add(new Tuple<int, double>(int.Parse(red[2]), (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                                                                        listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), red[0], "dug", nazivVozila, imeLokacije));
                                                                        Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                                                                        vozilo1.ZahtjevZaPromjenuStanja();

                                                                        double potrosnja = vozilo.PotrosnjaBaterije(brojKm);

                                                                        double vrPunjenja = vozilo.VrijemePunjenja();
                                                                        TimeSpan date = vozilo.DohvatVrijeme();
                                                                        int smanjiBroj = brojMjesta - 1;
                                                                        lokacijeVozila.SetBrojMjesta(smanjiBroj);
                                                                        AktivnostiTXT.izlaznaLista.Remove(aktivnost2);
                                                                        /*foreach (var item in AktivnostiTXT.izlaznaLista)
                                                                        {
                                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                                            //Console.WriteLine(item.GetIdAktivnosti());
                                                                        }*/
                                                                    }
                                                                    else
                                                                    {
                                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                                        tempAktivnost.SetIdAktivnosti(4);
                                                                        tempAktivnost.SetDatum(red[1]);
                                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                        tempAktivnost.SetBrojKm(int.Parse(red[5]));
                                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                        Cjenik cjenik = Tvrtka.ListaCjenika.Find(x => x.GetIdVrsteVozila().First() == idVozila);
                                                                        double cijenaNajma = cjenik.GetNajam();
                                                                        double cijenaPoSatu = cjenik.GetPoSatu();
                                                                        double cijenaPoKm = cjenik.GetPoKm();
                                                                        int brojKm = int.Parse(red[5]);
                                                                        int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                                                        int brojRaspolozivih = lokacijeVozila.GetRaspolozivihMjesta();
                                                                        Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                        DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1, 19));
                                                                        DateTime uneseniDatum = DateTime.Parse(red[1].Trim().Substring(1, 19));
                                                                        TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                                                                        int vrijeme = odrediDatum.Hours;
                                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4] + ";" + red[5]);
                                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " na lokaciji " + imeLokacije + " vraća unajmljeni " + nazivVozila + " koji ima ukupno " + red[5] + " km."
                                                                            + " Stavke računa su: 1 najam " + nazivVozila + "a - " + cijenaNajma + " kn, najam je bio: " + vrijeme + " sata  - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                                                        brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " + cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                                                                        listaRacuna.Add(new Tuple<int, double>(int.Parse(red[2]), (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                                                                        listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), red[0], "dug", nazivVozila, imeLokacije));
                                                                        Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                                                                        vozilo1.ZahtjevZaPromjenuStanja();
                                                                        double potrosnja = vozilo.PotrosnjaBaterije(brojKm);
                                                                        
                                                                        double vrPunjenja = vozilo.VrijemePunjenja();
                                                                        TimeSpan date = vozilo.DohvatVrijeme();
                                                                        int smanjiBroj = brojMjesta - 1;
                                                                        lokacijeVozila.SetBrojMjesta(smanjiBroj);
                                                                        AktivnostiTXT.izlaznaLista.Remove(aktivnost2);
                                                                        /*foreach (var item in AktivnostiTXT.izlaznaLista)
                                                                        {
                                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                                        }*/
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

                            }
                            else if (red.Length == 7)
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }

                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Datum mora biti veći od datuma prethodne aktivnosti!");

                                    }
                                    else
                                    {
                                        if (!Datoteka.ProvjeriSamoBrojevi(red[2].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[3].Trim()) || !Datoteka.ProvjeriSamoBrojevi(red[4].Trim()))
                                        {
                                            Console.WriteLine("Neispravno uneseni podaci!");
                                        }
                                        else
                                        {
                                            bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(red[2])));
                                            if (provjeraOsoba == false)
                                            {
                                                Console.WriteLine("Unesena osoba ne postoji!");
                                            }
                                            else
                                            {
                                                bool provjeraLokacija = Tvrtka.ListaLokacija.Exists(x => x.GetId().Equals(int.Parse(red[3])));
                                                if (provjeraLokacija == false)
                                                {
                                                    Console.WriteLine("Unesena lokacija ne postoji!");
                                                }
                                                else
                                                {
                                                    bool provjeraVozila = Tvrtka.ListaVozila.Exists(x => x.GetId().Equals(int.Parse(red[4])));
                                                    if (provjeraVozila == false)
                                                    {
                                                        Console.WriteLine("Uneseno vozilo ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        Osoba osoba = Tvrtka.ListaOsoba.Find(x => x.GetId() == int.Parse(red[2]));
                                                        string imePrezime = osoba.GetImePrezime();
                                                        int idKorisnika = osoba.GetId();
                                                        Lokacija lokacija = Tvrtka.ListaLokacija.Find(x => x.GetId() == int.Parse(red[3]));
                                                        string imeLokacije = lokacija.GetNazivLokacije();
                                                        int idLokacija = lokacija.GetId();
                                                        Vozilo vozilo = Tvrtka.ListaVozila.Find(x => x.GetId() == int.Parse(red[4]));
                                                        string nazivVozila = vozilo.GetNazivVozila();
                                                        int idVozila = vozilo.GetId();
                                                        int domet = vozilo.GetDomet();
                                                        LokacijeVozila lokacijeVozila = Tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));
                                                        if (int.Parse(red[5]) > domet)
                                                        {
                                                            Console.WriteLine("Unjeli ste krivi broj kilometara! Najveća kilometraža(domet) ovog vozila je: " + vozilo.GetDomet() + " km!");
                                                        }
                                                        else
                                                        {
                                                            if (lokacijeVozila.GetBrojMjesta() <= 0)
                                                            {
                                                                Console.WriteLine("Na lokaciji " + imeLokacije + " ne postoje mjesta za vratiti " + nazivVozila + "e!");
                                                            }
                                                            else
                                                            {
                                                                if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Equals(idKorisnika) && x.GetIdVrsteVozila().Equals(idVozila) == false))
                                                                {
                                                                    Console.WriteLine(imePrezime + " ne može vratiti " + nazivVozila + " na lokaciju " + imeLokacije + " jer nema aktivni najam odabranog vozila!");
                                                                }
                                                                else
                                                                {
                                                                    var aktivnost3 = AktivnostiTXT.izlaznaLista.Where(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));

                                                                    if (aktivnost3.Count() != 0)
                                                                    {
                                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                                        tempAktivnost.SetIdAktivnosti(4);
                                                                        tempAktivnost.SetDatum(red[1]);
                                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                        tempAktivnost.SetBrojKm(int.Parse(red[5]));
                                                                        tempAktivnost.SetOpisProblema(red[6]);
                                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                        Cjenik cjenik = Tvrtka.ListaCjenika.Find(x => x.GetIdVrsteVozila().First() == idVozila);
                                                                        double cijenaNajma = cjenik.GetNajam();
                                                                        double cijenaPoSatu = cjenik.GetPoSatu();
                                                                        double cijenaPoKm = cjenik.GetPoKm();
                                                                        int brojKm = int.Parse(red[5]);
                                                                        int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                                                        Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                        DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1, 19));
                                                                        DateTime uneseniDatum = DateTime.Parse(red[1].Trim().Substring(1, 19));
                                                                        TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                                                                        int vrijeme = odrediDatum.Hours;
                                                                        int brojMjesta2 = lokacijeVozila.GetBrojMjesta();
                                                                        int brojRaspolozivih2 = lokacijeVozila.GetRaspolozivihMjesta();
                                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4] + ";" + red[5] + ";" + red[6]);
                                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " na lokaciji " + imeLokacije + " vraća unajmljeni " + nazivVozila + " koji ima ukupno " + red[5] + " km te prijavljuje da vozilo ima problem:" + red[6] + "."
                                                                            + " Stavke računa su: 1 najam " + nazivVozila + "a - " + cijenaNajma + " kn, najam je bio: " + vrijeme + " sata  - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                                                        brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " + cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                                                                        listaRacuna.Add(new Tuple<int, double>(int.Parse(red[2]), (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                                                                        listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), red[0], "dug", nazivVozila, imeLokacije));
                                                                        Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                                                                        vozilo1.ZahtjevZaPromjenuStanja();
                                                                        Console.WriteLine("\nVozilo više nije dostupno!");
                                                                        int smanjiBroj = brojMjesta2 - 1;
                                                                        lokacijeVozila.SetBrojMjesta(smanjiBroj);
                                                                        int brojNeispravnih = osoba.GetBrojVracanjaNeispravnihVozila();
                                                                        int povecajBrojNeispravnih = brojNeispravnih + 1;
                                                                        osoba.SetBrojVracanjaNeispravnihVozila(povecajBrojNeispravnih);
                                                                        AktivnostiTXT.izlaznaLista.Remove(aktivnost2);
                                                                        /*foreach (var item in AktivnostiTXT.izlaznaLista)
                                                                        {
                                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                                        }*/
                                                                    }
                                                                    else
                                                                    {
                                                                        Aktivnost tempAktivnost = new Aktivnost();
                                                                        tempAktivnost.SetIdAktivnosti(4);
                                                                        tempAktivnost.SetDatum(red[1]);
                                                                        tempAktivnost.SetIdKorisnika(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[2]).ToString()));
                                                                        tempAktivnost.SetIdLokacije(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[3]).ToString()));
                                                                        tempAktivnost.SetIdVrsteVozila(AktivnostiTXT.IzradiListuLokacijaVozila(int.Parse(red[4]).ToString()));
                                                                        tempAktivnost.SetBrojKm(int.Parse(red[5]));
                                                                        tempAktivnost.SetOpisProblema(red[6]);
                                                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                                                        Cjenik cjenik = Tvrtka.ListaCjenika.Find(x => x.GetIdVrsteVozila().First() == idVozila);
                                                                        double cijenaNajma = cjenik.GetNajam();
                                                                        double cijenaPoSatu = cjenik.GetPoSatu();
                                                                        double cijenaPoKm = cjenik.GetPoKm();
                                                                        int brojKm = int.Parse(red[5]);
                                                                        int brojMjesta = lokacijeVozila.GetBrojMjesta();
                                                                        Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                                                                        DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1, 19));
                                                                        DateTime uneseniDatum = DateTime.Parse(red[1].Trim().Substring(1, 19));
                                                                        TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                                                                        int vrijeme = odrediDatum.Hours;
                                                                        int brojMjesta2 = lokacijeVozila.GetBrojMjesta();
                                                                        int brojRaspolozivih2 = lokacijeVozila.GetRaspolozivihMjesta();
                                                                        Console.WriteLine("\n" + red[0] + ";" + red[1] + ";" + red[2] + ";" + red[3] + ";" + red[4] + ";" + red[5] + ";" + red[6]);
                                                                        Console.WriteLine("\nU:" + red[1] + " korisnik " + imePrezime + " na lokaciji " + imeLokacije + " vraća unajmljeni " + nazivVozila + " koji ima ukupno " + red[5] + " km te prijavljuje da vozilo ima problem:" + red[6] + "."
                                                                            + " Stavke računa su: 1 najam " + nazivVozila + "a - " + cijenaNajma + " kn, najam je bio: " + vrijeme + " sata  - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                                                        brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " + cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                                                                        listaRacuna.Add(new Tuple<int, double>(int.Parse(red[2]), (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                                                                        listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), red[0], "dug", nazivVozila, imeLokacije));
                                                                        Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                                                                        vozilo1.ZahtjevZaPromjenuStanja();
                                                                        Console.WriteLine("\nVozilo više nije dostupno!");
                                                                        int smanjiBroj = brojMjesta2 - 1;
                                                                        lokacijeVozila.SetBrojMjesta(smanjiBroj);
                                                                        AktivnostiTXT.izlaznaLista.Remove(aktivnost2);
                                                                        int brojNeispravnih = osoba.GetBrojVracanjaNeispravnihVozila();
                                                                        int povecajBrojNeispravnih = brojNeispravnih + 1;
                                                                        osoba.SetBrojVracanjaNeispravnihVozila(povecajBrojNeispravnih);
                                                                        /*foreach (var item in AktivnostiTXT.izlaznaLista)
                                                                        {
                                                                            Datoteka.DodjeliJednoznacniID(vozilo);
                                                                        }*/
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 4!");
                        }

                    }
                    else if (izbor == 5)
                    {
                        Console.WriteLine("\nSintaksa: id_aktivnosti(5); datoteka\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 5)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(red);
                                
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 5!");
                        }
                    }

                    else if (izbor == 6)
                    {
                        TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                        Console.WriteLine("\nSintaksa: id_aktivnosti(6); komanda {komanda …} {id}\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 6)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                string novi = "";
                                string novi1 = "";

                                if (red[1].Trim().Length == 9)
                                {
                                    Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                    Console.WriteLine("\n");
                                    Datoteka.ZaglavljeStrukture();
                                    tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(red[1].Trim(), 6, args);
                                }
                                else if (red[1].Trim().Length == 11)
                                {

                                    novi = red[1].Trim().Substring(10, 1);
                                    if (!Datoteka.IspravanInt(novi))
                                    {
                                        Console.WriteLine("ID mora biti cijeli broj!");
                                    }
                                    else
                                    {
                                        if (tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetIDTvrtke() == int.Parse(novi)) == false)
                                        {
                                            Console.WriteLine("Tvrtka za koju tražite strukturu ne postoji!");
                                        }
                                        else
                                        {
                                            var tvrtka = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi));
                                            Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                            Console.WriteLine("\n");
                                            Datoteka.ZaglavljeStrukture();
                                            tvrtka.PrikaziPodatke(red[1].Trim(), 6, args);
                                        }
                                    }

                                }
                                else if (red[1].Trim().Length == 16)
                                {
                                    Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                    Console.WriteLine("\n");
                                    Datoteka.ZaglavljeStruktureStanja();
                                    tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(red[1].Trim(), 6, args);
                                }
                                else if (red[1].Trim().Length == 18)
                                {
                                    novi1 = red[1].Trim().Substring(17, 1);
                                    if (!Datoteka.IspravanInt(novi1))
                                    {
                                        Console.WriteLine("ID mora biti cijeli broj!");
                                    }
                                    else
                                    {
                                        if (tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetIDTvrtke() == int.Parse(novi1)) == false)
                                        {
                                            Console.WriteLine("Tvrtka za koju tražite strukturu i stanje ne postoji!");
                                        }
                                        else
                                        {
                                            var tvrtka = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi1));
                                            Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                            Console.WriteLine("\n");
                                            Datoteka.ZaglavljeStruktureStanja();
                                            tvrtka.PrikaziPodatke(red[1].Trim(), 6, args);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Unjeli ste previše/premalo argumenata!");
                                }
                                
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 6!");
                        }
                    }

                    else if (izbor == 7)
                    {
                        TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                        Console.WriteLine("\nSintaksa: id_aktivnosti(7); komanda {komanda …} datum_1 datum_2 {id}\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 7)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                string novi = "";
                                string novi1 = "";
                                string novi2 = "";
                                if (red[1].Trim().Length == 31)
                                {
                                    novi = red[1].Trim().Substring(10, 10);
                                    novi1 = red[1].Trim().Substring(21, 10);
                                    if (!Datoteka.IspravanFormatDatuma(novi))
                                    {
                                        Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                    }
                                    else
                                    {
                                        if (!Datoteka.IspravanFormatDatuma(novi1))
                                        {
                                            Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                        }
                                        else
                                        {
                                            var datum = DateTime.ParseExact(novi, "dd.MM.yyyy", null);
                                            var datum1 = DateTime.ParseExact(novi1, "dd.MM.yyyy", null);
                                            if (datum > datum1)
                                            {
                                                Console.WriteLine("Za raspon datuma prvi datum mora biti manji od drugoga!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                                Console.WriteLine("\n");
                                                Datoteka.ZaglavljeStrukture();
                                                tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(red[1].Trim(), 7, args);
                                            }

                                        }
                                        
                                    }
                                    
                                }
                                else if (red[1].Trim().Length == 33)
                                {
                                    novi = red[1].Trim().Substring(10, 10);
                                    novi1 = red[1].Trim().Substring(21, 10);
                                    novi2 = red[1].Trim().Substring(32, 1);
                                    if (!Datoteka.IspravanInt(novi2))
                                    {
                                        Console.WriteLine("ID mora biti cijeli broj!");
                                    }
                                    else
                                    {
                                        if (!Datoteka.IspravanFormatDatuma(novi))
                                        {
                                            Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                        }
                                        else
                                        {
                                            if (!Datoteka.IspravanFormatDatuma(novi1))
                                            {
                                                Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                            }
                                            else
                                            {
                                                var datum = DateTime.ParseExact(novi, "dd.MM.yyyy", null);
                                                var datum1 = DateTime.ParseExact(novi1, "dd.MM.yyyy", null);
                                                if (datum > datum1)
                                                {
                                                    Console.WriteLine("Za raspon datuma prvi datum mora biti manji od drugoga!");
                                                }
                                                else
                                                {
                                                    if (tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetIDTvrtke() == int.Parse(novi2)) == false)
                                                    {
                                                        Console.WriteLine("Tvrtka za koju tražite strukturu ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        var tvrtka = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi2));
                                                        Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                                        Console.WriteLine("\n");
                                                        Datoteka.ZaglavljeStrukture();
                                                        tvrtka.PrikaziPodatke(red[1].Trim(), 7, args);
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 7!");
                        }
                    }

                    else if (izbor == 8)
                    {
                        TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                        Console.WriteLine("\nSintaksa: id_aktivnosti(8); komanda {komanda …} datum_1 datum_2 {id}\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 8)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                string novi = "";
                                string novi1 = "";
                                string novi2 = "";
                                if (red[1].Trim().Length == 31)
                                {
                                    novi = red[1].Trim().Substring(10, 10);
                                    novi1 = red[1].Trim().Substring(21, 10);
                                    if (!Datoteka.IspravanFormatDatuma(novi))
                                    {
                                        Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                    }
                                    else
                                    {
                                        if (!Datoteka.IspravanFormatDatuma(novi1))
                                        {
                                            Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                        }
                                        else
                                        {
                                            var datum = DateTime.ParseExact(novi, "dd.MM.yyyy", null);
                                            var datum1 = DateTime.ParseExact(novi1, "dd.MM.yyyy", null);
                                            if (datum > datum1)
                                            {
                                                Console.WriteLine("Za raspon datuma prvi datum mora biti manji od drugoga!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                                Console.WriteLine("\n");
                                                Datoteka.ZaglavljeStrukture();
                                                tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(red[1].Trim(), 8, args);
                                            }

                                        }

                                    }

                                }
                                else if (red[1].Trim().Length == 33)
                                {
                                    novi = red[1].Trim().Substring(10, 10);
                                    novi1 = red[1].Trim().Substring(21, 10);
                                    novi2 = red[1].Trim().Substring(32, 1);
                                    if (!Datoteka.IspravanInt(novi2))
                                    {
                                        Console.WriteLine("ID mora biti cijeli broj!");
                                    }
                                    else
                                    {
                                        if (!Datoteka.IspravanFormatDatuma(novi))
                                        {
                                            Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                        }
                                        else
                                        {
                                            if (!Datoteka.IspravanFormatDatuma(novi1))
                                            {
                                                Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                            }
                                            else
                                            {
                                                var datum = DateTime.ParseExact(novi, "dd.MM.yyyy", null);
                                                var datum1 = DateTime.ParseExact(novi1, "dd.MM.yyyy", null);
                                                if (datum > datum1)
                                                {
                                                    Console.WriteLine("Za raspon datuma prvi datum mora biti manji od drugoga!");
                                                }
                                                else
                                                {
                                                    if (tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetIDTvrtke() == int.Parse(novi2)) == false)
                                                    {
                                                        Console.WriteLine("Tvrtka za koju tražite strukturu ne postoji!");
                                                    }
                                                    else
                                                    {
                                                        var tvrtka = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi2));
                                                        Console.WriteLine("\n" + red[0] + ";" + red[1]);
                                                        Console.WriteLine("\n");
                                                        Datoteka.ZaglavljeStrukture();
                                                        tvrtka.PrikaziPodatke(red[1].Trim(), 8, args);
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 8!");
                        }
                    }

                    else if (izbor == 9)
                    {
                        Console.WriteLine("\nSintaksa: id_aktivnosti(9)\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 9)
                        {
                            if (red.Length != 1)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                string ispisID = "";
                                var ispisDatum = "";
                                string ispisOsoba = "";
                                TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                                var dohvatAktivnosti = tvrtkaSingleton.ListaAktivnosti.Where(x => x.GetIdAktivnosti() == 2).ToList();
                                var dohvatiId = dohvatAktivnosti.Select(x => x.GetIdKorisnika()[0]).Distinct();
                                Datoteka.ZaglavljeAktivnostDevet();
                                foreach (var item in dohvatiId)
                                {
                                    var dohvatiDatum = dohvatAktivnosti.Where(x => x.GetIdKorisnika()[0] == item).Select(x => x.GetDatum()).Last();
                                    var dohvatOsob = tvrtkaSingleton.ListaOsoba.Where(x => x.GetId() == item).ToList();
                                    var dohvatOsoba = dohvatOsob.Select(x => x.GetImePrezime()).Distinct();
                                    decimal stanje = 0;
                                    ispisID = item.ToString();
                                    foreach (var item2 in dohvatiDatum)
                                    {
                                        ispisDatum += item2;
                                    }
                                    foreach (var item3 in dohvatOsoba)
                                    {
                                        ispisOsoba = item3.ToString();
                                    }
                                    foreach (var item4 in listaRacuna)
                                    {
                                        if (item4.Item1.Equals(item))
                                        {
                                            stanje += decimal.Parse(item4.Item2.ToString());
                                            
                                        }
                                        else
                                        {
                                            
                                        }
                                    }
                                    string status = "";
                                    string dodaj = "";
                                    if (stanje == 0)
                                    {
                                        status = " kn";
                                        dodaj = "00";
                                    }
                                    else if (stanje > 0)
                                    {
                                        status = " kn dug";
                                    }

                                    if (Dugovanje == 0)
                                    {

                                    }
                                    else
                                    {
                                        if (stanje > Dugovanje)
                                        {
                                            ispisDugovanja = "ID: " + ispisID + ", osoba "+ ispisOsoba + " premašio/la je iznos dopuštenog dugovanja! Onemogućeno je daljnje iznajmljivanje!";
                                        }
                                        else
                                        {

                                        }
                                    }
                                    Ispis.Brojac = 0;
                                    IDecoratorRedakTablice redakTablice =
                                        new DecoratorText(
                                            new DecoratorText(
                                                new DecoratorBroj(
                                                    new DecoratorText(
                                                        new DecoratorKonkretniRedak()))));
                                    string format = redakTablice.KreirajRedak();
                                    string output = String.Format(format, ispisDatum, new String(' ', 4) + stanje.ToString(""+dodaj+".00") + status, new String(' ', 4) + ispisID, ispisOsoba);
                                    Console.WriteLine(output);
                                    if (Ispis.FormatTekst == 0)
                                    {
                                        Console.WriteLine(new String('-', 3 * 30));
                                    }
                                    else
                                    {
                                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 20));
                                    }
                                    ispisDatum = "";
                                    
                                }

                                
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 9!");
                        }
                    }

                    else if (izbor == 10)
                    {
                        string novi = "";
                        string novi1 = "";
                        Console.WriteLine("\nSintaksa: id_aktivnosti(10); korisnik_id datum_1 datum_2\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 10)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {
                                if (red[1].Trim().Length == 23)
                                {
                                    string dohvatBroja = red[1].Substring(1, 1);
                                    if (Datoteka.IspravanInt(dohvatBroja))
                                    {
                                        bool provjeraOsoba = Tvrtka.ListaOsoba.Exists(x => x.GetId().Equals(int.Parse(dohvatBroja)));
                                        if (provjeraOsoba == false)
                                        {
                                            Console.WriteLine("Unesena osoba ne postoji!");
                                        }
                                        else
                                        {
                                            Datoteka.ZaglavljeAktivnostiDeset();
                                            string cw = "";
                                            novi = red[1].Substring(3, 10);
                                            novi1 = red[1].Substring(14, 10);
                                            if (!Datoteka.IspravanFormatDatuma(novi))
                                            {
                                                Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                            }
                                            else
                                            {
                                                if (!Datoteka.IspravanFormatDatuma(novi1))
                                                {
                                                    Console.WriteLine("Datum nije ispravno unesen! Mora biti u formatu dd.mm.yyyy!");
                                                }
                                                else
                                                {
                                                    var datum = DateTime.ParseExact(novi, "dd.MM.yyyy", null);
                                                    var datum1 = DateTime.ParseExact(novi1, "dd.MM.yyyy", null);
                                                    if (datum > datum1)
                                                    {
                                                        Console.WriteLine("Za raspon datuma prvi datum mora biti manji od drugoga!");
                                                    }
                                                    else
                                                    {
                                                        int brojGresaka = 0;
                                                        foreach (var item in listaRacuna)
                                                        {
                                                            if (item.Item1.Equals(int.Parse(dohvatBroja)))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                brojGresaka++;
                                                            }
                                                        }
                                                        if (brojGresaka>0)
                                                        {
                                                            Console.WriteLine("Za unesenog korisnika ne postoje računi!");
                                                            
                                                        }
                                                        else
                                                        {
                                                            foreach (var item2 in listaRacunaZa10)
                                                            {
                                                                var triman = item2.Item3.Remove(0, 1);
                                                                var trimani = triman.Remove(10, 10);
                                                                var datum21 = DateTime.ParseExact(trimani, "yyyy-MM-dd", null);
                                                                var datum22 = datum21.ToString("dd.MM.yyyy");
                                                                var datum2 = DateTime.ParseExact(datum22, "dd.MM.yyyy", null);
                                                                if (datum2 < datum1 && datum2 > datum)
                                                                {
                                                                    decimal iznos = decimal.Parse(item2.Item2.ToString());
                                                                    Ispis.Brojac = 0;
                                                                    IDecoratorRedakTablice redakTablice =
                                                                        new DecoratorText(
                                                                            new DecoratorText(
                                                                                new DecoratorText(
                                                                                    new DecoratorText(
                                                                                        new DecoratorBroj(
                                                                                            new DecoratorBroj(
                                                                                                new DecoratorKonkretniRedak()))))));
                                                                    string format = redakTablice.KreirajRedak();
                                                                    string output = String.Format(format, item2.Item6, item2.Item5, item2.Item4, datum22, iznos.ToString("00.00") + "kn", new String(' ', 4) + item2.Item1);
                                                                    Console.WriteLine(output);
                                                                    if (Ispis.FormatTekst == 0)
                                                                    {
                                                                        Console.WriteLine(new String('-', 4 * 30));
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine(new String('-', 4 * Ispis.FormatTekst) + new String('-', 20));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    
                                                                }
                                                            }
                                                        }
                                                                  
                                                    }
                                                }
                                            }
                                        }
                                            
                                    }
                                    else
                                    {
                                        Console.WriteLine("ID korisnika mora biti cijeli broj!");
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Unjeli ste previše/premalo elemenata!");
                                }
                                
                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 10!");
                        }
                    }

                    else if (izbor == 11)
                    {
                        Console.WriteLine("\nSintaksa: id_aktivnosti(11); korisnik_id iznos\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 11)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");

                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 11!");
                        }
                    }

                    else if (izbor == 0)
                    { ///FUNKCIONALNOST AKO JE IZBOR 0
                        Console.WriteLine("\nSintaksa: id_aktivnosti(0); vrijeme\n");
                        string unosAktivnosti = Console.ReadLine();
                        Console.WriteLine();
                        string[] red = Datoteka.PrepoznajRetkeIzDatoteke(unosAktivnosti, ';');
                        if (!Datoteka.ProvjeriSamoBrojevi(red[0]) || red[0] == "")
                        {
                            Console.WriteLine("Neispravno uneseni podaci!");
                        }
                        else if (int.Parse(red[0]) == 0)
                        {
                            if (red.Length != 2)
                            {
                                Console.WriteLine("Unjeli ste previše/premalo argumenata!");
                                
                            }
                            else
                            {
                                if (red[1].Trim().Length != 21)
                                {
                                    Console.WriteLine("Neispravno uneseni podaci!");
                                }
                                else if (!Datoteka.IspravanDatum(red[1]))
                                {
                                    Console.WriteLine("Unesite ispravan datum!");

                                }
                                else
                                {
                                    if (!Datoteka.ProvjeraDaLiJeDatumVeciOdPrethodnog(red[1], AktivnostiTXT.izlaznaLista, args))
                                    {
                                        Console.WriteLine("Greška!");

                                    }
                                    else
                                    {
                                        Aktivnost tempAktivnost = new Aktivnost();
                                        tempAktivnost.SetIdAktivnosti(0);
                                        tempAktivnost.SetDatum(red[1]);
                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                        Console.WriteLine(0 + ";" + red[1]);
                                        Console.WriteLine("U:" + red[1] + " program završava s radom.");
                                        izlaz = true;
                                    }
                                    
                                }
                            }
                                
                        }
                        else
                        {
                            Console.WriteLine("Aktivnost za ovu opciju mora iznositi 0!");
                        }
                        

                        
                    }
                    else
                    {
                        Console.WriteLine("Molim odaberite jednu od ponuđenih opcija (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)!");
                    }
                }
                else
                {
                    Console.WriteLine("Krivi unos! Birajte jednu od ponuđenih opcija!");
                }

            }

            Console.WriteLine("\n\nKraj programa!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        /// <summary>
        /// Metoda koja služi za ispis grešaka po redovima
        /// </summary>
        /// <param name="brojRetka"></param>
        /// <param name="redak"></param>
        public static void IspisGreskeRedak(int brojRetka, string redak)
        {
            Console.WriteLine("U {0}. retku došlo je do greške! --> Preskačem redak --> {1}",
                brojRetka, redak);
        }

        /// <summary>
        /// Metoda koja služi za prikaz skupnog načina rada
        /// </summary>
        /// <param name="args"></param>
        public static void PrikazSkupni(string[] args)
        {
            Console.WriteLine("\n");
            TvrtkaSingleton tvrtka = TvrtkaSingleton.GetTvrtkaInstance();
            foreach (var item in tvrtka.ListaAktivnosti)
            {
                int id = item.GetIdAktivnosti();
                string datum = item.GetDatum();
                if (id == 1)
                {
                    List<int> idKorisnika = item.GetIdKorisnika();
                    List<int> idLokacije = item.GetIdLokacije();
                    List<int> idVrsteVozila = item.GetIdVrsteVozila();
                    int brojKm = item.GetBrojKm();
                    Osoba osoba = tvrtka.ListaOsoba.Find(x => x.GetId() == idKorisnika[0]);
                    string ime = osoba.GetImePrezime();
                    Lokacija lokacija = tvrtka.ListaLokacija.Find(x => x.GetId() == idLokacije[0]);
                    string nazivLokacije = lokacija.GetNazivLokacije();
                    Vozilo vozilo = tvrtka.ListaVozila.Find(x => x.GetId() == idVrsteVozila[0]);
                    string vrstaVozila = vozilo.GetNazivVozila();
                    LokacijeVozila lokacijeVozila = tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacije[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                    Console.WriteLine(id + "; " + datum + "; " + idKorisnika[0] + "; " + idLokacije[0] + "; " + idVrsteVozila[0]);
                    Console.WriteLine("U " + datum + " korisnik " + ime + " traži na lokaciji " + nazivLokacije + " broj raspoloživih " +
                        vrstaVozila + "a.");
                    Console.WriteLine("Raspoloživo je: " + raspolozivaMjesta + " " + vrstaVozila + "a.");
                }
                else if (id == 2)
                {
                    List<int> idKorisnika = item.GetIdKorisnika();
                    List<int> idLokacije = item.GetIdLokacije();
                    List<int> idVrsteVozila = item.GetIdVrsteVozila();
                    int brojKm = item.GetBrojKm();
                    Osoba osoba = tvrtka.ListaOsoba.Find(x => x.GetId() == idKorisnika[0]);
                    string ime = osoba.GetImePrezime();
                    Lokacija lokacija = tvrtka.ListaLokacija.Find(x => x.GetId() == idLokacije[0]);
                    string nazivLokacije = lokacija.GetNazivLokacije();
                    Vozilo vozilo = tvrtka.ListaVozila.Find(x => x.GetId() == idVrsteVozila[0]);
                    string vrstaVozila = vozilo.GetNazivVozila();
                    LokacijeVozila lokacijeVozila = tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacije[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                    int raspolozivaMjesta = lokacijeVozila.GetRaspolozivihMjesta();
                    Console.WriteLine(id + "; " + datum + "; " + idKorisnika[0] + "; " + idLokacije[0] + "; " + idVrsteVozila[0]);
                    Console.WriteLine("U " + datum + " korisnik " + ime + " traži na lokaciji " + nazivLokacije + " najam " +
                        vrstaVozila + "a.");
                    Console.WriteLine(ime + " unajmljuje " + vrstaVozila + " na lokaciji " + nazivLokacije + "!");
                    int smanjiBroj = raspolozivaMjesta - 1;
                    lokacijeVozila.SetRaspolozivihMjesta(smanjiBroj);
                }
                else if (id == 3)
                {
                    List<int> idKorisnika = item.GetIdKorisnika();
                    List<int> idLokacije = item.GetIdLokacije();
                    List<int> idVrsteVozila = item.GetIdVrsteVozila();
                    int brojKm = item.GetBrojKm();
                    Osoba osoba = tvrtka.ListaOsoba.Find(x => x.GetId() == idKorisnika[0]);
                    string ime = osoba.GetImePrezime();
                    Lokacija lokacija = tvrtka.ListaLokacija.Find(x => x.GetId() == idLokacije[0]);
                    string nazivLokacije = lokacija.GetNazivLokacije();
                    Vozilo vozilo = tvrtka.ListaVozila.Find(x => x.GetId() == idVrsteVozila[0]);
                    string vrstaVozila = vozilo.GetNazivVozila();
                    LokacijeVozila lokacijeVozila = tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacije[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                    int brojMjesta = lokacijeVozila.GetBrojMjesta();
                    Console.WriteLine(id + "; " + datum + "; " + idKorisnika[0] + "; " + idLokacije[0] + "; " + idVrsteVozila[0]);
                    Console.WriteLine("U " + datum + " korisnik " + ime + " traži na lokaciji " + nazivLokacije + " broj raspoloživih mjesta za " +
                        vrstaVozila + "e.");
                    Console.WriteLine("Raspoloživo je: " + brojMjesta + " mjesta za " + vrstaVozila + "e.");
                }
                else if (id == 4)
                {
                    List<int> idKorisnika = item.GetIdKorisnika();
                    List<int> idLokacije = item.GetIdLokacije();
                    List<int> idVrsteVozila = item.GetIdVrsteVozila();
                    int brojKm = item.GetBrojKm();
                    Osoba osoba = tvrtka.ListaOsoba.Find(x => x.GetId() == idKorisnika[0]);
                    string ime = osoba.GetImePrezime();
                    Lokacija lokacija = tvrtka.ListaLokacija.Find(x => x.GetId() == idLokacije[0]);
                    string nazivLokacije = lokacija.GetNazivLokacije();
                    Vozilo vozilo = tvrtka.ListaVozila.Find(x => x.GetId() == idVrsteVozila[0]);
                    string vrstaVozila = vozilo.GetNazivVozila();
                    Cjenik cjenik = tvrtka.ListaCjenika.Find(x => x.GetIdVrsteVozila().First() == idVrsteVozila[0]);
                    double cijenaNajma = cjenik.GetNajam();
                    double cijenaPoSatu = cjenik.GetPoSatu();
                    double cijenaPoKm = cjenik.GetPoKm();
                    LokacijeVozila lokacijeVozila = tvrtka.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacije[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                    if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Equals(1)) == true)
                    {
                        Console.Write("Greška! --> ");
                    }
                    else
                    {
                        if (item.GetOpisProblema() == null)
                        {
                            Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x =>  x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                            int brojMjesta = lokacijeVozila.GetBrojMjesta();
                            int brojRaspolozivih = lokacijeVozila.GetRaspolozivihMjesta();
                            DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1, 19));
                            DateTime uneseniDatum = DateTime.Parse(datum.Trim().Substring(1, 19));
                            TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                            int vrijeme = odrediDatum.Hours;
                            Console.WriteLine(id + "; " + datum + "; " + idKorisnika[0] + "; " + idLokacije[0] + "; " + idVrsteVozila[0] + "; " + brojKm);
                            Console.WriteLine("U " + datum + " korisnik " + ime + " na lokaciji " + nazivLokacije + " vraća unajmljeni " +
                                vrstaVozila + " koji ima ukupno " + brojKm + " km." + " Stavke računa su: 1 najam " + vrstaVozila + "a - " + cijenaNajma +
                                " kn, najam je bio " + vrijeme + " sata - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " +
                                cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                            listaRacuna.Add(new Tuple<int, double>(idKorisnika[0], (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                            listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), datum, "dug", vrstaVozila, nazivLokacije));
                            Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                            vozilo1.ZahtjevZaPromjenuStanja();
                            int smanjiBroj = brojMjesta - 1;
                            lokacijeVozila.SetBrojMjesta(smanjiBroj);
                            int povecajBroj = brojRaspolozivih + 1;
                            lokacijeVozila.SetRaspolozivihMjesta(povecajBroj);
                        }
                        else
                        {
                            string opis = item.GetOpisProblema();
                            Aktivnost aktivnost2 = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika[0]) && x.GetIdVrsteVozila().Contains(idVrsteVozila[0]));
                            int brojMjesta = lokacijeVozila.GetBrojMjesta();
                            int brojRaspolozivih = lokacijeVozila.GetRaspolozivihMjesta();
                            DateTime prosliDatum = DateTime.Parse(aktivnost2.GetDatum().Trim().Substring(1, 19));
                            DateTime uneseniDatum = DateTime.Parse(datum.Trim().Substring(1, 19));
                            TimeSpan odrediDatum = uneseniDatum - prosliDatum;
                            int vrijeme = odrediDatum.Hours;
                            Console.WriteLine(id + "; " + datum + "; " + idKorisnika[0] + "; " + idLokacije[0] + "; " + idVrsteVozila[0] + "; " + brojKm + ";" + opis);
                            Console.WriteLine("U " + datum + " korisnik " + ime + " na lokaciji " + nazivLokacije + " vraća unajmljeni " +
                                vrstaVozila + " koji ima ukupno " + brojKm + " km  te prijavljuje da vozilo ima problem:" + opis + "." + " Stavke računa su: 1 najam " + vrstaVozila + "a - " + cijenaNajma +
                                " kn, najam je bio " + vrijeme + " sata - " + vrijeme + " * " + cijenaPoSatu + " = " + vrijeme * cijenaPoSatu + " kn, prethodno stanje bilo je 0 km znači da je prošao " +
                                brojKm + " km - " + brojKm + " * " + cijenaPoKm + " = " + brojKm * cijenaPoKm + " kn. Račun ukupno iznosi " +
                                cijenaNajma + " kn + " + vrijeme * cijenaPoSatu + " kn + " + brojKm * cijenaPoKm + " kn = " + (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)) + " kn.");
                            listaRacuna.Add(new Tuple<int, double>(idKorisnika[0], (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm))));
                            listaRacunaZa10.Add(new Tuple<int, double, string, string, string, string>(BrojRacuna++, (cijenaNajma + (vrijeme * cijenaPoSatu) + (brojKm * cijenaPoKm)), datum, "dug", vrstaVozila, nazivLokacije));
                            Vozilo vozilo1 = new Vozilo(new StateUnajmljeno());
                            vozilo1.ZahtjevZaPromjenuStanja();
                            int smanjiBroj = brojMjesta - 1;
                            lokacijeVozila.SetBrojMjesta(smanjiBroj);
                            Console.WriteLine("Vozilo više nije dostupno!");
                        }

                    
                    }
                    
                }

                else if (id == 6)
                {
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    if (item.GetIdAktivnosti().Equals(6) && item.GetOpisAktivnosti().Length == 9)
                    {
                        Console.WriteLine("\n" + 6 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(item.GetOpisAktivnosti().Trim(), 6, args);
                    }
                    else if (item.GetIdAktivnosti().Equals(6) && item.GetOpisAktivnosti().Length == 11)
                    {
                        string novi = novi = item.GetOpisAktivnosti().Trim().Substring(10, 1);
                        var tvrtka3 = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi));
                        Console.WriteLine("\n" + 6 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtka3.PrikaziPodatke(item.GetOpisAktivnosti().Trim(), 6, args);

                    }
                    else if (item.GetIdAktivnosti().Equals(6) && item.GetOpisAktivnosti().Length == 16)
                    {
                        Console.WriteLine("\n" + 6 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStruktureStanja();
                        tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(item.GetOpisAktivnosti().Trim(), 6, args);

                    }
                    else if (item.GetIdAktivnosti().Equals(6) && item.GetOpisAktivnosti().Length == 18)
                    {
                        string novi1 = item.GetOpisAktivnosti().Trim().Substring(17, 1);
                        var tvrtka1 = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi1));
                        Console.WriteLine("\n" + 6 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStruktureStanja();
                        tvrtka1.PrikaziPodatke(item.GetOpisAktivnosti().Trim(), 6, args);

                    }

                }

                else if (id == 7)
                {
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    if (item.GetIdAktivnosti().Equals(7) && item.GetOpisAktivnosti().Length == 31)
                    {
                        Console.WriteLine("\n" + 7 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(item.GetOpisAktivnosti().Trim(), 7, args);
                    }
                    else if (item.GetIdAktivnosti().Equals(7) && item.GetOpisAktivnosti().Length == 33)
                    {
                        string novi = novi = item.GetOpisAktivnosti().Trim().Substring(32, 1);
                        var tvrtka3 = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi));
                        Console.WriteLine("\n" + 7 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtka3.PrikaziPodatke(item.GetOpisAktivnosti().Trim(), 7, args);

                    }
                }

                else if (id == 8)
                {
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    if (item.GetIdAktivnosti().Equals(8) && item.GetOpisAktivnosti().Length == 31)
                    {
                        Console.WriteLine("\n" + 8 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtkaSingleton.GetCompositeTvrtka().PrikaziPodatkeKoristiZaDjecu(item.GetOpisAktivnosti().Trim(), 8, args);
                    }
                    else if (item.GetIdAktivnosti().Equals(8) && item.GetOpisAktivnosti().Length == 33)
                    {
                        string novi = novi = item.GetOpisAktivnosti().Trim().Substring(32, 1);
                        var tvrtka3 = tvrtkaSingleton.GetCompositeTvrtka().PronadiTvrtku(int.Parse(novi));
                        Console.WriteLine("\n" + 8 + ";" + item.GetOpisAktivnosti());
                        Console.WriteLine("\n");
                        Datoteka.ZaglavljeStrukture();
                        tvrtka3.PrikaziPodatke(item.GetOpisAktivnosti().Trim(), 8, args);

                    }
                }

                else if (id == 9)
                {
                    string ispisID = "";
                    var ispisDatum = "";
                    string ispisOsoba = "";
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    var dohvatAktivnosti = tvrtkaSingleton.ListaAktivnosti.Where(x => x.GetIdAktivnosti() == 2).ToList();
                    var dohvatiId = dohvatAktivnosti.Select(x => x.GetIdKorisnika()[0]).Distinct();
                    Console.WriteLine("\n" + 9);
                    Console.WriteLine("\n");
                    Datoteka.ZaglavljeAktivnostDevet();
                    foreach (var item2 in dohvatiId)
                    {
                        var dohvatiDatum = dohvatAktivnosti.Where(x => x.GetIdKorisnika()[0] == item2).Select(x => x.GetDatum()).Last();
                        var dohvatOsob = tvrtkaSingleton.ListaOsoba.Where(x => x.GetId() == item2).ToList();
                        var dohvatOsoba = dohvatOsob.Select(x => x.GetImePrezime()).Distinct();
                        decimal stanje = 0;
                        ispisID = item2.ToString();
                        foreach (var item3 in dohvatiDatum)
                        {
                            ispisDatum += item3;
                        }
                        foreach (var item4 in dohvatOsoba)
                        {
                            ispisOsoba = item4.ToString();
                        }
                        foreach (var item5 in listaRacuna)
                        {
                            if (item5.Item1.Equals(item2))
                            {
                                stanje += decimal.Parse(item5.Item2.ToString());

                            }
                            else
                            {

                            }
                        }
                        string status = "";
                        string dodaj = "";
                        if (stanje == 0)
                        {
                            status = " kn";
                            dodaj = "00";
                        }
                        else if (stanje > 0)
                        {
                            status = " kn dug";
                        }

                        if (Dugovanje == 0)
                        {

                        }
                        else
                        {
                            if (stanje > Dugovanje)
                            {
                                ispisDugovanja = "ID: " + ispisID + ", osoba " + ispisOsoba + " premašio/la je iznos dopuštenog dugovanja! Onemogućeno je daljnje iznajmljivanje!";
                            }
                            else
                            {

                            }
                        }
                        Ispis.Brojac = 0;
                        IDecoratorRedakTablice redakTablice =
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorBroj(
                                        new DecoratorText(
                                            new DecoratorKonkretniRedak()))));
                        string format = redakTablice.KreirajRedak();
                        string output = String.Format(format, ispisDatum, new String(' ', 4) + stanje.ToString("" + dodaj + ".00") + status, new String(' ', 4) + ispisID, ispisOsoba);
                        Console.WriteLine(output);
                        if (Ispis.FormatTekst == 0)
                        {
                            Console.WriteLine(new String('-', 3 * 30));
                        }
                        else
                        {
                            Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 20));
                        }
                        ispisDatum = "";

                    }


                }

                else if (id == 10)
                {
                        
                    string dohvatBroja = item.GetOpisAktivnosti().Substring(1, 1);
                    Console.WriteLine("\n" + 10 + "; " + item.GetOpisAktivnosti());
                    Console.WriteLine("\n");
                    Datoteka.ZaglavljeAktivnostiDeset();
                    foreach (var item2 in listaRacunaZa10)
                    {
                        var triman = item2.Item3.Remove(0, 1);
                        var trimani = triman.Remove(10, 10);
                        var datum21 = DateTime.ParseExact(trimani, "yyyy-MM-dd", null);
                        var datum22 = datum21.ToString("dd.MM.yyyy");
                        var datum2 = DateTime.ParseExact(datum22, "dd.MM.yyyy", null);

                        decimal iznos = decimal.Parse(item2.Item2.ToString());
                        Ispis.Brojac = 0;
                        IDecoratorRedakTablice redakTablice =
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorText(
                                        new DecoratorText(
                                            new DecoratorBroj(
                                                new DecoratorBroj(
                                                    new DecoratorKonkretniRedak()))))));
                        string format = redakTablice.KreirajRedak();
                        string output = String.Format(format, item2.Item6, item2.Item5, item2.Item4, datum22, iznos.ToString("00.00") + "kn", new String(' ', 4) + item2.Item1);
                        Console.WriteLine(output);
                        if (Ispis.FormatTekst == 0)
                        {
                            Console.WriteLine(new String('-', 4 * 30));
                        }
                        else
                        {
                            Console.WriteLine(new String('-', 4 * Ispis.FormatTekst) + new String('-', 20));
                        }
                    }
                    
                }

                else if (id == 11)
                {

                }

                else if (id == 0)
                {
                    Console.WriteLine(id + "; " + datum);
                    Console.WriteLine("U " + datum  + " program završava s radom.");
                }
                else
                {
                    Console.WriteLine("Unjeli ste pogrešan ID!");
                }
                
            }
        }
        /// <summary>
        /// Metoda koja provjerava koja je zastavica na kojem mjestu i zatim ih učitava redom
        /// </summary>
        /// <param name="args"></param>
        public static void UcitajDatoteke(string[] args)
        {
            
            if (args[0].Equals("-v"))
            {
                VozilaTXT.UcitajPodatkeDatotekeVozila(args);
            }
            else if (args[0].Equals("-o"))
            {
                OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
            }
            else if (args[0].Equals("-c"))
            {
                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (args[0].Equals("-l"))
            {
                LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
            }
            else if (args[0].Equals("-k"))
            {
                Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (args[0].Equals("-s"))
            {
                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (args[0].Equals("-os"))
            {
                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (args[0].Equals("DZ_3_konfiguracija_1.txt") || args[0].Contains("konfiguracija"))
            {

            }

            if (args.Length == 1)
            {

            }
            else
            {

                if (args[2].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[2].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[2].Equals("-c"))
                {
                    if (args[0].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[2].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[2].Equals("-k"))
                {
                    Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (args[2].Equals("-s"))
                {
                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (args[2].Equals("-os"))
                {
                    if (args[0].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                if (args[4].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[4].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[4].Equals("-c"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[4].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[4].Equals("-k"))
                {
                    if (args[0].Equals("-v") && args[2].Equals("-l"))
                    {
                        LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                    }
                    else if (args[0].Equals("-l") && args[2].Equals("-v"))
                    {
                        LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[4].Equals("-s"))
                {
                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (args[4].Equals("-os"))
                {
                    if (args[0].Equals("-l") || args[2].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                if (args[6].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[6].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[6].Equals("-c"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[6].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[6].Equals("-k"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[6].Equals("-s"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l"))
                        {
                            if (args[0].Equals("-o") || args[2].Equals("-o") || args[4].Equals("-o"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(args);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[6].Equals("-os"))
                {
                    if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                if (args[8].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[8].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[8].Equals("-c"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[8].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[8].Equals("-k"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-v"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[8].Equals("-s"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l"))
                        {
                            if (args[0].Equals("-o") || args[2].Equals("-o") || args[4].Equals("-o") || args[6].Equals("-o"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(args);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[8].Equals("-os"))
                {
                    if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }

                if (args[10].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[10].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[10].Equals("-c"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[10].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[10].Equals("-k"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-v") || args[8].Equals("-l"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[10].Equals("-s"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l"))
                        {
                            if (args[0].Equals("-o") || args[2].Equals("-o") || args[4].Equals("-o") || args[6].Equals("-o") || args[8].Equals("-o"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(args);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[10].Equals("-os"))
                {
                    if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }


                if (args[12].Equals("-v"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                }
                else if (args[12].Equals("-o"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                }
                else if (args[12].Equals("-c"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[12].Equals("-l"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                }
                else if (args[12].Equals("-k"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-v") || args[8].Equals("-l") || args[10].Equals("-l"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[12].Equals("-s"))
                {
                    if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l") || args[10].Equals("-l"))
                        {
                            if (args[0].Equals("-o") || args[2].Equals("-o") || args[4].Equals("-o") || args[6].Equals("-o") || args[8].Equals("-o") || args[10].Equals("-o"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(args);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (args[12].Equals("-os"))
                {
                    if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l") ||
                    args[10].Equals("-l"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }


                if (args.Length == 14)
                {
                    PrikazGlavnogIzbornika(args);
                }
                else if (args.Length == 16)
                {
                    if (args[14].Equals("-v"))
                    {
                        VozilaTXT.UcitajPodatkeDatotekeVozila(args);
                    }
                    else if (args[14].Equals("-o"))
                    {
                        OsobeTXT.UcitajPodatkeDatotekeOsoba(args);
                    }
                    else if (args[14].Equals("-c"))
                    {
                        if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v")
                            || args[12].Equals("-v"))
                        {
                            CjenikTXT.UcitajPodatkeDatotekeCjenik(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else if (args[14].Equals("-l"))
                    {
                        LokacijeTXT.UcitajPodatkeDatotekeLokacije(args);
                    }
                    else if (args[14].Equals("-k"))
                    {
                        if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v")
                            || args[12].Equals("-v"))
                        {
                            if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-v") || args[8].Equals("-l") || args[10].Equals("-l")
                                || args[12].Equals("-l"))
                            {
                                LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(args);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else if (args[14].Equals("-s"))
                    {
                        if (args[0].Equals("-v") || args[2].Equals("-v") || args[4].Equals("-v") || args[6].Equals("-v") || args[8].Equals("-v") || args[10].Equals("-v")
                            || args[12].Equals("-v"))
                        {
                            if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l") || args[10].Equals("-l")
                                || args[12].Equals("-l"))
                            {
                                if (args[0].Equals("-o") || args[2].Equals("-o") || args[4].Equals("-o") || args[6].Equals("-o") || args[8].Equals("-o") || args[10].Equals("-o")
                                    || args[12].Equals("-o"))
                                {
                                    AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(args);
                                }
                                else
                                {
                                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                    Console.ReadLine();
                                    Environment.Exit(0);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else if (args[14].Equals("-os"))
                    {
                        if (args[0].Equals("-l") || args[2].Equals("-l") || args[4].Equals("-l") || args[6].Equals("-l") || args[8].Equals("-l") ||
                        args[10].Equals("-l") || args[12].Equals("-l"))
                        {
                            TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(args);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                }
            }

        }
    }
}

