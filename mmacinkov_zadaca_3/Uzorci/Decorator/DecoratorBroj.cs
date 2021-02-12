using mmacinkov_zadaca_3.Helperi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Decorator
{
    class DecoratorBroj : DecoratorRedakTablice
    {
        private string Podaci;
        public int BrojZaFormatiranje;


        public DecoratorBroj(IDecoratorRedakTablice redakTablice) : base(redakTablice)
        {

        }

        public override string KreirajRedak()
        {
            NapuniPodacima();
            return base.KreirajRedak() + Podaci;
        }

        public void NapuniPodacima()
        {
            if (Ispis.FormatCijeli > 0)
            {
                string podaci = " {" + Ispis.Brojac + ",-" + Ispis.FormatCijeli + "}";
                Ispis.Brojac++;
                Podaci = podaci;
            }
            else
            {
                string podaci = " {" + Ispis.Brojac + ",-5}";
                Ispis.Brojac++;
                Podaci = podaci;
            }

        }
    }
}
