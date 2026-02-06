using System;

namespace Es_3_interfacce.personaggi.mostri
{
    public class Goblin : Mostro
    {
        public Goblin(string nome) : base(nome, 4, 6) { }

        protected override void EseguiAzzanno(IPersonaggio bersaglio)
        {
            int dannoBase = TiraDadi(3);

            // CORREZIONE MATEMATICA
            int moltiplicatore = Forza / 2;
            if (moltiplicatore == 0 && Forza > 0) moltiplicatore = 1;

            int dannoTotale = dannoBase * moltiplicatore + arma.damage;

            Console.WriteLine($"{Nome} morde!");
            bersaglio.SubisciDanno(dannoTotale);
        }
    }
}