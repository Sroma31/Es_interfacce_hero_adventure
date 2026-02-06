using Es_3_interfacce.personaggi;
using Es_3_interfacce.personaggi.mostri;
using System;

namespace GiocoDiRuolo
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            Umano eroe = new Umano("Veneti il boss");

            Console.Write("C'è la luna piena stasera? (s/n): ");
            string rispostaLuna = Console.ReadLine();
            Licantropo.IsLunaPiena = (rispostaLuna.ToLower() == "s");

            Console.WriteLine($"\nL'avventura inizia nello scenario: {arena.AmbienteCorrente}");
            Console.WriteLine("Preparati a combattere...");
            System.Threading.Thread.Sleep(1500);// Pausa di 1.5 secondi

            int mostriSconfitti = 0;
            bool continuaACombattere = true;

            while (eroe.IsVivo && continuaACombattere)
            {
                Personaggio nemico = arena.GeneraMostroCasuale();
                arena.Scontro(eroe, nemico);

                if (eroe.IsVivo && eroe.Forza > 0) // Aggiunto check forza
                {
                    mostriSconfitti++;
                    Console.WriteLine($" Hai sconfitto {mostriSconfitti} mostri consecutivi! ");

                    Console.Write("Vuoi affrontare il prossimo mostro? (s/n): ");
                    string scelta = Console.ReadLine();
                    if (scelta.ToLower() != "s")
                    {
                        continuaACombattere = false;
                    }
                    else
                    {
                        arena.CambiaAmbienteCasuale();
                    }
                }
                else
                {
                    // Se esce dal loop perché morto o forza 0
                    continuaACombattere = false;
                }
            }

            Console.WriteLine("\n GAME OVER ");
            Console.WriteLine($"Eroe: {eroe.Nome}");
            Console.WriteLine($"Mostri Sconfitti: {mostriSconfitti}");
            Console.WriteLine("Premi invio per chiudere.");
            Console.ReadLine();
        }
    }
}