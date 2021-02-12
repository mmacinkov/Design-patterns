using mmacinkov_zadaca_3.Uzorci.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Iterator
{
    public class IteratorTvrtka : IIteratorComposite
    {
        private List<IComponentTvrtka> ListaDjece = new List<IComponentTvrtka>();
        private int Index;
        public IteratorTvrtka(List<IComponentTvrtka> listaDjece)
        {
            ListaDjece = listaDjece;
        }

        public bool PostojiSljedeci()
        {
            if (Index < ListaDjece.Count)
            {
                return true;
            }
            return false;
        }

        public Object Sljedeci()
        {
            if (this.PostojiSljedeci())
            {
                return ListaDjece[Index++];
            }
            return null;
        }
    }
}
