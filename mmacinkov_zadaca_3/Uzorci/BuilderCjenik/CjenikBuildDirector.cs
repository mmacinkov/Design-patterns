using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;

namespace mmacinkov_zadaca_3.Uzorci.BuilderCjenik
{
    class CjenikBuildDirector
    {
        private ICjenikBuilder builder;

        public CjenikBuildDirector(ICjenikBuilder builder)
        {
            this.builder = builder;
        }

        public Cjenik Construct(
            List<int> lista, double najam, double poSatu, double poKm)
        {
            return builder.SetIdVrsteVozila(lista)
                .SetNajam(najam)
                .SetPoSatu(poSatu)
                .SetPoKm(poKm)
                .Build();
        }
    }
}
