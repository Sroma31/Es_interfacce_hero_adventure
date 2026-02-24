namespace RpgGame.Characters.Abstraction
{
    
    public interface IAttackable
    {
        void TakeDamage(int amount, ICharacter attacker);
    }
}
