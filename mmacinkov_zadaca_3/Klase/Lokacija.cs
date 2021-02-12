using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Lokacija
    {
        private int Id;
        private string NazivLokacije;
        private string AdresaLokacije;
        private string Gps;

        public Lokacija() { }

        public void SetId(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetNazivLokacije(string nazivLokacije)
        {
            NazivLokacije = nazivLokacije;
        }

        public string GetNazivLokacije()
        {
            return NazivLokacije;
        }

        public void SetAdresaLokacije(string adresaLokacije)
        {
            AdresaLokacije = adresaLokacije;
        }

        public string GetAdresaLokacije()
        {
            return AdresaLokacije;
        }

        public void SetGps(string gps)
        {
            Gps = gps;
        }

        public string GetGps()
        {
            return Gps;
        }

    }

    
}


