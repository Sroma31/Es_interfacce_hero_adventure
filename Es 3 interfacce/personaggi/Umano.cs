using System;

namespace Es_3_interfacce.personaggi
{
    public class Umano : Personaggio
    {
        public Umano(string nome) : base(nome, 10, 50) { }

        public override void Attacca(IPersonaggio bersaglio)
        {
            if (!IsVivo) return;

            Console.WriteLine($"{Nome} attacca!");

            int dannoBase = TiraDadi(4);

            
            // Forziamo il moltiplicatore a 1 se c'è ancora un minimo di forza.
            int moltiplicatore = Forza / 2;
            if (moltiplicatore == 0 && Forza > 0) moltiplicatore = 1;

            int dannoTotale = dannoBase * moltiplicatore+ base.arma.damage;

            bersaglio.SubisciDanno(dannoTotale);

            // Consumo forza
            Forza -= 3;
            if (Forza < 0) Forza = 0;
        }
    }
}