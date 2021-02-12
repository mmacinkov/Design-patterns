using mmacinkov_zadaca_3.Uzorci.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Iterator
{
    public class IteratorLokacija : IIteratorComposite
    {
        private List<IComponentTvrtka> ListaDjece;
        private int Index;
        private int IdLokacija;

        public IteratorLokacija(List<IComponentTvrtka> listaDjece, int idLokacija)
        {
            ListaDjece = listaDjece;
            IdLokacija = idLokacija;
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
