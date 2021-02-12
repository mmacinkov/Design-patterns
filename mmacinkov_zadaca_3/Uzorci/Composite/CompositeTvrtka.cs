using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.Uzorci.Iterator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Composite
{
    public class CompositeTvrtka : IComponentTvrtka, IIteratorTvrtka, IIteratorLokacija
    {
        private List<IComponentTvrtka> ListaDjece = new List<IComponentTvrtka>();
        private string PrivatniNaziv = "";
        private int PrivatniId;

        public IIteratorComposite GetIterator()
        {
            return new IteratorTvrtka(ListaDjece);
        }

        public IIteratorComposite GetIterator(int idLokacija)
        {
            return new IteratorTvrtka(ListaDjece);
        }

        public void PrikaziPodatke(string izbor, int idAktivnosti, string[] args)
        {
            for (IIteratorComposite iterator = GetIterator(); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                komponenta.PrikaziPodatke(izbor, idAktivnosti, args);
            }
        }

        public void PrikaziPodatkeKoristiZaDjecu(string izbor, int idAktivnosti, string[] args)
        {
            for (IIteratorComposite iterator = GetIterator(); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                komponenta.PrikaziPodatkeKoristiZaDjecu(izbor, idAktivnosti, args);
            }
        }

        public void PrikaziPodatkePoLokaciji(int idLokacija)
        {
            for (IIteratorComposite iterator = GetIterator(idLokacija); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                komponenta.PrikaziPodatkePoLokaciji(idLokacija);
            }
        }

        public void DodajDijete(IComponentTvrtka dijete)
        {
            ListaDjece.Add(dijete);
        }
        public List<IComponentTvrtka> GetListaDjece()
        {
            return ListaDjece;
        }
        

        public List<Lokacija> GetLokacije()
        {
            return null;
        }

        public IComponentTvrtka PronadiTvrtku(int idTvrtke)
        {
            for (IIteratorComposite iterator = GetIterator(); iterator.PostojiSljedeci();)
            {
                IComponentTvrtka komponenta = (IComponentTvrtka)iterator.Sljedeci();
                if (komponenta.GetIDTvrtke() == idTvrtke)
                {
                    return komponenta;
                }
                var pretraga = komponenta.PronadiTvrtku(idTvrtke);
                if (pretraga != null)
                {
                    return pretraga;
                }
                
            }
            return null;
        }
        public void SetNaziv(string naziv)
        {
            PrivatniNaziv = naziv;
        }

        public string GetNaziv()
        {
            return PrivatniNaziv;
        }
        public void SetIDTvrtke(int id)
        {
            PrivatniId = id;
        }

        public int GetIDTvrtke()
        {
            return PrivatniId;
        }
    }
}
