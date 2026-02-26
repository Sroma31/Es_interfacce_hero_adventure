using System;
using RpgGame.Logic;

namespace RpgGame.Characters
{
    public class Human : Character
    {
        public Human(string name) 
            : base(name, 20, 100)
        { 
        
        }

        public override void Attack(ICharacter target)
        {
            if (!IsAlive) return;

            DisplayManager.PrintBattleLog($"{Name} attacks!", ConsoleColor.White);

            int totalDamage = CalculatePhysicalDamage(6); // 1d6 Base
            target.TakeDamage(totalDamage, this);

            ReduceWeaponDurability();

            // Humans get tired
            CharacterStrength = CharacterStrength - 1;
        }
    }
}