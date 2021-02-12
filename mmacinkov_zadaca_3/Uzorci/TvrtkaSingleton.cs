using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.Composite;

namespace mmacinkov_zadaca_3
{
    public class TvrtkaSingleton
    {
        private static volatile TvrtkaSingleton tvrtka = new TvrtkaSingleton();

        public CompositeTvrtka compositeTvrtka;

        private TvrtkaSingleton() { }

        public static TvrtkaSingleton GetTvrtkaInstance()
        {
            return tvrtka;
        }

        public List<Vozilo> ListaVozila;
        public List<Osoba> ListaOsoba;
        public List<Lokacija> ListaLokacija;
        public List<Cjenik> ListaCjenika;
        public List<LokacijeVozila> ListaLokacijaVozila;
        public List<Aktivnost> ListaAktivnosti;
        public List<Tvrtka> ListaTvrtki;
        private string MyFolderLocation;
        public void SetMyFolderLocation(string location)
        {
            MyFolderLocation = location;
        }
        public string GetMyFolderLocation()
        {
            return MyFolderLocation;
        }

        public void SetCompositeTvrtka()
        {
            if (compositeTvrtka == null)
            {
                compositeTvrtka = new CompositeTvrtka();
                compositeTvrtka.SetIDTvrtke(-1);
            }
        }

        public CompositeTvrtka GetCompositeTvrtka()
        {
            return compositeTvrtka;
        }

        public Lokacija PronadiLokaciju(int idLokacije)
        {
            foreach (var item in ListaLokacija)
            {
                if (item.GetId() == idLokacije)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
