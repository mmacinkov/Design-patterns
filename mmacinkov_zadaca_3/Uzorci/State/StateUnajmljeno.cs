using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.State
{
    public class StateUnajmljeno : IStateOsnovno
    {
        public void PromjeniStanje(Vozilo vozilo)
        {
             vozilo.Stanje = new StateNaPunjenu();
             Console.WriteLine("\nVozilo promjenjeno sa stanja unajmljeno na stanje na punjenju!");
            
        }
    }
}
