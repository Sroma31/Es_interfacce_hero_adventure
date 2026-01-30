using System;

namespace GiocoDiRuolo
{
    public class Umano : Personaggio
    {
        public Umano(string nome) : base(nome, 10, 12) { }

        public override void Attacca(IPersonaggio bersaglio)
        {
            if (!IsVivo) return;

            Console.WriteLine($"{Nome} attacca!");

            int dannoBase = TiraDadi(4);

            // CORREZIONE: Se Forza è 1, 1/2 farebbe 0. 
            // Forziamo il moltiplicatore a 1 se c'è ancora un minimo di forza.
            int moltiplicatore = Forza / 2;
            if (moltiplicatore == 0 && Forza > 0) moltiplicatore = 1;

            int dannoTotale = dannoBase * moltiplicatore;

            bersaglio.SubisciDanno(dannoTotale);

            // Consumo forza
            Forza -= 3;
            if (Forza < 0) Forza = 0;
        }
    }
}