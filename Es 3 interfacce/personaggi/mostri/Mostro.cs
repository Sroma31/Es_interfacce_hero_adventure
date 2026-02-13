using System;

namespace RpgGame.Characters.Monsters
{
    public abstract class Monster : Character
    {
        public Monster(string name, int strength, int healthDice)
            : base(name, strength, healthDice) { }

        public override void Attack(ICharacter target)
        {
            if (!IsAlive) return;

            PerformSpecialAttack(target);

            // Monsters lose energy/strength after attacking
            if (Strength > 0) Strength -= 2;
            if (Strength < 0) Strength = 0;
        }

        protected abstract void PerformSpecialAttack(ICharacter target);
    }
}