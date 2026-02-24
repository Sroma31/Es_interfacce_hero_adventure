using System;
using System.Threading;
using RpgGame.Characters;

namespace RpgGame.Logic
{
    
    public class BattleEngine
    {
        public void ProcessDuel(Human hero, Character monster)
        {
            while (hero.IsAlive && monster.IsAlive)
            {
                hero.PrintStatus();
                hero.PrintFullLoot();

                if (hero.Strength <= 0)
                {
                    Console.WriteLine("Hero is exhausted (0 Strength)!");
                    break;
                }

                Console.Write("\n[A]ttack or [F]lee? ");
                string input = Console.ReadLine();
                string choice;
                if (input != null)
                {
                    choice = input.ToLower();
                }
                else
                {
                    choice = "a";
                }

                if (choice == "f")
                {
                    Console.WriteLine($"{hero.Name} flees the battle! (-10 HP penalty)");
                    hero.TakeDamage(10, monster);
                    break;
                }

                hero.Attack(monster);
                if (!monster.IsAlive)
                {
                    hero.StealLoot(monster);
                    hero.PrintFullLoot();
                    break;
                }

                Console.WriteLine();

                monster.Attack(hero);

                Thread.Sleep(500);
            }
        }
    }
}
