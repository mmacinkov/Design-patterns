using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderLokacijeVozila
{
    interface ILokacijeVozilaBuilder
    {
        LokacijeVozila Build();

        ILokacijeVozilaBuilder SetIdLokacije(List<int> lista);
        ILokacijeVozilaBuilder SetIdVrsteVozila(List<int> lista2);
        ILokacijeVozilaBuilder SetBrojMjesta(int brojMjesta);
        ILokacijeVozilaBuilder SetRaspoloziviBrojMjesta(int raspolozivihMjesta);
    }
}
