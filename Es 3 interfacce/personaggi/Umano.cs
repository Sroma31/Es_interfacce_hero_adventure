using System;

namespace RpgGame.Characters
{
    public class Human : Character
    {
        public Human(string name) 
            : base(name, 10, 50)
        { 
        
        }

        public override void Attack(ICharacter target)
        {
            if (!IsAlive) return;

            Console.WriteLine($"{Name} attacks");

            int totalDamage = CalculatePhysicalDamage(6); // 1d6 Base
            target.TakeDamage(totalDamage, this);

            // Humans get tired
            Strength -= 1;
            if (Strength < 0) Strength = 0;
        }
    }
}