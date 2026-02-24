using System;
using RpgGame.Characters;
using RpgGame.Logic;

namespace RpgGame.Items
{
    public class Smeraldo : LootItem
    {
        public override string Name => "Smeraldo";

        public override void ApplyEffect(Character target)
        {
            target.ApplyStrengthBoost(2);
            DisplayManager.PrintBattleLog($"{target.Name} used Smeraldo! (+2 STR)", ConsoleColor.Green);
        }
    }
}
