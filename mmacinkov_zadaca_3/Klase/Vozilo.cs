using mmacinkov_zadaca_3.Uzorci.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Vozilo
    {
        private int Id;
        private string NazivVozila;
        private double VrijemePunjenjaBaterije;
        private int Domet;
        public IStateOsnovno Stanje { get; set; }

        //private int JednoznacniID = 0;
        private int BrojNajma = 0;

        public Vozilo() { }

        public Vozilo(IStateOsnovno stanje)
        {
            Stanje = stanje;
        }

        public void ZahtjevZaPromjenuStanja()
        {
            Stanje.PromjeniStanje(this);
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetNazivVozila(string nazivVozila)
        {
            NazivVozila = nazivVozila;
        }

        public string GetNazivVozila()
        {
            return NazivVozila;
        }

        public void SetVrijemePunjenjaBaterije(double vrijemePunjenja)
        {
            VrijemePunjenjaBaterije = vrijemePunjenja;
        }

        public double GetVrijemePunjenjaBaterije()
        {
            return VrijemePunjenjaBaterije;
        }

        public void SetDomet(int domet)
        {
            Domet = domet;
        }

        public int GetDomet()
        {
            return Domet;
        }

        /*public void SetJednoznacniID(int jedId)
        {
            JednoznacniID = jedId;
        }

        public int GetJednoznacniID()
        {
            return JednoznacniID;
        }
        */

        public void SetBrojNajma(int brNajma)
        {
            BrojNajma = brNajma;
        }

        public int GetBrojNajma()
        {
            return BrojNajma;
        }

        public double Potrosnja;

        public double PotrosnjaBaterije(int brojKm)
        {
            Potrosnja = (double.Parse(brojKm.ToString()) / double.Parse(Domet.ToString())) * 100;
            return Potrosnja;
        }

        public double VrijemePunjenja()
        {
            VrijemePunjenjaBaterije = VrijemePunjenjaBaterije * Potrosnja / 100;
            return VrijemePunjenjaBaterije;
        }

        public TimeSpan DohvatVrijeme()
        {
            TimeSpan date = TimeSpan.FromHours(VrijemePunjenjaBaterije);
            return date;
        }

    }
}
