using System;
using RpgGame.Logic;
using RpgGame.Characters;
using RpgGame.Characters.Monsters;

namespace RpgGame.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            Console.Write("Enter Hero Name: ");
            string heroName = Console.ReadLine() ?? "Hero";
            Human hero = new Human(heroName);

            Console.Write("Is it full moon tonight? (s/n): ");
            string moonInput = Console.ReadLine() ?? "n";
            Werewolf.IsFullMoon = (moonInput.ToLower() == "s");

            Console.WriteLine("\nChoose Mode:");
            Console.WriteLine("1. Classic Adventure (Travel and fight 1 by 1)");
            Console.WriteLine("2. Horde Mode (Survival Arena)");
            string mode = Console.ReadLine() ?? "1";

            if (mode == "2")
            {
                Console.Write("How many monsters do you want to fight?: ");
                if (int.TryParse(Console.ReadLine(), out int count))
                {
                    arena.StartHordeMode(hero, count);
                }
                else
                {
                    Console.WriteLine("Invalid number.");
                }
            }
            else
            {
                bool keepFighting = true;
                while (hero.IsAlive && keepFighting)
                {
                    Character monster = arena.GenerateRandomMonster();
                    arena.StartDuel(hero, monster);

                    if (hero.IsAlive && hero.Strength > 0)
                    {
                        Console.Write("Continue to next location? (s/n): ");
                        string cont = Console.ReadLine() ?? "n";
                        if (cont.ToLower() != "s")
                            keepFighting = false;
                        else
                            arena.ChangeEnvironment();
                    }
                    else
                    {
                        keepFighting = false;
                    }
                }
            }

            Console.WriteLine("\nPress Enter to exit.");
            Console.ReadLine();
        }
    }
}
