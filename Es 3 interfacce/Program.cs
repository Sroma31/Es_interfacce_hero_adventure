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
            bool playAgain = true;

            while (playAgain)
            {
                Arena arena = new Arena();

                DisplayManager.DrawHeader("HERO ADVENTURE", EnvironmentType.CursedForest);

                Console.Write(" Enter Hero Name: ");
                string heroName = Console.ReadLine() ?? "Hero";
                Human hero = new Human(heroName);

                Console.WriteLine();
                Console.Write(" Is it full moon tonight? (s/n): ");
                string moonInput = Console.ReadLine() ?? "n";
                Werewolf.IsFullMoon = (moonInput.ToLower() == "s");

                bool exit = false;
                while (!exit && hero.IsAlive)
                {
                    DisplayManager.DrawHeader("MAIN MENU", arena.CurrentEnvironment);
                    DisplayManager.PrintMessage($" Hero: {hero.Name} | HP: {hero.Health}/{hero.MaxHealth} | STR: {hero.Strength}", ConsoleColor.Green);
                    Console.WriteLine();
                    DisplayManager.PrintMessage(" 1. Classic Adventure (Travel and fight)", ConsoleColor.Gray);
                    DisplayManager.PrintMessage(" 2. Horde Mode (Survival Arena)", ConsoleColor.Gray);
                    DisplayManager.PrintMessage(" 3. View Inventory", ConsoleColor.Gray);
                    DisplayManager.PrintMessage(" 0. Exit Game", ConsoleColor.Gray);
                    Console.WriteLine();
                    Console.Write(" Choose Mode: ");

                    string mode = Console.ReadLine() ?? "0";

                    switch (mode)
                    {
                        case "1":
                            bool keepFighting = true;
                            while (hero.IsAlive && keepFighting)
                            {
                                Character monster = arena.GenerateRandomMonster();
                                arena.StartDuel(hero, monster);

                                if (hero.IsAlive && hero.Strength > 0)
                                {
                                    hero.AddHealth(5);
                                    DisplayManager.PrintMessage("\n Hero rests for a bit (+5 HP).", ConsoleColor.Green);

                                    Console.Write(" Continue to next location? (s/n): ");
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
                            break;

                        case "2":
                            Random rnd = new Random();
                            int count = rnd.Next(1, 16);
                            DisplayManager.PrintMessage($" You will face {count} monsters!", ConsoleColor.Yellow);
                            arena.StartHordeMode(hero, count);
                            break;

                        case "3":
                            DisplayManager.DrawHeader("INVENTORY", arena.CurrentEnvironment);
                            hero.PrintFullLoot();
                            Console.WriteLine("\n Press any key to return...");
                            Console.ReadKey();
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            DisplayManager.PrintMessage(" Invalid choice.", ConsoleColor.Red);
                            Thread.Sleep(1000);
                            break;
                    }
                }

                DisplayManager.DrawHeader("GAME OVER", arena.CurrentEnvironment);
                if (!hero.IsAlive)
                {
                    DisplayManager.CenterPrint("Your hero has fallen. Legends will remember you.", ConsoleColor.Red);
                    Console.WriteLine();
                    Console.Write(" Do you want to start a new adventure? (s/n): ");
                    string again = Console.ReadLine() ?? "n";
                    playAgain = (again.ToLower() == "s");
                }
                else
                {
                    DisplayManager.CenterPrint("Thanks for playing!", ConsoleColor.Cyan);
                    playAgain = false;
                }

                if (!playAgain)
                {
                    Console.WriteLine("\n Press Enter to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
