namespace RpgGame.Characters.Abstraction
{
    
    public interface ILootable
    {
        void StealLoot(ICharacter target);
        void EraseLoot();
        void PrintFullLoot();
    }
}
