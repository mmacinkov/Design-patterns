using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class LokacijeVozila
    {
        private List<int> IdLokacije;
        private List<int> IdVrsteVozila;
        private int BrojMjesta;
        private int RaspoloziviBrojMjesta;

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

        public void SetBrojMjesta(int brojMjesta)
        {
            BrojMjesta = brojMjesta;
        }

        public int GetBrojMjesta()
        {
            return BrojMjesta;
        }

        public void SetRaspolozivihMjesta(int raspolozivihMjesta)
        {
            RaspoloziviBrojMjesta = raspolozivihMjesta;
        }

        public int GetRaspolozivihMjesta()
        {
            return RaspoloziviBrojMjesta;
        }
    }
}
