using System;

namespace GiocoDiRuolo
{
    public class Arena
    {
        private Random _rnd = new Random();

        public TipoAmbiente AmbienteCorrente { get; private set; }

        public Arena()
        {
            Array valori = Enum.GetValues(typeof(TipoAmbiente));
            AmbienteCorrente = (TipoAmbiente)valori.GetValue(_rnd.Next(valori.Length));
        }

        public void Scontro(IPersonaggio eroe, IPersonaggio mostro)
        {
            Console.Clear();
            Console.WriteLine($"--- SCENARIO: {AmbienteCorrente} ---");
            Console.WriteLine($" INIZIO SCONTRO NELL'ARENA ");
            Console.WriteLine($"{eroe.Nome} (HP:{eroe.Vita} | STR:{eroe.Forza}) VS {mostro.Nome}");
            Console.WriteLine("\n");

            while (eroe.IsVivo && mostro.IsVivo)
            {
                // CORREZIONE LOOP: Controllo esaurimento forze
                if (eroe.Forza == 0)
                {
                    Console.WriteLine(">>> L'Eroe è esausto (Forza 0) e crolla a terra!");
                    break; // Interrompe il loop forzatamente
                }

                // Turno Eroe
                eroe.Attacca(mostro);
                if (!mostro.IsVivo) break;

                Console.WriteLine();

                // Turno Mostro
                mostro.Attacca(eroe);

                Console.WriteLine("\n Fine Round ");
                eroe.StampaStato();
                mostro.StampaStato();
                Console.WriteLine("\n");

                System.Threading.Thread.Sleep(800);
            }

            DecretaVincitore(eroe, mostro);
        }

        public IPersonaggio GeneraMostroCasuale()
        {
            int roll = _rnd.Next(1, 101);

            switch (AmbienteCorrente)
            {
                case TipoAmbiente.CastelloOscuro:
                    if (roll <= 50) return new Vampiro("Conte del Castello");
                    if (roll <= 70) return new Licantropo("Guardia Mannara");
                    return new Goblin("Servo Goblin");

                case TipoAmbiente.ForestaMaledetta:
                    if (roll <= 10) return new Vampiro("Vampiro Errante");
                    if (roll <= 50) return new Licantropo("Lupo della Foresta");
                    return new Goblin("Goblin dei Boschi");

                case TipoAmbiente.CavernaGoblin:
                default:
                    if (roll <= 5) return new Vampiro("Vampiro Perduto");
                    if (roll <= 20) return new Licantropo("Licantropo di Caverna");
                    return new Goblin("Goblin Minatore");
            }
        }

        private void DecretaVincitore(IPersonaggio p1, IPersonaggio p2)
        {
            Console.WriteLine("\n");
            // Se l'eroe è vivo ma ha forza 0, tecnicamente ha perso per ritiro/svenimento
            if (p1.IsVivo && p1.Forza > 0)
                Console.WriteLine($"VITTORIA! {p1.Nome} ha vinto.");
            else if (p1.Forza == 0)
                Console.WriteLine($"SCONFITTA TECNICA... {p1.Nome} è svenuto per la fatica.");
            else
                Console.WriteLine($"SCONFITTA... {p2.Nome} ha vinto.");
            Console.WriteLine("");
        }

        public void CambiaAmbienteCasuale()
        {
            Array valori = Enum.GetValues(typeof(TipoAmbiente));
            AmbienteCorrente = (TipoAmbiente)valori.GetValue(_rnd.Next(valori.Length));
            Console.WriteLine($"\n*** L'ambiente cambia! Ora sei in: {AmbienteCorrente} ***\n");
        }
    }
}