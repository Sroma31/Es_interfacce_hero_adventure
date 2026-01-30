using System;

namespace GiocoDiRuolo
{
    public class Licantropo : Personaggio
    {
        public static bool IsLunaPiena { get; set; } = false;

        public Licantropo(string nome) : base(nome, 12, 10) { } // Statistiche ipotizzate

        public override void Attacca(IPersonaggio bersaglio)
        {
            if (!IsVivo) return;

            Console.WriteLine($"{Nome}  attacca!");

            // Danno ipotetico basato su standard mostro (es. 1d6 danno base)
            int dannoBase = TiraDadi(6);
            int dannoTotale = dannoBase * (Forza / 2);
            bersaglio.SubisciDanno(dannoTotale);

            // Gestione Forza FASE 2:
            // 2 se luna piena, 3 nelle altre
            int malusForza = IsLunaPiena ? 2 : 3;
            Forza -= malusForza;
            if (Forza < 0) Forza = 0;

            Console.WriteLine($"(Luna Piena: {IsLunaPiena}  Perdita Forza: {malusForza})");
        }
    }
}