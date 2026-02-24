using System;
using RpgGame.ValueObjects;

namespace RpgGame.Characters.Monsters
{
    public class Vampire : Monster
    {
        public Vampire(string name) : base(name, 15, 20) { }

        protected override void PerformSpecialAttack(ICharacter target)
        {
            int totalDamage = CalculatePhysicalDamage(10);
            Console.WriteLine($"{Name} drains blood!");
            target.TakeDamage(totalDamage, this);

            
            int healAttempt = 5;

            int effectiveMax = Math.Max(0, MaxHealth);

            int previousHealth = Health;
            int newHealth = Math.Min(previousHealth + healAttempt, effectiveMax);

            int actualHealed = newHealth - previousHealth;

            CharacterHealth = new HealthPoints(newHealth);

            if (actualHealed > 0)
            {
                Console.WriteLine($"{Name} heals {actualHealed} HP.");
            }
            else
            {
                Console.WriteLine($"{Name} is already at full health.");
            }
        }

        private int _resurrections = 0;
        private const int MaxResurrections = 2;

        public override void TakeDamage(int amount, ICharacter attacker)
        {
            base.TakeDamage(amount, attacker);

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} falls to the ground...");

                bool isLethalWeapon = false;

                // Check if attacker has the specific weapon
                if (attacker != null && attacker.EquippedWeapon != null)
                {
                    if (attacker.EquippedWeapon.Name.ToLower().Contains("stake"))
                    {
                        isLethalWeapon = true;
                    }
                }

                if (isLethalWeapon)
                {
                    Console.WriteLine(" THE VAMPIRE TURNS TO DUST (Staked) ");
                }
                else if (_resurrections < MaxResurrections)
                {
                    _resurrections++;
                    Console.WriteLine($"The weapon was not holy... THE VAMPIRE RESURRECTS ({_resurrections}/{MaxResurrections})");
                    CharacterHealth = CharacterMaxHealth;
                }
                else
                {
                    Console.WriteLine($"{Name} is too weak to rise again. THE VAMPIRE IS DEFEATED.");
                }
            }
        }
    }
}