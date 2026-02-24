using System;
using RpgGame.Characters;

namespace RpgGame.Items
{
    public abstract class LootItem : ILootItem
    {
        protected static Random Dice = new Random();
        public abstract string Name { get; }
        public int Value { get; protected set; }

        public LootItem()
        {
            Value = Dice.Next(1, 21);
        }

        public string GetName() => Name;
        public int GetValue() => Value;

        public abstract void ApplyEffect(Character target);

        public static ILootItem GetRandomLoot()
        {
            int roll = Dice.Next(0, 3);
            switch (roll)
            {
                case 0:
                    return new Denaro();
                case 1:
                    return new Smeraldo();
                default:
                    return new Totem();
            }
        }
    }
}
