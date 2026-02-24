using System;
using System.Collections.Generic;
using RpgGame.Items;
using RpgGame.Systems;


namespace RpgGame.Characters
{
    public abstract class Character : ICharacter
    {
        protected static Random Dice = new Random();

        public Weapon? EquippedWeapon { get; protected set; }
        public string Name { get; protected set; }
        public int Strength { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }

        public bool IsAlive => Health > 0;

        public List<LootItem> Loot { get; protected set; } = new List<LootItem>();





        public Character(string name, int initialStrength, int healthDice)
        {
            Name = name;
            Strength = initialStrength;
            // Roll HP based on the provided dice type (e.g., 50 for Humans)
            MaxHealth = Dice.Next(1, healthDice + 1) + 20; 
            Health = MaxHealth;
            Loot.Add(new LootItem());
        }

        
        public void PrintFullLoot()
        {
            if (Loot.Count > 0)
            {
                foreach (LootItem item in Loot)
                {
                    Console.WriteLine($"Loot: {item.GetName()} (Value: {item.GetValue()})");
                }
            }
            else
            {
                Console.WriteLine("No loot available.");
            }
        }



        public void StealLoot(ICharacter target)
        {
            if (target is Character targetCharacter && targetCharacter.Loot.Count > 0)
            {
                //Steal all loot from target
                Loot.AddRange(targetCharacter.Loot);
                targetCharacter.EraseLoot();
            }
            else
            {
                Console.WriteLine($"{Name} tries to steal from {target.Name}, but there's nothing to steal!");
            }
        }


        public void EraseLoot()
        {
            Loot.Clear();
            Console.WriteLine($"{Name}'s loot has been erased.");
        }



        public abstract void Attack(ICharacter target);

        protected void ReduceWeaponDurability()
        {
            if (EquippedWeapon != null)
            {
                EquippedWeapon.Durability -= 1;
                if (EquippedWeapon.Durability <= 0)
                {
                    Console.WriteLine($"{Name}'s {EquippedWeapon.Name} broke!");
                    EquippedWeapon = null;
                }
            }
        }

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
                // Note: Durability reduction moved to Attack in implementation plan
                // But keeping it here for now if needed, or removing it as per plan
                // Move logic to Attack in next step
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
            // Correzione: chiamare il metodo printfulloot() sull'istanza corrente
            Console.WriteLine($"[{Name}] HP: {Health}/{MaxHealth} | STR: {Strength} | WPN: {weaponName}");
            
            
        }



        protected int RollDice(int faces)
        {
            if (faces <= 0) return 0;
            return Dice.Next(1, faces + 1);
        }
    }
}