using System;
using RpgGame.Characters;
using RpgGame.Characters.Monsters;
using RpgGame.Environments;

namespace RpgGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            Console.Write("Enter Hero Name: ");
            string heroName = Console.ReadLine();
            Human hero = new Human(heroName);

            Console.Write("Is it full moon tonight? (s/n): ");
            string moonInput = Console.ReadLine();
            Werewolf.IsFullMoon = (moonInput.ToLower() == "s");

            Console.WriteLine("\nChoose Mode:");
            Console.WriteLine("1. Classic Adventure (Travel and fight 1 by 1)");
            Console.WriteLine("2. Horde Mode (Survival Arena)");
            string mode = Console.ReadLine();

            if (mode == "2")
            {
                Console.Write("How many monsters do you want to fight?: ");
                //TryParse is used to safely convert the input to an integer. If the conversion fails ( user enters non-numeric), it will return false.
                //out is used to declare the variable 'count' that will hold the parsed integer value if the conversion is successful.
                if (int.TryParse(Console.ReadLine(), out int count))//
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
                // Classic Logic
                bool keepFighting = true;
                while (hero.IsAlive && keepFighting)
                {
                    Character monster = arena.GenerateRandomMonster();
                    arena.StartDuel(hero, monster);

                    if (hero.IsAlive && hero.Strength > 0)
                    {
                        Console.Write("Continue to next location? (s/n): ");
                        if (Console.ReadLine().ToLower() != "s")// If user doesn't want to continue, exit the loop. ToLower() is used to make it case-insensitive, so both 's' and 'S' will work.
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