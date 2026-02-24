using System;
using RpgGame.Characters;

namespace RpgGame.Items
{
    public class Denaro : LootItem
    {
        public override string Name => "Denaro";

        public override void ApplyEffect(Character target)
        {
            target.AddHealth(5);
            Console.WriteLine($"{target.Name} used Denaro to buy a health potion! (+5 HP)");
        }
    }
}
