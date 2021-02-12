using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Osoba
    {
        private int Id;
        private string ImePrezime;
        private int Ugovor;

        public void SetId(int id)
        {
            Id = id;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetImePrezime(string imePrezime)
        {
            ImePrezime = imePrezime;
        }

        public string GetImePrezime()
        {
            return ImePrezime;
        }

        private string MyFolderLocation;

        public void SetMyFolderLocation(string location)
        {
            MyFolderLocation = location;
        }
        public string GetMyFolderLocation()
        {
            return MyFolderLocation;
        }

        public void SetUgovor(int ugovor)
        {
            Ugovor = ugovor;
        }

        public int GetUgovor()
        {
            return Ugovor;
        }


        private int BrojVracanjaNeispravnihVozila;

        public void SetBrojVracanjaNeispravnihVozila(int broj)
        {
            BrojVracanjaNeispravnihVozila = broj;
        }

        public int GetBrojVracanjaNeispravnihVozila()
        {
            return BrojVracanjaNeispravnihVozila;
        }
    }
}
