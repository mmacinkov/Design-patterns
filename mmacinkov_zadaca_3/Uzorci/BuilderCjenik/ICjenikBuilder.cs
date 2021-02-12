using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Klase;

namespace mmacinkov_zadaca_3.Uzorci.BuilderCjenik
{
    interface ICjenikBuilder
    {
        Cjenik Build();
        ICjenikBuilder SetIdVrsteVozila(List<int> lista);
        ICjenikBuilder SetNajam(double najam);
        ICjenikBuilder SetPoSatu(double poSatu);
        ICjenikBuilder SetPoKm(double poKm);
    }
}
