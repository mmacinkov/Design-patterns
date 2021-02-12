using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderTvrtka
{
    class TvrtkaBuildDirector
    {
        private ITvrtkaBuilder builder;

        public TvrtkaBuildDirector(ITvrtkaBuilder builder)
        {
            this.builder = builder;
        }

        public Tvrtka Construct(
            int idTvrtke, string naziv, Tvrtka nadredena, List<Lokacija> lista2)
        {
            return builder.SetIdTvrtke(idTvrtke)
                .SetNaziv(naziv)
                .SetNadredena(nadredena)
                .SetLokacije(lista2)
                .Build();
        }
    }
}
