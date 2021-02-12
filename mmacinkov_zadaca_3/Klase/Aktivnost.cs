using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Aktivnost
    {
        private int IdAktivnosti;
        private string Datum;
        private List<int> IdKorisnika;
        private List<int> IdLokacije;
        private List<int> IdVrsteVozila;
        private int BrojKm;
        private string OpisProblema;
        private string OpisAktivnosti;

        public void SetIdAktivnosti(int id)
        {
            IdAktivnosti = id;
        }

        public int GetIdAktivnosti()
        {
            return IdAktivnosti;
        }

        public void SetOpisAktivnosti(string opis)
        {
            OpisAktivnosti = opis;
        }

        public string GetOpisAktivnosti()
        {
            return OpisAktivnosti;
        }

        public void SetDatum(string datum)
        {
            Datum = datum;
        }

        public string GetDatum()
        {
            return Datum;
        }

        public void SetIdKorisnika(List<int> lista)
        {
            IdKorisnika = lista;
        }

        public List<int> GetIdKorisnika()
        {
            return IdKorisnika;
        }

        public void SetIdLokacije(List<int> lista)
        {
            IdLokacije = lista;
        }

        public List<int> GetIdLokacije()
        {
            return IdLokacije;
        }

        public void SetIdVrsteVozila(List<int> lista)
        {
            IdVrsteVozila = lista;
        }

        public List<int> GetIdVrsteVozila()
        {
            return IdVrsteVozila;
        }

        public void SetBrojKm(int brojKm)
        {
            BrojKm = brojKm;
        }

        public int GetBrojKm()
        {
            return BrojKm;
        }

        public void SetOpisProblema(string opisProblema)
        {
            OpisProblema = opisProblema;
        }

        public string GetOpisProblema()
        {
            return OpisProblema;
        }
    }
}
