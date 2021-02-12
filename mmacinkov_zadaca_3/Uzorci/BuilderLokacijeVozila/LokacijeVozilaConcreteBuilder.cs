using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderLokacijeVozila
{
    class LokacijeVozilaConcreteBuilder : ILokacijeVozilaBuilder
    {
        private LokacijeVozila lokacijeVozila;
        public LokacijeVozilaConcreteBuilder()
        {
            lokacijeVozila = new LokacijeVozila();
        }
        public LokacijeVozila Build()
        {
            return lokacijeVozila;
        }
        public ILokacijeVozilaBuilder SetIdLokacije(List<int> lista)
        {
            lokacijeVozila.SetIdLokacije(lista);
            return this;
        }
        public ILokacijeVozilaBuilder SetIdVrsteVozila(List<int> lista2)
        {
            lokacijeVozila.SetIdVrsteVozila(lista2);
            return this;
        }
        public ILokacijeVozilaBuilder SetBrojMjesta(int brojMjesta)
        {
            lokacijeVozila.SetBrojMjesta(brojMjesta);
            return this;
        }
        public ILokacijeVozilaBuilder SetRaspoloziviBrojMjesta(int raspolozivihMjesta)
        {
            lokacijeVozila.SetRaspolozivihMjesta(raspolozivihMjesta);
            return this;
        }
    }
}
