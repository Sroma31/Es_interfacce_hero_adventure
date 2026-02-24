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
            Console.Clear();
            Console.WriteLine($" DUEL IN: {CurrentEnvironment} ");
            Console.WriteLine($"{hero.Name} VS {monster.Name}");
            

            if (hero.EquippedWeapon == null) hero.EquipRandomWeapon();
            monster.EquipRandomWeapon();


            _battleEngine.ProcessDuel(hero, monster);
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
                
                Character monster = _monsterFactory.CreateRandomMonster(CurrentEnvironment);
                _monsterFactory.ApplyScaling(monster, i);

                monster.EquipRandomWeapon();

                Console.WriteLine($" {monster.Name} appears");

                _battleEngine.ProcessDuel(hero, monster);

                if (hero.IsAlive && hero.Strength > 0)
                {
                    defeatedCount++;
                    Console.WriteLine($"Wave {i} cleared! Hero rests for a bit (+5 HP).");
                    hero.AddHealth(5);
                    hero.EquipRandomWeapon();
                }
            }

            Console.WriteLine("\n HORDE MODE RESULTS ");
            if (hero.IsAlive)
                Console.WriteLine("VICTORY. The hero survived the horde.");
            else
                Console.WriteLine($"DEFEAT... The hero fell after defeating {defeatedCount} monsters.");
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
