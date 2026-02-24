using System;
using RpgGame.Characters;

namespace RpgGame.Items
{
    public class Totem : LootItem
    {
        public override string Name => "Totem";

        public override void ApplyEffect(Character target)
        {
            target.ApplyMaxHealthBoost(10);
            target.AddHealth(10);
            Console.WriteLine($"{target.Name} used Totem to increase vitality! (+10 MaxHP)");
        }
    }
}
