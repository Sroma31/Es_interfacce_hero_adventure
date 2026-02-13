using System;
using RpgGame.Items;
using RpgGame.Systems;

namespace RpgGame.Characters
{
    public abstract class Character : ICharacter
    {
        protected static Random Dice = new Random();

        public Weapon EquippedWeapon { get; protected set; }
        public string Name { get; protected set; }
        public int Strength { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }

        public bool IsAlive => Health > 0;

        public Character(string name, int initialStrength, int healthDice)
        {
            Name = name;
            Strength = initialStrength;
            // Roll HP based on the provided dice type (e.g., 50 for Humans)
            MaxHealth = Dice.Next(1, healthDice + 1) + 20; // Added base 20 HP so they don't start with 1
            Health = MaxHealth;
        }

        public abstract void Attack(ICharacter target);

        // Centralized damage calculation
        protected int CalculatePhysicalDamage(int baseDiceFaces)
        {
            int baseRoll = RollDice(baseDiceFaces);

            // Strength multiplier (min 1 if strength > 0)
            int multiplier = (Strength > 0) ? Math.Max(1, Strength / 2) : 0;

            int weaponDamage = (EquippedWeapon != null) ? EquippedWeapon.Damage : 0;

            return (baseRoll * multiplier) + weaponDamage;
        }

        public virtual void TakeDamage(int amount, ICharacter attacker)
        {
            Health -= amount;
            if (Health < 0)
            {
                Health = 0;
            }
            Console.WriteLine($"{Name} takes {amount} damage (HP: {Health}/{MaxHealth})");

            // Handle Weapon Durability degradation
            if (EquippedWeapon != null)
            {
                EquippedWeapon.Durability -= 1;
                if (EquippedWeapon.Durability <= 0)
                {
                    Console.WriteLine($"CRACK! {Name}'s {EquippedWeapon.Name} broke!");
                    EquippedWeapon = null;
                }
            }
        }

        public void EquipRandomWeapon()
        {
            EquippedWeapon = Arsenal.GetRandomWeapon();
            Console.WriteLine($"{Name} equipped {EquippedWeapon.Name} (Dmg: {EquippedWeapon.Damage})");
        }

        public void PrintStatus()
        {
            string weaponName = (EquippedWeapon != null) ? EquippedWeapon.Name : "Fists";
            Console.WriteLine($"[{Name}] HP: {Health}/{MaxHealth} | STR: {Strength} | WPN: {weaponName}");
        }

        protected int RollDice(int faces)
        {
            if (faces <= 0) return 0;
            return Dice.Next(1, faces + 1);
        }
    }
}