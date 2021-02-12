using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmacinkov_zadaca_3.Klase
{
    public class Cjenik
    {
        private List<int> IdVrsteVozila;
        private double Najam;
        private double PoSatu;
        private double PoKm;

        public void SetIdVrsteVozila(List<int> lista)
        {
            IdVrsteVozila = lista;
        }

        public List<int> GetIdVrsteVozila()
        {
            return IdVrsteVozila;
        }

        public void SetNajam(double najam)
        {
            Najam = najam;
        }

        public double GetNajam()
        {
            return Najam;
        }

        public void SetPoSatu(double poSatu)
        {
            PoSatu = poSatu;
        }

        public double GetPoSatu()
        {
            return PoSatu;
        }

        public void SetPoKm(double poKm)
        {
            PoKm = poKm;
        }

        public double GetPoKm()
        {
            return PoKm;
        }
    }
}
