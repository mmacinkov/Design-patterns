using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.Composite
{
    public interface IComponentTvrtka
    {
        void PrikaziPodatke(string izbor, int idAktivnosti, string[] args);
        void PrikaziPodatkePoLokaciji(int idLokacija);
        void PrikaziPodatkeKoristiZaDjecu(string izbor, int idAktivnosti, string[] args);
        void DodajDijete(IComponentTvrtka dijete);
        List<IComponentTvrtka> GetListaDjece();
        List<Lokacija> GetLokacije();
        IComponentTvrtka PronadiTvrtku(int idTvrtke);
        string GetNaziv();
        int GetIDTvrtke();
        
        
    }
}
