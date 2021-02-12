using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.UcitavanjeDatoteka;
using mmacinkov_zadaca_3.Uzorci.Decorator;

namespace mmacinkov_zadaca_3.Helperi
{
    class Datoteka
    {
        public static readonly char LINE_SPLIT = '\n';
        public static readonly char ATTR_SPLIT = ';';
        /// <summary>
        /// Varijable koje služe za provjeru regularnih izraza za testirane datoteke i potrebni datum
        /// </summary>
        private static readonly string provjeraDatoteka = @"^(([a-zA-Z\d\s-_#\\:.!]{1,250})(\.txt))$";
        public static readonly string provjeraDatuma = "^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]$";
        public static readonly string provjeriFormatDatuma = @"^([0-2][0-9]|(3)[0-1])(\.)(((0)[0-9])|((1)[0-2]))(\.)\d{4}$";
        
        /*public static int JednoznacniIDVozila = 0;

        public static void DodjeliJednoznacniID(Vozilo vozilo)
        {
            JednoznacniIDVozila++;
            vozilo.SetJednoznacniID(JednoznacniIDVozila);
        }*/
        /// <summary>
        /// Metoda koja služi za provjeru ulaznih parametara
        /// </summary>
        /// <param name="args"></param>
        public static void ProvjeraUlaznihParametara(string[] args)
        {
            if (ProvjeraInteraktivni(args, provjeraDatoteka, provjeraDatuma))
            {
                Console.WriteLine("\nUlazni parametri interaktivnog načina rada su pravilni. Pokrećem program!");
            }
            else if (ProvjeraSkupni(args, provjeraDatoteka, provjeraDatuma))
            {
                Console.WriteLine("\nUlazni parametri skupnog načina rada su pravilni. Pokrećem program!");
            }
            else if (args.Length==1)
            {
                Konfiguracija_1_i_2TXT.UcitavanjePrekoKonfiguracije(args);
            }
            else
            {
                Console.WriteLine("\nUlazni parametri nisu ispravno uneseni. Gasim program!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Metoda za dohvat mjesta na kojem je unesen datum
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Datum</returns>
        public static string DohvatiUneseniDatum(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Contains("-t"))
                {
                    return args[i + 1].ToString();
                }
                else if (args[0].Contains("konfiguracija"))
                {
                    if (AktivnostiTXT.izlaznaLista.Count == 0)
                    {
                        return "0000-00-00 00:00:00";
                    }
                    else
                    {
                        Aktivnost prva = AktivnostiTXT.izlaznaLista.First();
                        string dohvat = prva.GetDatum();
                        string subsDohvat = dohvat.Substring(1, 19);
                        return subsDohvat;
                    }
                }
                else if (args[1].Trim().Contains("aktivnosti"))
                {
                    if (AktivnostiTXT.izlaznaLista.Count == 0)
                    {
                        return "0000-00-00 00:00:00";
                    }
                    else
                    {
                        Aktivnost prva = AktivnostiTXT.izlaznaLista.First();
                        string dohvat = prva.GetDatum();
                        string subsDohvat = dohvat.Substring(1, 19);
                        return subsDohvat;
                    }
                }
                else
                {

                }
            }
            return "";
        }

        /// <summary>
        /// Metoda koja služi za provjeru interaktivnog načina rada
        /// </summary>
        /// <param name="args"></param>
        /// <param name="provjera"></param>
        /// <param name="datum"></param>
        /// <returns>Vraća true ako su ispravno uneseni svi parametri za interaktivni rad, inače vraća false</returns>
        
        public static bool ProvjeraInteraktivni(string[] args, string provjera, string datum)
        {
            if (args.Length != 14 || !SadrziSvakuPotrebnuZastavicu(args))
            {
                return false;
            }
            else
            {
                if (!ParametriIspravnoRasporedeni(args))
                    return false;
                else
                {
                    if (args[0].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], datum);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[2].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], datum);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[4].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], datum);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[6].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], datum);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[8].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], datum);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[10].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], datum);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[12].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], datum);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }


        /// <summary>
        /// Metoda koja služi za provjeru skupnog načina rada
        /// </summary>
        /// <param name="args"></param>
        /// <param name="provjera"></param>
        /// <param name="datum"></param>
        /// <returns>Vraća true ako su ispravno uneseni svi parametri za skupno rad, inače vraća false</returns>
        
        public static bool ProvjeraSkupni(string[] args, string provjera, string datum)
        {
            if (args.Length != 16 || !SadrziSvakuPotrebnuZastavicuSkupni(args))
            {
                return false;
            }
            else
            {
                if (!ParametriIspravnoRasporedeniSkupni(args))
                    return false;
                else
                {
                    if (args[0].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], datum);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[2].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], datum);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[4].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], datum);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[6].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], datum);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[8].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], datum);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[10].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], datum);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[12].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], datum);
                        Match regularniIzraz15 = Regex.Match(args[15], provjera);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (args[14].Contains("-t"))
                    {
                        Match regularniIzraz1 = Regex.Match(args[1], provjera);
                        Match regularniIzraz3 = Regex.Match(args[3], provjera);
                        Match regularniIzraz5 = Regex.Match(args[5], provjera);
                        Match regularniIzraz7 = Regex.Match(args[7], provjera);
                        Match regularniIzraz9 = Regex.Match(args[9], provjera);
                        Match regularniIzraz11 = Regex.Match(args[11], provjera);
                        Match regularniIzraz13 = Regex.Match(args[13], provjera);
                        Match regularniIzraz15 = Regex.Match(args[15], datum);

                        if (regularniIzraz1.Success && regularniIzraz3.Success &&
                            regularniIzraz5.Success && regularniIzraz7.Success && regularniIzraz9.Success &&
                            regularniIzraz11.Success && regularniIzraz13.Success && regularniIzraz15.Success)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        /// <summary>
        /// Metoda koja provjerava zastavice kod interaktivnog načina
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraća true ako je prava zastavica inače vraća false</returns>
        private static bool JeZastavica(string podatak)
        {
            if (podatak == "-v")
                return true;
            else if (podatak == "-l")
                return true;
            else if (podatak == "-c")
                return true;
            else if (podatak == "-k")
                return true;
            else if (podatak == "-o")
                return true;
            else if (podatak == "-t")
                return true;
            else if (podatak == "-os")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda koja provjerava jesu li parametri ispravno raspoređeni kod interaktivnog načina
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća false ako je kriv raspored, inače vraća true</returns>
        private static bool ParametriIspravnoRasporedeni(string[] args)
        {
            if (!JeZastavica(args[0].Trim()))
                return false;
            else if (!JeZastavica(args[2].Trim()))
                return false;
            else if (!JeZastavica(args[4].Trim()))
                return false;
            else if (!JeZastavica(args[6].Trim()))
                return false;
            else if (!JeZastavica(args[8].Trim()))
                return false;
            else if (!JeZastavica(args[10].Trim()))
                return false;
            else if (!JeZastavica(args[12].Trim()))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Metoda koja provjerava sadrži li unos sve potrebne zastavice kod interaktivnog načina
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća false ako ne sadrži neku zastavicu, ako sadrži sve vraća true</returns>
        private static bool SadrziSvakuPotrebnuZastavicu(string[] args)
        {
            List<string> listaArgumenata = new List<string>();
            foreach (string arg in args)
            {
                listaArgumenata.Add(arg.Trim());
            }

            if (!listaArgumenata.Contains("-v"))
                return false;
            else if (!listaArgumenata.Contains("-l"))
                return false;
            else if (!listaArgumenata.Contains("-c"))
                return false;
            else if (!listaArgumenata.Contains("-k"))
                return false;
            else if (!listaArgumenata.Contains("-o"))
                return false;
            else if (!listaArgumenata.Contains("-t"))
                return false;
            else if (!listaArgumenata.Contains("-os"))
                return false;
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Metoda koja provjerava zastavice kod skupnog načina
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraća true ako je prava zastavica inače vraća false</returns>
        private static bool JeZastavicaSkupni(string podatak)
        {
            if (podatak == "-v")
                return true;
            else if (podatak == "-l")
                return true;
            else if (podatak == "-c")
                return true;
            else if (podatak == "-k")
                return true;
            else if (podatak == "-o")
                return true;
            else if (podatak == "-t")
                return true;
            else if (podatak == "-s")
                return true;
            else if (podatak == "-os")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda koja provjerava jesu li parametri ispravno raspoređeni kod skupnog načina
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća false ako je kriv raspored, inače vraća true</returns>
        private static bool ParametriIspravnoRasporedeniSkupni(string[] args)
        {
            if (!JeZastavicaSkupni(args[0].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[2].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[4].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[6].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[8].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[10].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[12].Trim()))
                return false;
            else if (!JeZastavicaSkupni(args[14].Trim()))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Metoda koja provjerava sadrži li unos sve potrebne zastavice kod skupnog načina
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Vraća false ako ne sadrži neku zastavicu, ako sadrži sve vraća true</returns>
        private static bool SadrziSvakuPotrebnuZastavicuSkupni(string[] args)
        {
            List<string> listaArgumenata = new List<string>();
            foreach (string arg in args)
            {
                listaArgumenata.Add(arg.Trim());
            }

            if (!listaArgumenata.Contains("-v"))
                return false;
            else if (!listaArgumenata.Contains("-l"))
                return false;
            else if (!listaArgumenata.Contains("-c"))
                return false;
            else if (!listaArgumenata.Contains("-k"))
                return false;
            else if (!listaArgumenata.Contains("-o"))
                return false;
            else if (!listaArgumenata.Contains("-t"))
                return false;
            else if (!listaArgumenata.Contains("-s"))
                return false;
            else if (!listaArgumenata.Contains("-os"))
                return false;
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Metoda koja služi za dohvaćanje mjesta datoteke
        /// </summary>
        /// <param name="lokacijaDatoteke"></param>
        /// <returns>Vraća lokaciju mjesta datoteke</returns>
        public static string DohvatiLokacijuMjestaDatoteke(string lokacijaDatoteke)
        {
            string lokacijaMjesta = new FileInfo(lokacijaDatoteke).Directory.FullName;
            return lokacijaMjesta;
        }

        /// <summary>
        /// Metoda koja služi za prepoznavanje redaka koji se učitavaju iz datoteke
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splitter"></param>
        /// <returns>Vraća listu sa podacima</returns>
        public static string[] PrepoznajRetkeIzDatoteke(string input, char splitter)
        {
            string[] izlaznaLista = input.Split(splitter);
            for (int i = 0; i < izlaznaLista.Length; i++)
            {
                izlaznaLista[i].Trim();
            }
            return izlaznaLista;
        }
        /// <summary>
        /// Metoda koja služi za provjeru broja atributa
        /// </summary>
        /// <param name="inputObjekt"></param>
        /// <param name="broj"></param>
        /// <returns>Ako je broj redaka ispravan vraća true, inače false</returns>
        public static bool ProvjeriBrojAtributa(string[] inputObjekt, int broj)
        {
            if (inputObjekt.Length == broj)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda koja provjerava je li unešen integer
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraća testirani podatak</returns>
        public static bool IspravanInt(string podatak)
        {
            if (!int.TryParse(podatak.Trim(), out int tempBroj))
                return false;
            else
                return true;
        }

        /// <summary>
        /// etoda koja provjerava je li unešen double
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns></returns>
        public static bool IspravanDouble(string podatak)
        {
            if (!double.TryParse(podatak.Trim(), out double tempBroj))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Metoda koja provjerava je li unešen string
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraća false ako podatak nije unešen, inače vraća true.</returns>
        public static bool IspravanString(string podatak)
        {
            if (podatak.Trim().Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Metoda koja provjerava je li datum ispravan preko regularnog izraza
        /// </summary>
        /// <param name="podatak"></param>
        /// <returns>Vraća true ako je izraz uspješan, inače false</returns>
        public static bool IspravanDatum(string podatak)
        {
            string pod = podatak.Trim();
            string podatak2 = pod.Substring(1, 19);
            Match regularniIzraz1 = Regex.Match(podatak2, provjeraDatuma);
            if (regularniIzraz1.Success)
            {
                return true;
               
            }
            else
            {
                return false;
            }
        }

        public static bool IspravanFormatDatuma(string podatak)
        {
            string pod = podatak.Trim();
            Match regularniIzraz1 = Regex.Match(pod, provjeriFormatDatuma);
            if (regularniIzraz1.Success)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public static bool ProvjeraDaLiJeDatumVeciOdPrethodnog(string podatak, List<Aktivnost> listaAktivnosti, string[] args)
        {
            DateTime datumUnesenoT = DateTime.Parse(DohvatiUneseniDatum(args));
            string pod = podatak.Trim();
            string podatak2 = pod.Substring(1, 19);
            var datumAktivnosti = DateTime.ParseExact(podatak2, "yyyy-MM-dd HH:mm:ss", null);
            if (listaAktivnosti.Count == 0)
            {
                if (datumAktivnosti <= datumUnesenoT)
                {
                    Console.Write("Datum/vrijeme mora biti veći od početnog! -->");
                    return false;
                }
                else
                {
                    
                }
            }
            else
            {
                Aktivnost prosla = listaAktivnosti.Last<Aktivnost>();
                if (prosla.GetDatum() == null)
                {
                    var dateTimeProslo1 = DateTime.ParseExact("2000-05-05 00:00:00", "yyyy-MM-dd HH:mm:ss", null);
                    if (datumAktivnosti <= datumUnesenoT)
                    {
                        Console.Write("Datum/vrijeme mora biti veći od početnog! -->");
                        return false;
                    }
                    else if (datumAktivnosti <= dateTimeProslo1)
                    {
                        Console.Write("Datum/vrijeme mora biti veći od datum/vrijeme prethodne aktivnosti! --> ");
                        return false;
                    }
                }
                else
                {
                    string pod2 = prosla.GetDatum().Trim();
                    string podatak3 = pod2.Substring(1, 19);
                    var dateTimeProslo = DateTime.ParseExact(podatak3, "yyyy-MM-dd HH:mm:ss", null);
                    if (datumAktivnosti <= datumUnesenoT)
                    {
                        Console.Write("Datum/vrijeme mora biti veći od početnog! -->");
                        return false;
                    }
                    else if (datumAktivnosti <= dateTimeProslo)
                    {
                        Console.Write("Datum/vrijeme mora biti veći od datum/vrijeme prethodne aktivnosti! --> ");
                        return false;
                    }
                }
                
            }
            return true;
        }

        public static bool ProvjeriSamoBrojevi(string str)
        {
            foreach (char item in str)
            {
                if (item < '0' || item > '9' || item == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ProvjeriPostojiLiKorisnikSPosudbom(int idKorisnika, int idVozila)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila) == false))
            {
                Osoba osoba = tvrtkaSingleton.ListaOsoba.Find(x => x.GetId() == idKorisnika);
                string imePrezime = osoba.GetImePrezime();
                Vozilo vozilo = tvrtkaSingleton.ListaVozila.Find(x => x.GetId() == idVozila);
                string nazivVozila = vozilo.GetNazivVozila();
                Console.Write(imePrezime + " smije imati samo jedan aktivni najam " + nazivVozila + "a! Prvo vratite aktivni da možete iznajmiti novi ili odaberite drugu vrstu vozila! --> ");
                return false;
            }
            else
            {
                    return true;
                
                /*if (aktivnost == true)
                {
                    Console.Write(imePrezime + " smije imati samo jedan aktivni najam " + nazivVozila + "a! Prvo vratite aktivni da možete iznajmiti novi ili odaberite drugu vrstu vozila! --> ");

                    return false;
                }
                else
                {*/
                //if (aktivnost.GetIdAktivnosti().Equals(2))
                //{
                //return true;
                //}
                /*else if (aktivnost.GetIdAktivnosti().Equals(3) || aktivnost.GetIdAktivnosti().Equals(4))
                {
                    return true;
                }
                else
                {
                    return true;
                }*/

                //}
            }
            

        }

        public static bool ProvjeraPostojeLiRaspolozivaVozila(int idLokacija, int idVozila)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            Lokacija lokacija = tvrtkaSingleton.ListaLokacija.Find(x => x.GetId() == idLokacija);
            string imeLokacije = lokacija.GetNazivLokacije();
            Vozilo vozilo = tvrtkaSingleton.ListaVozila.Find(x => x.GetId() == idVozila);
            string nazivVozila = vozilo.GetNazivVozila();
            LokacijeVozila lokacijeVozila = tvrtkaSingleton.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));

            if (lokacijeVozila.GetRaspolozivihMjesta() <= 0)
            {
                Console.Write("Na lokaciji " + imeLokacije + " ne postoje raspoloživi " + nazivVozila + "i! --> ");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ProvjeravaJeLiBrojKmVeciOdDometa(int idVozila, int brojKm)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            Vozilo vozilo = tvrtkaSingleton.ListaVozila.Find(x => x.GetId() == idVozila);
            int domet = vozilo.GetDomet();
            if (brojKm > domet)
            {
                Console.Write("Unjeli ste krivi broj kilometara! Najveća kilometraža(domet) ovog vozila je: " + domet + " km! --> ");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ProvjeraPostojiLiBrojMjesta(int idLokacija, int idVozila)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            Lokacija lokacija = tvrtkaSingleton.ListaLokacija.Find(x => x.GetId() == idLokacija);
            string imeLokacije = lokacija.GetNazivLokacije();
            Vozilo vozilo = tvrtkaSingleton.ListaVozila.Find(x => x.GetId() == idVozila);
            string nazivVozila = vozilo.GetNazivVozila();
            LokacijeVozila lokacijeVozila = tvrtkaSingleton.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));

            if (lokacijeVozila.GetBrojMjesta() <= 0)
            {
                Console.Write("Na lokaciji " + imeLokacije + " ne postoje mjesta za vratiti " + nazivVozila + "e! --> ");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ProvjeraMozeLiKorisnikVratitiVoziloNaLokaciju(int idLokacija, int idVozila, int idKorisnika)
        {
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            Osoba osoba = tvrtkaSingleton.ListaOsoba.Find(x => x.GetId() == idKorisnika);
            string imePrezime = osoba.GetImePrezime();
            Lokacija lokacija = tvrtkaSingleton.ListaLokacija.Find(x => x.GetId() == idLokacija);
            string imeLokacije = lokacija.GetNazivLokacije();
            Vozilo vozilo = tvrtkaSingleton.ListaVozila.Find(x => x.GetId() == idVozila);
            string nazivVozila = vozilo.GetNazivVozila();
            LokacijeVozila lokacijeVozila = tvrtkaSingleton.ListaLokacijaVozila.Find(x => x.GetIdLokacije().Contains(idLokacija) && x.GetIdVrsteVozila().Contains(idVozila));
            if (AktivnostiTXT.izlaznaLista.Exists(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila)) == false)
            {
                Console.Write(imePrezime + " ne može vratiti " + nazivVozila + " na lokaciju " + imeLokacije + " jer nema aktivni najam odabranog vozila! --> ");
                return false;
            }
            else
            {
                Aktivnost aktivnost = AktivnostiTXT.izlaznaLista.Find(x => x.GetIdAktivnosti().Equals(2) && x.GetIdKorisnika().Contains(idKorisnika) && x.GetIdVrsteVozila().Contains(idVozila));
                if (aktivnost == null)
                {
                    Console.Write(imePrezime + " ne može vratiti " + nazivVozila + " na lokaciju " + imeLokacije + " jer nema aktivni najam odabranog vozila! --> ");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }

        public static bool ProvjeraDaSuSveLokacijeUkljucene()
        {
            /*
            int duljina = tvrtkaSingleton.ListaLokacija.Count();
            bool tvrtka = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(duljina) && x.GetLokacije().Contains(13) && x.GetLokacije().Contains(12)
            && x.GetLokacije().Contains(11) && x.GetLokacije().Contains(10) && x.GetLokacije().Contains(9) && x.GetLokacije().Contains(8) && x.GetLokacije().Contains(7)
            && x.GetLokacije().Contains(6) && x.GetLokacije().Contains(5) && x.GetLokacije().Contains(4) && x.GetLokacije().Contains(3) && x.GetLokacije().Contains(2)
            && x.GetLokacije().Contains(1));
            if (tvrtka == false)
            {
                Console.WriteLine("NEVALJA");
                return false;
            }
            else
            {
                return true;
            }*/
            TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
            bool tvrtka = tvrtkaSingleton.ListaTvrtki.Exists(x=>x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[0]));
            bool tvrtka2 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[1]));
            bool tvrtka3 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[2]));
            bool tvrtka4 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[3]));
            bool tvrtka5 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[4]));
            bool tvrtka6 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[5]));
            bool tvrtka7 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[6]));
            bool tvrtka8 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[7]));
            bool tvrtka9 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[8]));
            bool tvrtka10 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[9]));
            bool tvrtka11 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[10]));
            bool tvrtka12 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[11]));
            bool tvrtka13 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[12]));
            bool tvrtka14 = tvrtkaSingleton.ListaTvrtki.Exists(x => x.GetLokacije().Contains(tvrtkaSingleton.ListaLokacija[13]));
            if (tvrtka == true && tvrtka2 == true && tvrtka3 == true && tvrtka4 == true && tvrtka5 == true && tvrtka6 == true && tvrtka7 == true &&
                tvrtka8 == true && tvrtka9 == true && tvrtka10 == true && tvrtka11 == true && tvrtka12 == true && tvrtka13 == true && tvrtka14 == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("GREŠKA! --> Sve lokacije moraju biti uključene u tvrtke! --> GASIM PROGRAM!");
                Console.ReadLine();
                Environment.Exit(0);
                return false;
            }
            
            





        }

        public static void ZaglavljeStrukture()
        {
            Ispis.Brojac = 0;
            IDecoratorRedakTablice redakTablice =
                new DecoratorText(
                    new DecoratorText(
                        new DecoratorText(
                            new DecoratorKonkretniRedak())));
            string format = redakTablice.KreirajRedak();
            string output = String.Format(format, "LOKACIJA:", "NADREĐENA:", "JEDINICA:");
            if (Ispis.FormatTekst == 0)
            {
                Console.WriteLine(new String('-', 3 * 30));
            }
            else
            {
                Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
            }
            Console.WriteLine(output);
            if (Ispis.FormatTekst == 0)
            {
                Console.WriteLine(new String('-', 3 * 30));
            }
            else
            {
                Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
            }
        }

        public static void ZaglavljeStruktureStanja()
        {
            Ispis.Brojac = 0;
            IDecoratorRedakTablice redakTablice =
                new DecoratorBroj(
                    new DecoratorBroj(
                        new DecoratorBroj(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorText(
                                        new DecoratorText(
                                        new DecoratorKonkretniRedak())))))));
            string format = redakTablice.KreirajRedak();
            string output = String.Format(format, "NV:", "RM: ", "BM:", "VOZILO:", "LOKACIJA:", "NADREĐENA:", "JEDINICA:");
            Console.WriteLine(new String('-', 21*Ispis.FormatCijeli));
            Console.WriteLine(output);
            Console.WriteLine(new String('-', 21*Ispis.FormatCijeli));
        }

        public static void ZaglavljeAktivnostDevet()
        {
            Ispis.Brojac = 0;
            IDecoratorRedakTablice redakTablice =
                new DecoratorText(
                    new DecoratorText(
                        new DecoratorBroj(
                            new DecoratorText(
                                new DecoratorKonkretniRedak()))));
            string format = redakTablice.KreirajRedak();
            string output = String.Format(format, "ZADNJI NAJAM:", "STANJE: ", "ID:", "KORISNIK:");
            if (Ispis.FormatTekst == 0)
            {
                Console.WriteLine(new String('-', 3 * 30));
            }
            else
            {
                Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 20));
            }
            Console.WriteLine(output);
            if (Ispis.FormatTekst == 0)
            {
                Console.WriteLine(new String('-', 3 * 30));
            }
            else
            {
                Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 20));
            }
        }

        public static void ZaglavljeAktivnostiDeset()
        {
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
            string output = String.Format(format, new String(' ', 1) + "LOKACIJA:", new String(' ', 1) + "VOZILO:"," STATUS:", new String(' ', 1) + "DATUM: ", "IZNOS:", "BROJ:");
            if (Ispis.FormatTekst == 0)
            {
                Console.WriteLine(new String('-', 4 * 30));
            }
            else
            {
                Console.WriteLine(new String('-', 4 * Ispis.FormatTekst) + new String('-', 20));
            }
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
}
