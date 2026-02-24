using System;
using RpgGame.Characters;
using RpgGame.Logic;

namespace RpgGame.Items
{
    public class Totem : LootItem
    {
        public override string Name => "Totem";

        public override void ApplyEffect(Character target)
        {
            target.ApplyMaxHealthBoost(10);
            target.AddHealth(10);
            DisplayManager.PrintBattleLog($"{target.Name} used Totem! (+10 MaxHP)", ConsoleColor.Green);
        }
    }
}
