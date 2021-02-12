using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderLokacijeVozila
{
    class LokacijeVozilaBuildDirector
    {
        private ILokacijeVozilaBuilder builder;
        public LokacijeVozilaBuildDirector(ILokacijeVozilaBuilder builder)
        {
            this.builder = builder;
        }
        public LokacijeVozila Construct(
            List<int> lista, List<int> lista2, int brojMjesta, int raspolozivihMjesta)
        {
            return builder.SetIdLokacije(lista)
                .SetIdVrsteVozila(lista2)
                .SetBrojMjesta(brojMjesta)
                .SetRaspoloziviBrojMjesta(raspolozivihMjesta)
                .Build();
        }
    }
}
