using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.State
{
    public class StateSlobodno : IStateOsnovno
    {
        public void PromjeniStanje(Vozilo vozilo)
        {
            vozilo.Stanje = new StateUnajmljeno();
            vozilo.Stanje = new StateUnajmljeno2();
            Console.WriteLine("\nVozilo promijenjeno sa stanja slobodno na stanje unajmljeno!");
        }
    }
}
