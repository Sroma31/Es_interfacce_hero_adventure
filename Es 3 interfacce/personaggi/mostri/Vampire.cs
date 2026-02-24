using System;
using RpgGame.Logic;
using RpgGame.ValueObjects;

namespace RpgGame.Characters.Monsters
{
    public class Vampire : Monster
    {
        public Vampire(string name) : base(name, 15, 20) { }

        protected override void PerformSpecialAttack(ICharacter target)
        {
            int totalDamage = CalculatePhysicalDamage(10);
            DisplayManager.PrintBattleLog($"{Name} drains blood!", ConsoleColor.DarkMagenta);
            target.TakeDamage(totalDamage, this);

            
            int healAttempt = 5;

            int effectiveMax = Math.Max(0, MaxHealth);

            int previousHealth = Health;
            int newHealth = Math.Min(previousHealth + healAttempt, effectiveMax);

            int actualHealed = newHealth - previousHealth;

            CharacterHealth = new HealthPoints(newHealth);

            if (actualHealed > 0)
            {
                DisplayManager.PrintBattleLog($"{Name} heals {actualHealed} HP.", ConsoleColor.Green);
            }
            else
            {
                DisplayManager.PrintBattleLog($"{Name} is already at full health.", ConsoleColor.DarkGray);
            }
        }

        private int _resurrections = 0;
        private const int MaxResurrections = 2;

        public override void TakeDamage(int amount, ICharacter attacker)
        {
            base.TakeDamage(amount, attacker);

            if (Health <= 0)
            {
                DisplayManager.PrintBattleLog($"{Name} falls to the ground...", ConsoleColor.Red);

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
                    DisplayManager.PrintBattleLog(" THE VAMPIRE TURNS TO DUST (Staked) ", ConsoleColor.Yellow);
                }
                else if (_resurrections < MaxResurrections)
                {
                    _resurrections++;
                    DisplayManager.PrintBattleLog($"Not holy... THE VAMPIRE RESURRECTS ({_resurrections}/{MaxResurrections})", ConsoleColor.DarkYellow);
                    CharacterHealth = CharacterMaxHealth;
                }
                else
                {
                    DisplayManager.PrintBattleLog($"{Name} is too weak to rise. THE VAMPIRE IS DEFEATED.", ConsoleColor.Green);
                }
            }
        }
    }
}