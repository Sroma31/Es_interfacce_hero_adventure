using System;
using System.Threading;
using RpgGame.Characters;

namespace RpgGame.Logic
{
    
    public class BattleEngine
    {
        public void ProcessDuel(Human hero, Character monster)
        {
            DisplayManager.ResetBattleLog();

            while (hero.IsAlive && monster.IsAlive)
            {
                DisplayManager.UpdateBattleScreen(hero, monster);

                if (hero.Strength <= 0)
                {
                    DisplayManager.PrintBattleLog($"{hero.Name} is exhausted (0 Strength)!", ConsoleColor.Red);
                    Thread.Sleep(1500);
                    break;
                }

                // Clear prompt area and show input
                Console.SetCursorPosition(0, 11);
                Console.Write(new string(' ', 80));
                Console.SetCursorPosition(0, 11);
                Console.Write(" [A]ttack or [F]lee? ");
                string? input = Console.ReadLine();
                string choice = input?.ToLower() ?? "a";

                if (choice == "f")
                {
                    DisplayManager.PrintBattleLog($"{hero.Name} flees the battle! (-10 HP penalty)", ConsoleColor.Magenta);
                    hero.TakeDamage(10, monster);
                    Thread.Sleep(1200);
                    break;
                }

                // Hero attacks
                hero.Attack(monster);
                DisplayManager.UpdateBattleScreen(hero, monster);

                if (!monster.IsAlive)
                {
                    DisplayManager.PrintBattleLog($"VICTORY! {monster.Name} has been defeated.", ConsoleColor.Green);
                    Thread.Sleep(1500);
                    hero.StealLoot(monster);
                    Thread.Sleep(1000);
                    break;
                }

                Thread.Sleep(1200);

                // Monster attacks
                monster.Attack(hero);
                DisplayManager.UpdateBattleScreen(hero, monster);

                if (!hero.IsAlive)
                {
                    DisplayManager.PrintBattleLog($"{hero.Name} has fallen...", ConsoleColor.Red);
                    Thread.Sleep(1500);
                }

                Thread.Sleep(1200);
            }
        }
    }
}
