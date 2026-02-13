using RpgGame.Items;

namespace RpgGame.Characters
{
    public interface ICharacter
    {
        string Name { get; }
        int Strength { get; }
        int Health { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        Weapon EquippedWeapon { get; }

        void Attack(ICharacter target);

        // We pass the attacker to check for specific weaknesses (e.g., Vampire vs Stake)
        void TakeDamage(int amount, ICharacter attacker);

        void PrintStatus();
    }
}