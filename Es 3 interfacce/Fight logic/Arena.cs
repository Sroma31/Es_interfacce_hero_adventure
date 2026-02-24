using System;
using System.Threading;
using System.Collections.Generic;
using RpgGame.Characters;
using RpgGame.Characters.Monsters;
using RpgGame.Systems;
using RpgGame.Items;

namespace RpgGame.Logic
{   
    public class Arena
    {
        private Random _rnd = new Random();
        public EnvironmentType CurrentEnvironment { get; private set; }

        public Arena()
        {
            ChangeEnvironment();
        }

        public void StartDuel(Human hero, Character monster)
        {
            Console.Clear();
            Console.WriteLine($" DUEL IN: {CurrentEnvironment} ");
            Console.WriteLine($"{hero.Name} VS {monster.Name}");
            

            if (hero.EquippedWeapon == null) hero.EquipRandomWeapon();
            monster.EquipRandomWeapon();


            BattleLoop(hero, monster);
        }

        public void StartHordeMode(Human hero, int numberOfMonsters)
        {
            Console.Clear();
            Console.WriteLine($" HORDE MODE ACTIVATED");
            Console.WriteLine($"Hero: {hero.Name}");
            Console.WriteLine($"Objective: Survive {numberOfMonsters} monsters.");
            Console.WriteLine("");
            Thread.Sleep(2000);

            if (hero.EquippedWeapon == null) hero.EquipRandomWeapon();

            int defeatedCount = 0;

            for (int i = 1; i <= numberOfMonsters; i++)
            {
                if (!hero.IsAlive) break;

                Console.WriteLine($"\n WAVE {i}/{numberOfMonsters} ");
                Character monster = GenerateRandomMonster();
                monster.EquipRandomWeapon();

                Console.WriteLine($" {monster.Name} appears");

                BattleLoop(hero, monster);

                if (hero.IsAlive && hero.Strength > 0)
                {
                    defeatedCount++;
                    Console.WriteLine($"Wave {i} cleared");
                    hero.EquipRandomWeapon();
                }
            }

            Console.WriteLine("\n HORDE MODE RESULTS ");
            if (hero.IsAlive)
                Console.WriteLine("VICTORY. The hero survived the horde.");
            else
                Console.WriteLine($"DEFEAT... The hero fell after defeating {defeatedCount} monsters.");
        }

        private void BattleLoop(Human hero, Character monster)
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

        public Character GenerateRandomMonster()
        {
            int roll = _rnd.Next(1, 101);

            switch (CurrentEnvironment)
            {
                case EnvironmentType.DarkCastle:
                    if (roll <= 40) return new Vampire("Count Dracula");
                    if (roll <= 70) return new Werewolf("Royal Werewolf");
                    return new Goblin("Butler Goblin");

                case EnvironmentType.CursedForest:
                    if (roll <= 20) return new Vampire("Wandering Vampire");
                    if (roll <= 60) return new Werewolf("Alpha Wolf");
                    return new Goblin("Forest Goblin");

                default: // GoblinCave
                    if (roll <= 5) return new Vampire("Lost Vampire");
                    if (roll <= 20) return new Werewolf("Cave Werewolf");
                    return new Goblin("Goblin King");
            }
        }

        public void ChangeEnvironment()
        {
            List<EnvironmentType> values = new List<EnvironmentType>(Enum.GetValues<EnvironmentType>()); 
            CurrentEnvironment = values[_rnd.Next(values.Count)];
        }
    }
}
