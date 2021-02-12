using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Uzorci.Composite;
using mmacinkov_zadaca_3.Uzorci.Decorator;
using mmacinkov_zadaca_3.Uzorci.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Tvrtka : IComponentTvrtka
    {
        public int IDTvrtke;
        public string NazivJedinice;
        public Tvrtka NadredenaLokacija;
        public List<Lokacija> Lokacije = new List<Lokacija>();
        

        private List<IComponentTvrtka> kreiranaListaDjece = new List<IComponentTvrtka>();

        public void SetIDTvrtke(int idTvrtke)
        {
            IDTvrtke = idTvrtke;
        }

        public int GetIDTvrtke()
        {
            return IDTvrtke;
        }

        public void SetNaziv(string naziv)
        {
            NazivJedinice = naziv;
        }

        public string GetNaziv()
        {
            return NazivJedinice;
        }

        public void SetNadredena(Tvrtka nadredena)
        {
            NadredenaLokacija = nadredena;
        }

        public Tvrtka GetNadredena()
        {
            return NadredenaLokacija;
        }

        public void SetLokacije(List<Lokacija> lista2)
        {
            Lokacije = lista2;
        }

        public List<Lokacija> GetLokacije()
        {
            return Lokacije;
        }

        public void PrikaziPodatke(string izbor, int idAktivnosti, string[] args)
        {
            string ispisLokacije = "";
            string ispisNadredene = "";
            string ispisLokacije1 = "";
            string ispisNadredene1 = "";
            string ispisLokacije2 = "";
            string ispisNadredene2 = "";
            string ispisLokacije3 = "";
            string ispisNadredene3 = "";
            Lokacija lokacija = new Lokacija();
            if (idAktivnosti == 6)
            {
                if (izbor.Trim() == "struktura")
                {
                    
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije += " ";
                                }
                            }
                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije == "")
                    {
                        ispisLokacije = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format = redakTablice.KreirajRedak();
                    string output = String.Format(format, ispisLokacije, ispisNadredene, this.NazivJedinice);
                    Console.WriteLine(output);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3*30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3*Ispis.FormatTekst) + new String('-', 5));
                    }
                    
                }
                else if (izbor.Contains("struktura") && izbor.Trim().Length == 11)
                {
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije1 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije1 += " ";
                                }
                            }
                            
                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije1 += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije1 += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije1 == "")
                    {
                        ispisLokacije1 = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene1 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene1 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice1 =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format1 = redakTablice1.KreirajRedak();
                    string output1 = String.Format(format1, ispisLokacije1, ispisNadredene1, this.NazivJedinice);
                    Console.WriteLine(output1);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3 * 30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
                    }
                }

                else if (izbor.Contains("struktura") && izbor.Contains("stanje") && izbor.Length == 16)
                {
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        var dohvatVozila = tvrtkaSingleton.ListaLokacijaVozila.Where(x => x.GetIdLokacije()[0] == Lokacije[i].GetId()).ToList();
                        
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije2 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije2 += " ";
                                }
                            }

                        }

                        int brojNeispravnih = 0;
                        
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije2 += Lokacije[i].GetNazivLokacije();
                            ispisLokacije2 += new String(' ', Ispis.FormatTekst + 1 - Lokacije[i].GetNazivLokacije().Length);
                            foreach (var item in dohvatVozila)
                            {
                                if (tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti() == 4 && x.GetOpisProblema() != null) == true)
                                {
                                    var dohvat = tvrtkaSingleton.ListaAktivnosti.Find(x => x.GetIdAktivnosti().Equals(4) && x.GetOpisProblema() != null);
                                    int idKorisnika = dohvat.GetIdKorisnika()[0];
                                    int dohvatLokacija = dohvat.GetIdLokacije()[0]; 
                                    int dohvatVrsteVozila = dohvat.GetIdVrsteVozila()[0];
                                    var dohvatNeispravna = tvrtkaSingleton.ListaLokacijaVozila.Where(x=>x.GetIdLokacije()[0] == Lokacije[i].GetId() && x.GetIdVrsteVozila()[0] == item.GetIdVrsteVozila()[0]).ToList();
                                    foreach (var item2 in dohvatNeispravna)
                                    {
                                        if (item.GetIdVrsteVozila()[0] == dohvatVrsteVozila && item.GetIdLokacije()[0] == dohvatLokacija)
                                        {
                                            brojNeispravnih++;
                                        }
                                        else
                                        {
                                            brojNeispravnih = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    brojNeispravnih = 0;
                                }
                                ispisLokacije2 += tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila()
                                    + new String(' ', Ispis.FormatTekst + Ispis.FormatCijeli - tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila().Length)
                                    + item.GetBrojMjesta() + new String(' ', Ispis.FormatCijeli) + item.GetRaspolozivihMjesta() + new String(' ', Ispis.FormatCijeli)
                                    + brojNeispravnih
                                    + Environment.NewLine + new String(' ', (3 * Ispis.FormatTekst) + Ispis.FormatCijeli);
                            }
                            brojNeispravnih = 0;
                        }
                        else
                        {
                            ispisLokacije2 += Lokacije[i].GetNazivLokacije();
                            ispisLokacije2 += new String(' ', Ispis.FormatTekst + 1 - Lokacije[i].GetNazivLokacije().Length);
                            foreach (var item in dohvatVozila)
                            {
                                if (tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti() == 4 && x.GetOpisProblema() != null) == true)
                                {
                                    var dohvat = tvrtkaSingleton.ListaAktivnosti.Find(x => x.GetIdAktivnosti().Equals(4) && x.GetOpisProblema() != null);
                                    int idKorisnika = dohvat.GetIdKorisnika()[0];
                                    int dohvatLokacija = dohvat.GetIdLokacije()[0]; 
                                    int dohvatVrsteVozila = dohvat.GetIdVrsteVozila()[0];
                                    var dohvatNeispravna = tvrtkaSingleton.ListaLokacijaVozila.Where(x => x.GetIdLokacije()[0] == Lokacije[i].GetId() && x.GetIdVrsteVozila()[0] == item.GetIdVrsteVozila()[0]).ToList();
                                    foreach (var item2 in dohvatNeispravna)
                                    {
                                        if (item.GetIdVrsteVozila()[0] == dohvatVrsteVozila && item.GetIdLokacije()[0] == dohvatLokacija)
                                        {
                                            brojNeispravnih++;
                                        }
                                        else
                                        {
                                            brojNeispravnih = 0;
                                        }

                                    }
                                }
                                else
                                {
                                    brojNeispravnih = 0;
                                }
                                ispisLokacije2 += tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila() 
                                    + new String(' ', Ispis.FormatTekst + Ispis.FormatCijeli - tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila().Length) 
                                    + item.GetBrojMjesta() + new String(' ', Ispis.FormatCijeli) + item.GetRaspolozivihMjesta() + new String(' ', Ispis.FormatCijeli)
                                    + brojNeispravnih
                                    + Environment.NewLine + new String(' ', (3 * Ispis.FormatTekst) + Ispis.FormatCijeli);
                            }
                            ispisLokacije2 += Environment.NewLine;
                            brojNeispravnih = 0;
                        }


                    }
                    if (ispisLokacije2 == "")
                    {
                        ispisLokacije2 = "Nema lokacija";
                       
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene2 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene2 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice2 =
                        new DecoratorBroj(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format2 = redakTablice2.KreirajRedak();
                    string output2 = String.Format(format2, ispisLokacije2, ispisNadredene2, this.NazivJedinice);
                    Console.WriteLine(output2);
                    Console.WriteLine(new String('-', 21*Ispis.FormatCijeli));
                }
                else if (izbor.Contains("struktura") && izbor.Contains("stanje") && izbor.Length == 18)
                {
                    TvrtkaSingleton tvrtkaSingleton = TvrtkaSingleton.GetTvrtkaInstance();
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        var dohvatVozila = tvrtkaSingleton.ListaLokacijaVozila.Where(x => x.GetIdLokacije()[0] == Lokacije[i].GetId()).ToList();

                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije3 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije3 += " ";
                                }
                            }

                        }

                        int brojNeispravnih = 0;

                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije3 += Lokacije[i].GetNazivLokacije();
                            ispisLokacije3 += new String(' ', Ispis.FormatTekst + 1 - Lokacije[i].GetNazivLokacije().Length);
                            foreach (var item in dohvatVozila)
                            {
                                if (tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti() == 4 && x.GetOpisProblema() != null) == true)
                                {
                                    var dohvat = tvrtkaSingleton.ListaAktivnosti.Find(x => x.GetIdAktivnosti().Equals(4) && x.GetOpisProblema() != null);
                                    int idKorisnika = dohvat.GetIdKorisnika()[0];
                                    int dohvatLokacija = dohvat.GetIdLokacije()[0];
                                    int dohvatVrsteVozila = dohvat.GetIdVrsteVozila()[0];
                                    var dohvatNeispravna = tvrtkaSingleton.ListaLokacijaVozila.Where(x => x.GetIdLokacije()[0] == Lokacije[i].GetId() && x.GetIdVrsteVozila()[0] == item.GetIdVrsteVozila()[0]).ToList();
                                    foreach (var item2 in dohvatNeispravna)
                                    {
                                        if (item.GetIdVrsteVozila()[0] == dohvatVrsteVozila && item.GetIdLokacije()[0] == dohvatLokacija)
                                        {
                                            brojNeispravnih++;
                                        }
                                        else
                                        {
                                            brojNeispravnih = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    brojNeispravnih = 0;
                                }
                                ispisLokacije3 += tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila()
                                    + new String(' ', Ispis.FormatTekst + Ispis.FormatCijeli - tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila().Length)
                                    + item.GetBrojMjesta() + new String(' ', Ispis.FormatCijeli) + item.GetRaspolozivihMjesta() + new String(' ', Ispis.FormatCijeli)
                                    + brojNeispravnih
                                    + Environment.NewLine + new String(' ', (3 * Ispis.FormatTekst) + Ispis.FormatCijeli);
                            }
                            brojNeispravnih = 0;
                        }
                        else
                        {
                            ispisLokacije3 += Lokacije[i].GetNazivLokacije();
                            ispisLokacije3 += new String(' ', Ispis.FormatTekst + 1 - Lokacije[i].GetNazivLokacije().Length);
                            foreach (var item in dohvatVozila)
                            {
                                if (tvrtkaSingleton.ListaAktivnosti.Exists(x => x.GetIdAktivnosti() == 4 && x.GetOpisProblema() != null) == true)
                                {
                                    var dohvat = tvrtkaSingleton.ListaAktivnosti.Find(x => x.GetIdAktivnosti().Equals(4) && x.GetOpisProblema() != null);
                                    int idKorisnika = dohvat.GetIdKorisnika()[0];
                                    int dohvatLokacija = dohvat.GetIdLokacije()[0];
                                    int dohvatVrsteVozila = dohvat.GetIdVrsteVozila()[0];
                                    var dohvatNeispravna = tvrtkaSingleton.ListaLokacijaVozila.Where(x => x.GetIdLokacije()[0] == Lokacije[i].GetId() && x.GetIdVrsteVozila()[0] == item.GetIdVrsteVozila()[0]).ToList();
                                    foreach (var item2 in dohvatNeispravna)
                                    {
                                        if (item.GetIdVrsteVozila()[0] == dohvatVrsteVozila && item.GetIdLokacije()[0] == dohvatLokacija)
                                        {
                                            brojNeispravnih++;
                                        }
                                        else
                                        {
                                            brojNeispravnih = 0;
                                        }

                                    }
                                }
                                else
                                {
                                    brojNeispravnih = 0;
                                }
                                ispisLokacije3 += tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila()
                                    + new String(' ', Ispis.FormatTekst + Ispis.FormatCijeli - tvrtkaSingleton.ListaVozila.Where(x => x.GetId() == item.GetIdVrsteVozila()[0]).FirstOrDefault().GetNazivVozila().Length)
                                    + item.GetBrojMjesta() + new String(' ', Ispis.FormatCijeli) + item.GetRaspolozivihMjesta() + new String(' ', Ispis.FormatCijeli)
                                    + brojNeispravnih
                                    + Environment.NewLine + new String(' ', (3 * Ispis.FormatTekst) + Ispis.FormatCijeli);
                            }
                            ispisLokacije3 += Environment.NewLine;
                            brojNeispravnih = 0;
                        }


                    }
                    if (ispisLokacije3 == "")
                    {
                        ispisLokacije3 = "Nema lokacija";

                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene3 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene3 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice3 =
                        new DecoratorBroj(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format3 = redakTablice3.KreirajRedak();
                    string output2 = String.Format(format3, ispisLokacije3, ispisNadredene3, this.NazivJedinice);
                    Console.WriteLine(output2);
                    Console.WriteLine(new String('-', 21 * Ispis.FormatCijeli));
                }



            }
            else if (idAktivnosti == 7)
            {
                string ispisLokacije4 = "";
                string ispisNadredene4 = "";
                string ispisLokacije5 = "";
                string ispisNadredene5 = "";
                if (izbor.Trim().Contains("struktura") && izbor.Trim().Length == 31)
                {

                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije4 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije4 += " ";
                                }
                            }
                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije4 += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije4 += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije4 == "")
                    {
                        ispisLokacije4 = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene4 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene4 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice4 =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format4 = redakTablice4.KreirajRedak();
                    string output4 = String.Format(format4, ispisLokacije4, ispisNadredene4, this.NazivJedinice);
                    Console.WriteLine(output4);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3 * 30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
                    }

                }
                else if (izbor.Trim().Contains("struktura") && izbor.Trim().Length == 33)
                {
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije5 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije5 += " ";
                                }
                            }

                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije5 += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije5 += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije5 == "")
                    {
                        ispisLokacije5 = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene5 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene5 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice5 =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format5 = redakTablice5.KreirajRedak();
                    string output5 = String.Format(format5, ispisLokacije5, ispisNadredene5, this.NazivJedinice);
                    Console.WriteLine(output5);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3 * 30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
                    }
                }
            }
            else if (idAktivnosti == 8)
            {
                string ispisLokacije6 = "";
                string ispisNadredene6 = "";
                string ispisLokacije7 = "";
                string ispisNadredene7 = "";
                if (izbor.Trim().Contains("struktura") && izbor.Trim().Length == 31)
                {

                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije7 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije7 += " ";
                                }
                            }
                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije7 += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije7 += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije7 == "")
                    {
                        ispisLokacije7 = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene7 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene7 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice7 =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format7 = redakTablice7.KreirajRedak();
                    string output7 = String.Format(format7, ispisLokacije7, ispisNadredene7, this.NazivJedinice);
                    Console.WriteLine(output7);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3 * 30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
                    }

                }
                else if (izbor.Trim().Contains("struktura") && izbor.Trim().Length == 33)
                {
                    for (int i = 0; i < Lokacije.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (Ispis.FormatTekst == 0)
                            {
                                for (int j = 0; j < (2 * 30) + 4; j++)
                                {
                                    ispisLokacije6 += " ";
                                }
                            }
                            else
                            {
                                for (int j = 0; j < (2 * Ispis.FormatTekst) + 4; j++)
                                {
                                    ispisLokacije6 += " ";
                                }
                            }

                        }
                        if (i == Lokacije.Count - 1)
                        {
                            ispisLokacije6 += Lokacije[i].GetNazivLokacije();
                        }
                        else
                        {
                            ispisLokacije6 += Lokacije[i].GetNazivLokacije() + Environment.NewLine;

                        }
                    }
                    if (ispisLokacije6 == "")
                    {
                        ispisLokacije6 = "Nema lokacija";
                    }

                    if (this.GetNadredena() == null)
                    {
                        ispisNadredene6 = "Nema nadređenog";
                    }
                    else
                    {
                        ispisNadredene6 = this.GetNadredena().NazivJedinice;
                    }

                    Ispis.Brojac = 0;
                    IDecoratorRedakTablice redakTablice6 =
                        new DecoratorText(
                            new DecoratorText(
                                new DecoratorText(
                                    new DecoratorKonkretniRedak())));
                    string format6 = redakTablice6.KreirajRedak();
                    string output6 = String.Format(format6, ispisLokacije6, ispisNadredene6, this.NazivJedinice);
                    Console.WriteLine(output6);
                    if (Ispis.FormatTekst == 0)
                    {
                        Console.WriteLine(new String('-', 3 * 30));
                    }
                    else
                    {
                        Console.WriteLine(new String('-', 3 * Ispis.FormatTekst) + new String('-', 5));
                    }
                }
            }
        }

        public void PrikaziPodatkePoLokaciji(int idLokacija)
        {
            
        }

        public void PrikaziPodatkeKoristiZaDjecu(string izbor, int idAktivnosti, string[] args)
        {
            PrikaziPodatke(izbor, idAktivnosti, args);
            for (IIteratorComposite iterator = GetIterator(); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                komponenta.PrikaziPodatkeKoristiZaDjecu(izbor, idAktivnosti, args);
            }
        }

        public IComponentTvrtka PronadiTvrtku(int idTvrtka)
        {
            for (IIteratorComposite iterator = GetIterator(); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                if (komponenta.GetIDTvrtke() == idTvrtka)
                    return komponenta;

                var pretragaDjece = komponenta.PronadiTvrtku(idTvrtka);
                if (pretragaDjece != null)
                    return pretragaDjece;
            }
            return null;
        }

        public IIteratorComposite GetIterator()
        {
            return new IteratorTvrtka(kreiranaListaDjece);
        }

        public List<IComponentTvrtka> GetListaDjece()
        {
            return kreiranaListaDjece;
        }
        public void DodajDijete(IComponentTvrtka komponenta)
        {
            kreiranaListaDjece.Add(komponenta);
        }


    }
}
