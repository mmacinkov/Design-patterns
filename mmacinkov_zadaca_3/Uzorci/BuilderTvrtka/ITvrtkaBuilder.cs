using mmacinkov_zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Uzorci.BuilderTvrtka
{
    interface ITvrtkaBuilder
    {
        Tvrtka Build();
        ITvrtkaBuilder SetIdTvrtke(int idTvrtke);
        ITvrtkaBuilder SetNaziv(string naziv);
        ITvrtkaBuilder SetNadredena(Tvrtka nadredena);
        ITvrtkaBuilder SetLokacije(List<Lokacija> lista2);
    }
}
