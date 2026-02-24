using RpgGame.Items;
using RpgGame.Characters.Abstraction;
using RpgGame.ValueObjects;

namespace RpgGame.Characters
{

    public interface ICharacter : IAttackable, ILootable
    {
        CharacterName Name { get; }
        int Strength { get; }
        int Health { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        Weapon? EquippedWeapon { get; }

        void Attack(ICharacter target);
        void PrintStatus();
    }
}