using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using mmacinkov_zadaca_3.Helperi;
using mmacinkov_zadaca_3.Klase;
using mmacinkov_zadaca_3.UcitavanjeDatoteka;

namespace mmacinkov_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Datoteka.ProvjeraUlaznihParametara(args);
            Ispis.UcitajDatoteke(args);
            Datoteka.ProvjeraDaSuSveLokacijeUkljucene();
            
            if (args.Length == 1)
            {
                
            }
            else
            {
                Console.WriteLine("Unjeli ste pogrešan broj elemenata!");
            }
            
        }
        
    }
}
