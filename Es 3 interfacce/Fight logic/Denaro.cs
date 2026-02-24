using System;
using RpgGame.Characters;
using RpgGame.Logic;

namespace RpgGame.Items
{
    public class Denaro : LootItem
    {
        public override string Name => "Denaro";

        public override void ApplyEffect(Character target)
        {
            target.AddHealth(5);
            DisplayManager.PrintBattleLog($"{target.Name} used Denaro! (+5 HP)", ConsoleColor.Green);
        }
    }
}
