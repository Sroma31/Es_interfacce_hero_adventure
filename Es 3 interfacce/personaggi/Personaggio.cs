using System;
using System.Collections.Generic;

namespace Es_3_interfacce.personaggi
{
    public abstract class Personaggio : IPersonaggio
    {
        protected static Random dadi = new Random();

        public Arsenale abcv = new Arsenale();
        protected Arma arma;

        public string Nome { get; protected set; }
        public int Forza { get; protected set; }
        public int Vita { get; protected set; }
        public int VitaMax { get; protected set; }

        public bool IsVivo => Vita > 0;

        public Personaggio(string nome, int forzaIniziale, int dadiVita)
        {
            Nome = nome;
            Forza = forzaIniziale;
            // 1dX significa un numero tra 1 e X
            VitaMax = dadi.Next(1, dadiVita + 1);
            Vita = VitaMax;
        }

        public abstract void Attacca(IPersonaggio bersaglio);

        public virtual void SubisciDanno(int danno)
        {
            Vita -= danno;
            if (Vita < 0) Vita = 0;
            if (arma.durability <= 0)
            {
                Console.WriteLine($"{Nome} ha distrutto {arma.Name}!");
                arma = null; // L'arma è distrutta
            }
            else
            {
                arma.durability -= 1; // Ogni attacco riduce la durabilità dell'arma

            }
            Console.WriteLine($"{Nome} subisce {danno} danni (Vita: {Vita}/{VitaMax})");
        }

        public void StampaStato()
        {
            Console.WriteLine($"[{Nome}] HP: {Vita}/{VitaMax} | STR: {Forza}");
        }



        protected int TiraDadi(int facce)
        {
            if (facce <= 0)// Controllo per evitare valori non validi
            {
                throw new ArgumentOutOfRangeException(nameof(facce), facce, "Il numero di facce deve essere maggiore di zero.");
            }

            // Genera un valore casuale tra 1 e 'facce' inclusi
            int risultato = dadi.Next(1, facce + 1);
            return risultato;
        }


        public void EquipaggiaArma()
        {
            Random rnd = new Random();
            arma = abcv.GetArma(rnd.Next(0, abcv.GetArmaCount()));
            Console.WriteLine($"{Nome} ha equipaggiato {arma.Name} (Danno: {arma.damage})");

        }
    }
}