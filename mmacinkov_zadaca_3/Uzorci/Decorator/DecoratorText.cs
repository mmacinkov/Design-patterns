using mmacinkov_zadaca_3.Helperi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Decorator
{
    public class DecoratorText : DecoratorRedakTablice
    {
        private string Podaci;
        public int BrojZaFormatiranje;
        
        
        public DecoratorText(IDecoratorRedakTablice redakTablice) : base(redakTablice)
        {

        }

        public override string KreirajRedak()
        {
            NapuniPodacima();
            return base.KreirajRedak() + Podaci;
        }

        public void NapuniPodacima()
        {
            if (Ispis.FormatTekst > 0)
            {
                string podaci = " {" + Ispis.Brojac + ",-" + Ispis.FormatTekst + "}";
                Ispis.Brojac++;
                Podaci = podaci;
            }
            else
            {
                string podaci = " {" + Ispis.Brojac + ",-30}";
                Ispis.Brojac++;
                Podaci = podaci;
            }
            
        }
    }
}
