namespace Es_3_interfacce.personaggi
{
    public interface IPersonaggio
    {
        string Nome { get; }
        int Forza { get; }
        int Vita { get; }
        int VitaMax { get; }
        bool IsVivo { get; }

        void Attacca(IPersonaggio bersaglio);
        void SubisciDanno(int danno);
        void StampaStato();
    }
}