using System;

namespace GiocoDiRuolo
{
    public abstract class Personaggio : IPersonaggio
    {
        protected static Random dadi = new Random();

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
            Console.WriteLine($"{Nome} subisce {danno} danni (Vita: {Vita}/{VitaMax})");
        }

        public void StampaStato()
        {
            Console.WriteLine($"[{Nome}] HP: {Vita}/{VitaMax} | STR: {Forza}");
        }

        protected int TiraDadi(int facce) => dadi.Next(1, facce + 1);
    }
}