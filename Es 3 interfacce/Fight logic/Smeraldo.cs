using System;
using RpgGame.Characters;

namespace RpgGame.Items
{
    public class Smeraldo : LootItem
    {
        public override string Name => "Smeraldo";

        public override void ApplyEffect(Character target)
        {
            target.ApplyStrengthBoost(2);
            Console.WriteLine($"{target.Name} used Smeraldo to boost strength! (+2 STR)");
        }
    }
}
