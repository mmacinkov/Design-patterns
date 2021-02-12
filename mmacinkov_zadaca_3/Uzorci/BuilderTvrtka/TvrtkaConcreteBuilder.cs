using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderTvrtka
{
    class TvrtkaConcreteBuilder : ITvrtkaBuilder
    {
        private Tvrtka tvrtka;

        public TvrtkaConcreteBuilder()
        {
            tvrtka = new Tvrtka();
        }

        public Tvrtka Build()
        {
            return tvrtka;
        }
        public ITvrtkaBuilder SetIdTvrtke(int idTvrtke)
        {
            tvrtka.SetIDTvrtke(idTvrtke);
            return this;
        }
        public ITvrtkaBuilder SetNaziv(string naziv)
        {
            tvrtka.SetNaziv(naziv);
            return this;
        }
        public ITvrtkaBuilder SetNadredena(Tvrtka nadredena)
        {
            tvrtka.SetNadredena(nadredena);
            return this;
        }
        public ITvrtkaBuilder SetLokacije(List<Lokacija> lista2)
        {
            tvrtka.SetLokacije(lista2);
            return this;
        }
    }
}
