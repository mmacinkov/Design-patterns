using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;

namespace mmacinkov_zadaca_3.Uzorci.BuilderCjenik
{
    class CjenikConcreteBuilder : ICjenikBuilder
    {
        private Cjenik cjenik;

        public CjenikConcreteBuilder()
        {
            cjenik = new Cjenik();
        }

        public Cjenik Build()
        {
            return cjenik;
        }

        public ICjenikBuilder SetIdVrsteVozila(List<int> lista)
        {
            cjenik.SetIdVrsteVozila(lista);
            return this;
        }

        public ICjenikBuilder SetNajam(double najam)
        {
            cjenik.SetNajam(najam);
            return this;
        }

        public ICjenikBuilder SetPoSatu(double poSatu)
        {
            cjenik.SetPoSatu(poSatu);
            return this;
        }

        public ICjenikBuilder SetPoKm(double poKm)
        {
            cjenik.SetPoKm(poKm);
            return this;
        }
    }
}
