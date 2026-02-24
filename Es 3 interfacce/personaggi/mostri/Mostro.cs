using System;
using RpgGame.Logic;
using RpgGame.ValueObjects;

namespace RpgGame.Characters.Monsters
{
    public abstract class Monster : Character
    {
        public Monster(string name, int strength, int healthDice)
            : base(name, strength, healthDice) { }

        public void Scale(int level)
        {
            int boost = level * 2;
            CharacterStrength = CharacterStrength + boost;
            CharacterMaxHealth = CharacterMaxHealth + boost;
            CharacterHealth = CharacterMaxHealth;
            DisplayManager.PrintBattleLog($"{Name} is empowered! (+{boost} STR/HP)", ConsoleColor.Magenta);
        }

        public override void Attack(ICharacter target)
        {
            if (!IsAlive) return;

            PerformSpecialAttack(target);
            ReduceWeaponDurability();

            // Monsters lose energy/strength after attacking
            CharacterStrength = CharacterStrength - 2;
        }

        protected abstract void PerformSpecialAttack(ICharacter target);
    }
}