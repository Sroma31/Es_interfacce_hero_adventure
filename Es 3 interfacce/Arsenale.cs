using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_3_interfacce.personaggi
{
    public class Arsenale
    {
        List<Arma> armi = new List<Arma>();

        public Arsenale()
        {
            armi.Add(new Arma("Pistola", 10, 100));
            armi.Add(new Arma("Fucile", 15, 120));
            armi.Add(new Arma("Coltello", 5, 80));    
            armi.Add(new Arma("Balestra", 12, 90));
            armi.Add(new Arma("Ascia", 8, 110));
        }

        public void AggiungiArma(Arma arma)
        {
            armi.Add(arma);
        }

        public Arma GetArma(int pos)
        {
            return armi[pos];
        }

        public int GetArmaCount()
        {
            return armi.Count;
        }
    }
}
