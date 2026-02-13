using System;

namespace RpgGame.Characters.Monsters
{
    public class Goblin : Monster
    {
        public Goblin(string name) : base(name, 6, 15) { }

        protected override void PerformSpecialAttack(ICharacter target)
        {
            int totalDamage = CalculatePhysicalDamage(4); // Weak attack
            Console.WriteLine($"{Name} strikes sneakily!");
            target.TakeDamage(totalDamage, this);
        }
    }
}