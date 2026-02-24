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
        private readonly MonsterFactory _monsterFactory = new MonsterFactory();
        private readonly BattleEngine _battleEngine = new BattleEngine();
        private readonly EnvironmentManager _envManager = new EnvironmentManager();

        public EnvironmentType CurrentEnvironment => _envManager.CurrentEnvironment;

        public void StartDuel(Human hero, Character monster)
        {
            DisplayManager.DrawHeader("DUEL BEGINS", CurrentEnvironment);
            DisplayManager.CenterPrint($"{hero.Name} VS {monster.Name}", ConsoleColor.Red);
            Console.WriteLine();

            if (hero.EquippedWeapon == null) hero.EquipRandomWeapon();
            monster.EquipRandomWeapon();

            Thread.Sleep(1000);
            _battleEngine.ProcessDuel(hero, monster);
        }

        public void StartHordeMode(Human hero, int numberOfMonsters)
        {
            DisplayManager.DrawHeader("HORDE MODE", CurrentEnvironment);
            DisplayManager.CenterPrint($"Objective: Survive {numberOfMonsters} monsters.", ConsoleColor.Yellow);
            Console.WriteLine("");
            Thread.Sleep(1500);

            if (hero.EquippedWeapon == null) hero.EquipRandomWeapon();

            int defeatedCount = 0;

            for (int i = 1; i <= numberOfMonsters; i++)
            {
                if (!hero.IsAlive) break;

                DisplayManager.PrintMessage($"\n --- WAVE {i}/{numberOfMonsters} ---", ConsoleColor.Cyan);
                
                Character monster = _monsterFactory.CreateRandomMonster(CurrentEnvironment);
                _monsterFactory.ApplyScaling(monster, i);

                monster.EquipRandomWeapon();

                DisplayManager.PrintMessage($"{monster.Name} appears!", ConsoleColor.DarkYellow);
                Thread.Sleep(1000);

                _battleEngine.ProcessDuel(hero, monster);

                if (hero.IsAlive && hero.Strength > 0)
                {
                    defeatedCount++;
                    DisplayManager.PrintMessage($"Wave {i} cleared! Hero rests (+5 HP).", ConsoleColor.Green);
                    hero.AddHealth(5);
                    hero.EquipRandomWeapon();
                }
            }

            DisplayManager.DrawHeader("HORDE MODE RESULTS", CurrentEnvironment);
            if (hero.IsAlive)
                DisplayManager.CenterPrint("VICTORY! The hero survived the horde.", ConsoleColor.Green);
            else
                DisplayManager.CenterPrint($"DEFEAT... The hero fell after defeating {defeatedCount} monsters.", ConsoleColor.Red);
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public Character GenerateRandomMonster()
        {
            return _monsterFactory.CreateRandomMonster(CurrentEnvironment);
        }

        public void ChangeEnvironment()
        {
            _envManager.ChangeEnvironment();
        }
    }
}
