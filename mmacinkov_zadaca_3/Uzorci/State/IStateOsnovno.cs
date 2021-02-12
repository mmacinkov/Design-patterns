using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.State
{
    public interface IStateOsnovno
    {
        void PromjeniStanje(Vozilo vozilo);
    }
}
