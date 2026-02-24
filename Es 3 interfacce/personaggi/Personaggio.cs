using System;
using System.Collections.Generic;
using RpgGame.Items;
using RpgGame.Systems;
using RpgGame.ValueObjects;
using RpgGame.Characters.Abstraction;
using RpgGame.Logic;


namespace RpgGame.Characters
{
    public abstract class Character : ICharacter
    {
        protected static Random Dice = new Random();

        public Weapon? EquippedWeapon { get; protected set; }
        public CharacterName Name { get; protected set; }
        
        // Value objects handle clamping internally
        public Strength CharacterStrength { get; protected set; }
        public HealthPoints CharacterHealth { get; protected set; }
        public HealthPoints CharacterMaxHealth { get; protected set; }

        // ICharacter properties delegate to value objects (implicit int conversion)
        int ICharacter.Strength => CharacterStrength;
        int ICharacter.Health => CharacterHealth;
        int ICharacter.MaxHealth => CharacterMaxHealth;

        // Convenience properties for internal use
        public int Strength => CharacterStrength;
        public int Health => CharacterHealth;
        public int MaxHealth => CharacterMaxHealth;

        public bool IsAlive => CharacterHealth > 0;

        public Inventory Backpack { get; protected set; } = new Inventory();


        public Character(string name, int initialStrength, int healthDice)
        {
            Name = new CharacterName(name);
            CharacterStrength = new Strength(initialStrength);
            // Roll HP based on the provided dice type (e.g., 50 for Humans)
            int maxHp = Dice.Next(1, healthDice + 1) + 20; 
            CharacterMaxHealth = new HealthPoints(maxHp);
            CharacterHealth = new HealthPoints(maxHp);
            Backpack.Add(LootItem.GetRandomLoot());
        }

        
        public void PrintFullLoot()
        {
            Backpack.PrintContents(Name);
        }



        public void StealLoot(ICharacter target)
        {
            if (target is Character targetCharacter && targetCharacter.Backpack.Items.Count > 0)
            {
                // Create a separate reference to stolen items for immediate effect processing
                List<ILootItem> stolenItems = new List<ILootItem>(targetCharacter.Backpack.Items);
                
                // Steal (transfer) all loot from target
                Backpack.TransferFrom(targetCharacter.Backpack);
                
                // Hero gains benefits from loot immediately upon stealing
                if (this is Human)
                {
                    ApplyLootEffects(stolenItems);
                }
            }
            else
            {
                DisplayManager.PrintBattleLog($"{Name} tries to steal from {target.Name}, but nothing to steal!", ConsoleColor.DarkYellow);
            }
        }

        private void ApplyLootEffects(List<ILootItem> items)
        {
            foreach (var item in items)
            {
                item.ApplyEffect(this);
            }
        }

        public void AddHealth(int amount)
        {
            int newHp = Math.Min(CharacterHealth + amount, CharacterMaxHealth);
            CharacterHealth = new HealthPoints(newHp);
        }

        public void ApplyStrengthBoost(int amount)
        {
            CharacterStrength = CharacterStrength + amount;
        }

        public void ApplyMaxHealthBoost(int amount)
        {
            CharacterMaxHealth = CharacterMaxHealth + amount;
        }


        public void EraseLoot()
        {
            Backpack.Clear();
            DisplayManager.PrintBattleLog($"{Name}'s loot has been erased.", ConsoleColor.DarkGray);
        }



        public abstract void Attack(ICharacter target);

        protected void ReduceWeaponDurability()
        {
            if (EquippedWeapon != null)
            {
                EquippedWeapon.Durability -= 1;
                if (EquippedWeapon.Durability <= 0)
                {
                    DisplayManager.PrintBattleLog($"{Name}'s {EquippedWeapon.Name} broke!", ConsoleColor.Red);
                    EquippedWeapon = null;
                }
            }
        }

        // Centralized damage calculation
        protected int CalculatePhysicalDamage(int baseDiceFaces)
        {
            int baseRoll = RollDice(baseDiceFaces);

            // Strength multiplier (min 1 if strength > 0)
            int multiplier;
            if (Strength > 0)
            {
                multiplier = Math.Max(1, Strength / 2);
            }
            else
            {
                multiplier = 0;
            }

            int weaponDamage;
            if (EquippedWeapon != null)
            {
                weaponDamage = EquippedWeapon.Damage;
            }
            else
            {
                weaponDamage = 0;
            }

            return (baseRoll * multiplier) + weaponDamage;
        }

        public virtual void TakeDamage(int amount, ICharacter attacker)
        {
            CharacterHealth = CharacterHealth - amount;
            DisplayManager.PrintBattleLog($"{Name} takes {amount} damage (HP: {Health}/{MaxHealth})", ConsoleColor.DarkRed);
        }

        public void EquipRandomWeapon()
        {
            EquippedWeapon = Arsenal.GetRandomWeapon();
            DisplayManager.PrintBattleLog($"{Name} equipped {EquippedWeapon.Name} (Dmg: {EquippedWeapon.Damage})", ConsoleColor.Cyan);
        }
        
        public void PrintStatus()
        {
            string weaponName;
            if (EquippedWeapon != null)
            {
                weaponName = EquippedWeapon.Name;
            }
            else
            {
                weaponName = "Fists";
            }
            DisplayManager.PrintBattleLog($"[{Name}] HP: {Health}/{MaxHealth} | STR: {Strength} | WPN: {weaponName}");
        }

        protected int RollDice(int faces)
        {
            if (faces <= 0) return 0;
            return Dice.Next(1, faces + 1);
        }
    }
}