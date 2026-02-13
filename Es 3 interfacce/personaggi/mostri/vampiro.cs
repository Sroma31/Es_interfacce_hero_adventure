using System;

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

            // Protezione contro valori anomali di MaxHealth
            int effectiveMax = Math.Max(0, MaxHealth);//math.max per evitare valori negativi di MaxHealth

            int previousHealth = Health;
            int newHealth = Math.Min(previousHealth + healAttempt, effectiveMax);// Calcolo reale della cura effettiva
            // math.min per evitare di superare MaxHealth

            int actualHealed = newHealth - previousHealth;

            Health = newHealth;

            if (actualHealed > 0)
            {
                Console.WriteLine($"{Name} heals {actualHealed} HP.");
            }
            else
            {
                Console.WriteLine($"{Name} is already at full health.");
            }
        }

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
                else
                {
                    Console.WriteLine("The weapon was not holy... THE VAMPIRE RESURRECTS");
                    Health = MaxHealth;
                }
            }
        }
    }
}