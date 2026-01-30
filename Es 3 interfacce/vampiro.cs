using System;

namespace GiocoDiRuolo
{
    public class Vampiro : Mostro
    {
        public Vampiro(string nome) : base(nome, 15, 6) { } // Forza 15, Vita 1d6

        protected override void EseguiAzzanno(IPersonaggio bersaglio)
        {
            // Attacco 1d20, Danno 1d10 * forza/2
            int dannoBase = TiraDadi(10);
            int dannoTotale = dannoBase * (Forza / 2);

            Console.WriteLine($"{Nome} azzanna!");
            bersaglio.SubisciDanno(dannoTotale);
        }

        // Logica speciale di morte
        public override void SubisciDanno(int danno)
        {
            base.SubisciDanno(danno);

            if (Vita <= 0)
            {
                Console.WriteLine($"{Nome} è a terra");

                // 20% probabilità palo, 40% accendino
                bool haPalo = dadi.NextDouble() < 0.20;
                bool haAccendino = dadi.NextDouble() < 0.40;

                if (haPalo)
                {
                    Console.WriteLine("Trovato palo di frassino! Il Vampiro è distrutto.");
                }
                else if (haAccendino)
                {
                    Console.WriteLine("Accendino funzionante! Il Vampiro è bruciato.");
                }
                else
                {
                    Console.WriteLine("Nessun oggetto sacro trovato... VAMPIRO SI RIGENERA!");
                    Vita = VitaMax; // Torna in vita
                }
            }
        }
    }
}