using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Decorator
{
    public class DecoratorRedakTablice : IDecoratorRedakTablice
    {
        public IDecoratorRedakTablice Redak;
        public int Index = 0;
        public DecoratorRedakTablice(IDecoratorRedakTablice redak)
        {
            Redak = redak;
        }

        public virtual string KreirajRedak()
        {
            return Redak.KreirajRedak();
        }
    }
}
