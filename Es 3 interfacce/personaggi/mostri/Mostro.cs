using System;

namespace Es_3_interfacce.personaggi.mostri
{
    // Classe intermedia per raggruppare i mostri che si comportano allo stesso modo
    public abstract class Mostro : Personaggio
    {
        public Mostro(string nome, int forzaIniziale, int dadiVita)
            : base(nome, forzaIniziale, dadiVita) { }

        // Override sigillato nella logica di consumo, ma aperto nel calcolo danno
        public override void Attacca(IPersonaggio bersaglio)
        {
            if (!IsVivo) return;

            Console.WriteLine($"{Nome}  prepara l'attacco");

            // Chiama il metodo specifico del mostro concreto
            EseguiAzzanno(bersaglio);

            // Regola della dispensa: il mostro perde 2 di forza ad ogni azzanno
            Forza -= 2;
            if (Forza < 0) Forza = 0;
        }

        // Ogni mostro specifico (Goblin, Vampiro) deve definire come calcola i danni
        protected abstract void EseguiAzzanno(IPersonaggio bersaglio);
    }
}