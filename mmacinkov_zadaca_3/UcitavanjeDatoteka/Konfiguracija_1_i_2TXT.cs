using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.Decorator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.UcitavanjeDatoteka
{
    public class Konfiguracija_1_i_2TXT
    {
        public static FileStream odabirPodataka;
        public static StreamWriter zapisPodataka;
        public static void UcitavanjePrekoKonfiguracije(string[] args)
        {

            string putanja = SaznajPutanjuDatZaKonfig(args);
            string konfiguracijaDatotekaText = "";
            try
            {
                konfiguracijaDatotekaText = File.ReadAllText(putanja);

            }
            catch
            {
                Console.WriteLine("Pogreška kod čitanja datoteke konfiguracije.\nProvjerite da li se " +
                    "datoteka nalazi na navedenoj putanji (" + putanja + ").\nIzlazak iz programa.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("\nZapočinjem čitanje datoteke konfiguracije.");
            Console.WriteLine("Završeno čitanje datoteke konfiguracije.");
            string[] nizRedakaKonfiguracije = Datoteka.PrepoznajRetkeIzDatoteke(konfiguracijaDatotekaText, Datoteka.LINE_SPLIT);
            Console.WriteLine("Započinjem učitavanje podataka datoteke kongifuracije.");
            Console.WriteLine("Završeno učitavanje podataka konfiguracije.");
            string[] novi = nizRedakaKonfiguracije[0].Split('=');
            int broj = nizRedakaKonfiguracije.Length;
            if (nizRedakaKonfiguracije[0].Contains("izlaz") || nizRedakaKonfiguracije[1].Contains("izlaz") || nizRedakaKonfiguracije[2].Contains("izlaz") ||
                nizRedakaKonfiguracije[3].Contains("izlaz") || nizRedakaKonfiguracije[4].Contains("izlaz") || nizRedakaKonfiguracije[5].Contains("izlaz") ||
                nizRedakaKonfiguracije[6].Contains("izlaz") || nizRedakaKonfiguracije[7].Contains("izlaz"))
            {
                Console.WriteLine("Molim smjestite datoteku izlaz nakon ključeva za učitavanje datoteka i/ili aktivnosti u datoteki konfiguracija da bi pristupili upisivanju u datoteku! " +
                    "Ili, obrišite ključ izlaz da bi pristupili skupnom ili interaktivnom načinu!");

            }
            else if ((broj == 9 && nizRedakaKonfiguracije[7].Contains("aktivnosti") && nizRedakaKonfiguracije[8].Contains("izlaz")) || 
                (broj == 10 && nizRedakaKonfiguracije[7].Contains("aktivnosti") && nizRedakaKonfiguracije[9].Contains("izlaz")) || 
                (broj == 11 && nizRedakaKonfiguracije[7].Contains("aktivnosti") && nizRedakaKonfiguracije[10].Contains("izlaz")) || 
                (broj == 12 && nizRedakaKonfiguracije[7].Contains("aktivnosti") && nizRedakaKonfiguracije[11].Contains("izlaz")) || 
                (broj == 13 && nizRedakaKonfiguracije[7].Contains("aktivnosti") && nizRedakaKonfiguracije[12].Contains("izlaz")))
            {
                for (int g = 0; g < nizRedakaKonfiguracije.Length; g++)
                {
                    if (nizRedakaKonfiguracije[g].Contains("izlaz"))
                    {
                        string[] izlaz = nizRedakaKonfiguracije[g].Split('=');
                        string dohvatDatoteke = izlaz[1].Trim();
                        odabirPodataka = new FileStream(dohvatDatoteke, FileMode.Create);
                        zapisPodataka = new StreamWriter(odabirPodataka);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("\nOdabrano je spremanje izlaznih podataka u datoteku: " + dohvatDatoteke + "!");
                        Console.WriteLine("Datoteka s izlaznim podacima je spremna!");
                        Console.ResetColor();
                        Console.SetOut(zapisPodataka);
                        for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                        {
                            if (nizRedakaKonfiguracije[i].Contains("vrijeme"))
                            {
                                string[] vrijeme = nizRedakaKonfiguracije[i].Split('=');
                                if (vrijeme[0].Contains("vrijeme"))
                                {
                                    string pravilnoVrijeme = "\"" + vrijeme[1] + "\"";
                                    if (Datoteka.IspravanDatum(pravilnoVrijeme))
                                    {
                                        Aktivnost tempAktivnost = new Aktivnost();
                                        tempAktivnost.SetIdAktivnosti(99);
                                        tempAktivnost.SetDatum(pravilnoVrijeme);
                                        AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Datum i vrijeme nisu ispravni!");
                                        Environment.Exit(0);
                                    }
                                }
                            }
                            else
                            {

                            }
                        }

                        for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                        {
                            if (nizRedakaKonfiguracije[i].Contains("tekst"))
                            {
                                string[] tekst = nizRedakaKonfiguracije[i].Split('=');
                                string dohvatBrojTeksta = tekst[1].Trim();
                                if (Datoteka.IspravanInt(dohvatBrojTeksta))
                                {
                                    Ispis.FormatTekst = int.Parse(dohvatBrojTeksta);
                                }
                                else
                                {
                                    Console.WriteLine("Broj za formatiranje teksta mora biti cijeli broj!");
                                    Environment.Exit(0);
                                }
                            }
                        }

                        for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                        {
                            if (nizRedakaKonfiguracije[i].Contains("cijeli"))
                            {
                                string[] cijeli = nizRedakaKonfiguracije[i].Split('=');
                                string dohvatBrojCijeli = cijeli[1].Trim();
                                if (Datoteka.IspravanInt(dohvatBrojCijeli))
                                {
                                    Ispis.FormatCijeli = int.Parse(dohvatBrojCijeli);
                                }
                                else
                                {
                                    Console.WriteLine("Broj za formatiranje cijelog broja mora biti cijeli broj!");
                                    Environment.Exit(0);
                                }
                            }
                        }



                        if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila"))
                        {
                            novi = nizRedakaKonfiguracije[0].Split('=');
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("cjenik") && novi[0].Contains("cjenik"))
                        {
                            Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("kapaciteti") && novi[0].Contains("kapaciteti"))
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("aktivnosti") && novi[0].Contains("aktivnosti"))
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[0].Contains("struktura") && novi[0].Contains("struktura"))
                        {
                            Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (!nizRedakaKonfiguracije[0].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nNeispravni elementi u prvom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }


                        string[] novi2 = nizRedakaKonfiguracije[1].Split('=');
                        if (nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi2);
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi2);
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("cjenik") && novi2[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi2);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi2);
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("kapaciteti") && novi2[0].Contains("kapaciteti"))
                        {
                            Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("aktivnosti") && novi2[0].Contains("aktivnosti"))
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[1].Contains("struktura") && novi2[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi2);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[1].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nNeispravni elementi u drugom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                        string[] novi23 = nizRedakaKonfiguracije[2].Split('=');
                        if (nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi23);
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi23);
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("cjenik") && novi23[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi23);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi23);
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("kapaciteti") && novi23[0].Contains("kapaciteti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") && nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                            {
                                LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi23);
                            }
                            else if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") && nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                            {
                                LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi23);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("aktivnosti") && novi23[0].Contains("kapaciteti"))
                        {
                            Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                        else if (nizRedakaKonfiguracije[2].Contains("struktura") && novi23[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi23);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[2].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nNeispravni elementi u trećem retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                        string[] novi3 = nizRedakaKonfiguracije[3].Split('=');
                        if (nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi3);
                        }
                        else if (nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi3);
                        }
                        else if (nizRedakaKonfiguracije[3].Contains("cjenik") && novi3[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi3);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi3);
                        }
                        else if (nizRedakaKonfiguracije[3].Contains("kapaciteti") && novi3[0].Contains("kapaciteti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                                {
                                    LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi3);
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
                        else if (nizRedakaKonfiguracije[3].Contains("aktivnosti") && novi3[0].Contains("aktivnosti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe"))
                                    {
                                        AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi3);
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
                        else if (nizRedakaKonfiguracije[3].Contains("struktura") && novi3[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi3);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[3].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nNeispravni elementi u četvrtom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                        string[] novi4 = nizRedakaKonfiguracije[4].Split('=');
                        if (nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi4);
                        }
                        else if (nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi4);
                        }
                        else if (nizRedakaKonfiguracije[4].Contains("cjenik") && novi4[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi4);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi4);
                        }
                        else if (nizRedakaKonfiguracije[4].Contains("kapaciteti") && novi4[0].Contains("kapaciteti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                                {
                                    LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi4);
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
                        else if (nizRedakaKonfiguracije[4].Contains("aktivnosti") && novi4[0].Contains("aktivnosti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe"))
                                    {
                                        AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi4);
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
                        else if (nizRedakaKonfiguracije[4].Contains("struktura") && novi4[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi4);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[4].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nNeispravni elementi u petom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }

                        string[] novi5 = nizRedakaKonfiguracije[5].Split('=');
                        if (nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi5);
                        }
                        else if (nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi5);
                        }
                        else if (nizRedakaKonfiguracije[5].Contains("cjenik") && novi5[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi5);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi5);
                        }
                        else if (nizRedakaKonfiguracije[5].Contains("kapaciteti") && novi5[0].Contains("kapaciteti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                                {
                                    LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi5);
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
                        else if (nizRedakaKonfiguracije[5].Contains("aktivnosti") && novi5[0].Contains("aktivnosti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe"))
                                    {
                                        AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi5);
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
                        else if (nizRedakaKonfiguracije[5].Contains("struktura") && novi5[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi5);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[5].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        /*else
                        {
                            Console.WriteLine("\nNeispravni elementi u šestom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }*/

                        string[] novi6 = nizRedakaKonfiguracije[6].Split('=');
                        if (nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                        {
                            VozilaTXT.UcitajPodatkeDatotekeVozila(novi6);
                        }
                        else if (nizRedakaKonfiguracije[6].Contains("osobe") && novi6[0].Contains("osobe"))
                        {
                            OsobeTXT.UcitajPodatkeDatotekeOsoba(novi6);
                        }
                        else if (nizRedakaKonfiguracije[6].Contains("cjenik") && novi6[0].Contains("cjenik"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                            {
                                CjenikTXT.UcitajPodatkeDatotekeCjenik(novi6);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                        {
                            LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi6);
                        }
                        else if (nizRedakaKonfiguracije[6].Contains("kapaciteti") && novi6[0].Contains("kapaciteti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                                {
                                    LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi6);
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
                        else if (nizRedakaKonfiguracije[6].Contains("aktivnosti") && novi6[0].Contains("aktivnosti"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe") || nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe"))
                                    {
                                        AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi6);
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
                        else if (nizRedakaKonfiguracije[6].Contains("struktura") && novi6[0].Contains("struktura"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                            {
                                TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi6);
                            }
                            else
                            {
                                Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        else if (!nizRedakaKonfiguracije[6].Contains("vrijeme"))
                        {
                            Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                            Environment.Exit(0);
                        }
                        /*else
                        {
                            Console.WriteLine("\nNeispravni elementi u sedmom retku datoteke konfiguracije!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }*/


                        if (nizRedakaKonfiguracije.Length == broj-1)
                        {
                            Ispis.PrikazGlavnogIzbornika(args);
                        }
                        else
                        {
                            string[] novi7 = nizRedakaKonfiguracije[7].Split('=');
                            if (nizRedakaKonfiguracije[7].Contains("vozila") && novi7[0].Contains("vozila"))
                            {
                                VozilaTXT.UcitajPodatkeDatotekeVozila(novi7);
                            }
                            else if (nizRedakaKonfiguracije[7].Contains("osobe") && novi7[0].Contains("osobe"))
                            {
                                OsobeTXT.UcitajPodatkeDatotekeOsoba(novi7);
                            }
                            else if (nizRedakaKonfiguracije[7].Contains("cjenik") && novi7[0].Contains("cjenik"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                                {
                                    CjenikTXT.UcitajPodatkeDatotekeCjenik(novi7);
                                }
                                else
                                {
                                    Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                                    Console.ReadLine();
                                    Environment.Exit(0);
                                }
                            }
                            else if (nizRedakaKonfiguracije[7].Contains("lokacije") && novi7[0].Contains("lokacije"))
                            {
                                LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi7);
                            }
                            else if (nizRedakaKonfiguracije[7].Contains("kapaciteti") && novi7[0].Contains("kapaciteti"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                                    {
                                        LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi7);
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
                            else if (nizRedakaKonfiguracije[7].Contains("aktivnosti") && novi7[0].Contains("aktivnosti"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                                {
                                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                                    {
                                        if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe") || nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe") || nizRedakaKonfiguracije[6].Contains("osobe") && novi6[0].Contains("osobe"))
                                        {
                                            AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi7);
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
                            else if (nizRedakaKonfiguracije[7].Contains("struktura") && novi7[0].Contains("struktura"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                                {
                                    TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi7);
                                }
                                else
                                {
                                    Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                                    Console.ReadLine();
                                    Environment.Exit(0);
                                }
                            }

                            /*else
                            {
                                Console.WriteLine("\nNeispravni elementi u osmom retku datoteke konfiguracije!\n");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }*/


                            if ((nizRedakaKonfiguracije.Length == broj && nizRedakaKonfiguracije[g].Contains("izlaz")) || (nizRedakaKonfiguracije.Length <= broj+2 && nizRedakaKonfiguracije[g].Contains("izlaz")))
                            {

                                TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                                Ispis.PrikazSkupni(args);

                            }
                            else if ((nizRedakaKonfiguracije.Length == broj) || (nizRedakaKonfiguracije.Length <= broj+2))
                            {
                                    if (nizRedakaKonfiguracije[7].Contains("aktivnosti"))
                                    {
                                        TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                                        Ispis.PrikazSkupni(args);
                                        bool aktivnost = tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti().Equals(0));
                                        if (aktivnost == false)
                                        {
                                            Ispis.PrikazGlavnogIzbornika(args);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        Ispis.PrikazGlavnogIzbornika(args);
                                    }
                            }
                            else if (nizRedakaKonfiguracije[13].Equals("") || nizRedakaKonfiguracije[14].Equals("") || nizRedakaKonfiguracije[15].Equals(""))
                            {

                            }
                            else
                            {
                                Console.WriteLine("Nesipravan broj elemenata! Molim provjerite podatke u datoteci: " + args[0]);
                            }

                        }
                        zapisPodataka.Close();
                    }

                }
            }
            else
            {
                for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                {
                    if (nizRedakaKonfiguracije[i].Contains("vrijeme"))
                    {
                        string[] vrijeme = nizRedakaKonfiguracije[i].Split('=');
                        if (vrijeme[0].Contains("vrijeme"))
                        {
                            string pravilnoVrijeme = "\"" + vrijeme[1] + "\"";
                            if (Datoteka.IspravanDatum(pravilnoVrijeme))
                            {
                                Aktivnost tempAktivnost = new Aktivnost();
                                tempAktivnost.SetIdAktivnosti(99);
                                tempAktivnost.SetDatum(pravilnoVrijeme);
                                AktivnostiTXT.izlaznaLista.Add(tempAktivnost);
                            }
                            else
                            {
                                Console.WriteLine("Datum i vrijeme nisu ispravni!");
                                Environment.Exit(0);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("\nOdabran je skupni/interaktivni način rada!");
                Console.ResetColor();

                for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                {
                    if (nizRedakaKonfiguracije[i].Contains("tekst"))
                    {
                        string[] tekst = nizRedakaKonfiguracije[i].Split('=');
                        string dohvatBrojTeksta = tekst[1].Trim();
                        if (Datoteka.IspravanInt(dohvatBrojTeksta))
                        {
                            Ispis.FormatTekst = int.Parse(dohvatBrojTeksta);
                        }
                        else
                        {
                            Console.WriteLine("Broj za formatiranje teksta mora biti cijeli broj!");
                            Environment.Exit(0);
                        }
                    }
                }

                for (int i = 0; i < nizRedakaKonfiguracije.Length; i++)
                {
                    if (nizRedakaKonfiguracije[i].Contains("cijeli"))
                    {
                        string[] cijeli = nizRedakaKonfiguracije[i].Split('=');
                        string dohvatBrojCijeli = cijeli[1].Trim();
                        if (Datoteka.IspravanInt(dohvatBrojCijeli))
                        {
                            Ispis.FormatCijeli = int.Parse(dohvatBrojCijeli);
                        }
                        else
                        {
                            Console.WriteLine("Broj za formatiranje cijelog broja mora biti cijeli broj!");
                            Environment.Exit(0);
                        }
                    }
                }



                if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila"))
                {
                    novi = nizRedakaKonfiguracije[0].Split('=');
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi);
                }
                else if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi);
                }
                else if (nizRedakaKonfiguracije[0].Contains("cjenik") && novi[0].Contains("cjenik"))
                {
                    Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi);
                }
                else if (nizRedakaKonfiguracije[0].Contains("kapaciteti") && novi[0].Contains("kapaciteti"))
                {
                    Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[0].Contains("aktivnosti") && novi[0].Contains("aktivnosti"))
                {
                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[0].Contains("struktura") && novi[0].Contains("struktura"))
                {
                    Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (!nizRedakaKonfiguracije[0].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nNeispravni elementi u prvom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }


                string[] novi2 = nizRedakaKonfiguracije[1].Split('=');
                if (nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi2);
                }
                else if (nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi2);
                }
                else if (nizRedakaKonfiguracije[1].Contains("cjenik") && novi2[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi2);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi2);
                }
                else if (nizRedakaKonfiguracije[1].Contains("kapaciteti") && novi2[0].Contains("kapaciteti"))
                {
                    Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[1].Contains("aktivnosti") && novi2[0].Contains("aktivnosti"))
                {
                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[1].Contains("struktura") && novi2[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi2);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[1].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nNeispravni elementi u drugom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                string[] novi23 = nizRedakaKonfiguracije[2].Split('=');
                if (nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi23);
                }
                else if (nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi23);
                }
                else if (nizRedakaKonfiguracije[2].Contains("cjenik") && novi23[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi23);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi23);
                }
                else if (nizRedakaKonfiguracije[2].Contains("kapaciteti") && novi23[0].Contains("kapaciteti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") && nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                    {
                        LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi23);
                    }
                    else if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") && nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila"))
                    {
                        LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi23);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se lokacije vozila učitale prvo mora biti učitana datoteka lokacija te datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[2].Contains("aktivnosti") && novi23[0].Contains("kapaciteti"))
                {
                    Console.WriteLine("\nDa bi se aktivnosti učitale prvo mora biti učitana datoteka osoba, datoteka lokacija te datoteka vozila!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (nizRedakaKonfiguracije[2].Contains("struktura") && novi23[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi23);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[2].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nNeispravni elementi u trećem retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                string[] novi3 = nizRedakaKonfiguracije[3].Split('=');
                if (nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi3);
                }
                else if (nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi3);
                }
                else if (nizRedakaKonfiguracije[3].Contains("cjenik") && novi3[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi3);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi3);
                }
                else if (nizRedakaKonfiguracije[3].Contains("kapaciteti") && novi3[0].Contains("kapaciteti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi3);
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
                else if (nizRedakaKonfiguracije[3].Contains("aktivnosti") && novi3[0].Contains("aktivnosti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi3);
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
                else if (nizRedakaKonfiguracije[3].Contains("struktura") && novi3[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi3);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[3].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nNeispravni elementi u četvrtom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                string[] novi4 = nizRedakaKonfiguracije[4].Split('=');
                if (nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi4);
                }
                else if (nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi4);
                }
                else if (nizRedakaKonfiguracije[4].Contains("cjenik") && novi4[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi4);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi4);
                }
                else if (nizRedakaKonfiguracije[4].Contains("kapaciteti") && novi4[0].Contains("kapaciteti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi4);
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
                else if (nizRedakaKonfiguracije[4].Contains("aktivnosti") && novi4[0].Contains("aktivnosti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi4);
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
                else if (nizRedakaKonfiguracije[4].Contains("struktura") && novi4[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi4);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[4].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nNeispravni elementi u petom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }

                string[] novi5 = nizRedakaKonfiguracije[5].Split('=');
                if (nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi5);
                }
                else if (nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi5);
                }
                else if (nizRedakaKonfiguracije[5].Contains("cjenik") && novi5[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi5);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi5);
                }
                else if (nizRedakaKonfiguracije[5].Contains("kapaciteti") && novi5[0].Contains("kapaciteti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi5);
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
                else if (nizRedakaKonfiguracije[5].Contains("aktivnosti") && novi5[0].Contains("aktivnosti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi5);
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
                else if (nizRedakaKonfiguracije[5].Contains("struktura") && novi5[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi5);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[5].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                /*else
                {
                    Console.WriteLine("\nNeispravni elementi u šestom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }*/

                string[] novi6 = nizRedakaKonfiguracije[6].Split('=');
                if (nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                {
                    VozilaTXT.UcitajPodatkeDatotekeVozila(novi6);
                }
                else if (nizRedakaKonfiguracije[6].Contains("osobe") && novi6[0].Contains("osobe"))
                {
                    OsobeTXT.UcitajPodatkeDatotekeOsoba(novi6);
                }
                else if (nizRedakaKonfiguracije[6].Contains("cjenik") && novi6[0].Contains("cjenik"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                    {
                        CjenikTXT.UcitajPodatkeDatotekeCjenik(novi6);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                {
                    LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi6);
                }
                else if (nizRedakaKonfiguracije[6].Contains("kapaciteti") && novi6[0].Contains("kapaciteti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                        {
                            LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi6);
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
                else if (nizRedakaKonfiguracije[6].Contains("aktivnosti") && novi6[0].Contains("aktivnosti"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe") || nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe"))
                            {
                                AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi6);
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
                else if (nizRedakaKonfiguracije[6].Contains("struktura") && novi6[0].Contains("struktura"))
                {
                    if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije"))
                    {
                        TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi6);
                    }
                    else
                    {
                        Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (!nizRedakaKonfiguracije[6].Contains("vrijeme"))
                {
                    Console.WriteLine("Ne postoji ispravan ključ za vrijeme!");
                    Environment.Exit(0);
                }
                /*else
                {
                    Console.WriteLine("\nNeispravni elementi u sedmom retku datoteke konfiguracije!\n");
                    Console.ReadLine();
                    Environment.Exit(0);
                }*/


                if (nizRedakaKonfiguracije.Length == broj-1)
                {
                    Ispis.PrikazGlavnogIzbornika(args);
                }
                else
                {
                    string[] novi7 = nizRedakaKonfiguracije[7].Split('=');
                    if (nizRedakaKonfiguracije[7].Contains("vozila") && novi7[0].Contains("vozila"))
                    {
                        VozilaTXT.UcitajPodatkeDatotekeVozila(novi7);
                    }
                    else if (nizRedakaKonfiguracije[7].Contains("osobe") && novi7[0].Contains("osobe"))
                    {
                        OsobeTXT.UcitajPodatkeDatotekeOsoba(novi7);
                    }
                    else if (nizRedakaKonfiguracije[7].Contains("cjenik") && novi7[0].Contains("cjenik"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                        {
                            CjenikTXT.UcitajPodatkeDatotekeCjenik(novi7);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se cjenik učitao prvo mora biti učitana datoteka vozila!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    else if (nizRedakaKonfiguracije[7].Contains("lokacije") && novi7[0].Contains("lokacije"))
                    {
                        LokacijeTXT.UcitajPodatkeDatotekeLokacije(novi7);
                    }
                    else if (nizRedakaKonfiguracije[7].Contains("kapaciteti") && novi7[0].Contains("kapaciteti"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                            {
                                LokacijeVozilaTXT.UcitajPodatkeDatotekeLokacijeVozila(novi7);
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
                    else if (nizRedakaKonfiguracije[7].Contains("aktivnosti") && novi7[0].Contains("aktivnosti"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("vozila") && novi[0].Contains("vozila") || nizRedakaKonfiguracije[1].Contains("vozila") && novi2[0].Contains("vozila") || nizRedakaKonfiguracije[2].Contains("vozila") && novi23[0].Contains("vozila") || nizRedakaKonfiguracije[3].Contains("vozila") && novi3[0].Contains("vozila") || nizRedakaKonfiguracije[4].Contains("vozila") && novi4[0].Contains("vozila") || nizRedakaKonfiguracije[5].Contains("vozila") && novi5[0].Contains("vozila") || nizRedakaKonfiguracije[6].Contains("vozila") && novi6[0].Contains("vozila"))
                        {
                            if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                            {
                                if (nizRedakaKonfiguracije[0].Contains("osobe") && novi[0].Contains("osobe") || nizRedakaKonfiguracije[1].Contains("osobe") && novi2[0].Contains("osobe") || nizRedakaKonfiguracije[2].Contains("osobe") && novi23[0].Contains("osobe") || nizRedakaKonfiguracije[3].Contains("osobe") && novi3[0].Contains("osobe") || nizRedakaKonfiguracije[4].Contains("osobe") && novi4[0].Contains("osobe") || nizRedakaKonfiguracije[5].Contains("osobe") && novi5[0].Contains("osobe") || nizRedakaKonfiguracije[6].Contains("osobe") && novi6[0].Contains("osobe"))
                                {
                                    AktivnostiTXT.UcitajPodatkeDatotekeAktivnosti(novi7);
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
                    else if (nizRedakaKonfiguracije[7].Contains("struktura") && novi7[0].Contains("struktura"))
                    {
                        if (nizRedakaKonfiguracije[0].Contains("lokacije") && novi[0].Contains("lokacije") || nizRedakaKonfiguracije[1].Contains("lokacije") && novi2[0].Contains("lokacije") || nizRedakaKonfiguracije[2].Contains("lokacije") && novi23[0].Contains("lokacije") || nizRedakaKonfiguracije[3].Contains("lokacije") && novi3[0].Contains("lokacije") || nizRedakaKonfiguracije[4].Contains("lokacije") && novi4[0].Contains("lokacije") || nizRedakaKonfiguracije[5].Contains("lokacije") && novi5[0].Contains("lokacije") || nizRedakaKonfiguracije[6].Contains("lokacije") && novi6[0].Contains("lokacije"))
                        {
                            TvrtkaTXT.UcitajPodatkeDatotekeTvrtka(novi7);
                        }
                        else
                        {
                            Console.WriteLine("\nDa bi se tvrtka učitala prvo mora biti učitana datoteka lokacija!\n");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }

                    /*else
                    {
                        Console.WriteLine("\nNeispravni elementi u osmom retku datoteke konfiguracije!\n");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }*/


                    if ((nizRedakaKonfiguracije.Length == broj) || (nizRedakaKonfiguracije.Length <= broj+2))
                    {
                            if (nizRedakaKonfiguracije[7].Contains("aktivnosti"))
                            {
                                TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                                Ispis.PrikazSkupni(args);
                                bool aktivnost = tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti().Equals(0));
                                if (aktivnost == false)
                                {
                                    Ispis.PrikazGlavnogIzbornika(args);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                Ispis.PrikazGlavnogIzbornika(args);
                            }
                        
                        
                    }
                    else if (nizRedakaKonfiguracije[13].Equals("") || nizRedakaKonfiguracije[14].Equals("") || nizRedakaKonfiguracije[15].Equals(""))
                    {

                    }
                    else
                    {
                        Console.WriteLine("Nesipravan broj elemenata! Molim provjerite podatke u datoteci: " + args[0]);
                    }

                }
            }

        }



        private static string SaznajPutanjuDatZaKonfig(string[] args)
        {
            for (int i = 0; i <= args.Length - 1; i++)
            {
                if (args[i].Trim() == "DZ_3_konfiguracija_1.txt")
                {
                    return args[i].Trim();
                }
                else if (args[i].Trim() == "DZ_3_konfiguracija_2.txt")
                {
                    return args[i].Trim();
                }
                else if (args[i].Trim().Contains("konfiguracija"))
                {
                    return args[i].Trim();
                }
                else
                {
                    Console.WriteLine("Pogrešno uneseni parametri!");
                    Environment.Exit(0);
                }
            }
            return "";
        }
    }
}
