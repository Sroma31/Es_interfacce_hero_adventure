using System;
using RpgGame.Characters;
using RpgGame.Characters.Monsters;

namespace RpgGame.Logic
{
 
    public class MonsterFactory
    {
        private readonly Random _rnd = new Random();
        
        private readonly string[] _vampireNames = { "Count Dracula", "Vlad the Impaler", "Carmilla", "Lestat", "Nosferatu" };
        private readonly string[] _werewolfNames = { "Royal Werewolf", "Fenrir", "Big Bad Wolf", "Alpha Wolf", "Silver Fang" };
        private readonly string[] _goblinNames = { "Butler Goblin", "Sneaky Grib", "Cave Skulker", "Goblin King", "Green Menace" };

        public Character CreateRandomMonster(EnvironmentType environment)
        {
            int roll = _rnd.Next(1, 101);

            switch (environment)
            {
                case EnvironmentType.DarkCastle:
                    if (roll <= 40)
                    {
                        return new Vampire(GetRandomName(_vampireNames));
                    }
                    else if (roll <= 70)
                    {
                        return new Werewolf(GetRandomName(_werewolfNames));
                    }
                    else
                    {
                        return new Goblin(GetRandomName(_goblinNames));
                    }

                case EnvironmentType.CursedForest:
                    if (roll <= 20)
                    {
                        return new Vampire(GetRandomName(_vampireNames));
                    }
                    else if (roll <= 60)
                    {
                        return new Werewolf(GetRandomName(_werewolfNames));
                    }
                    else
                    {
                        return new Goblin(GetRandomName(_goblinNames));
                    }

                default: // GoblinCave
                    if (roll <= 5)
                    {
                        return new Vampire(GetRandomName(_vampireNames));
                    }
                    else if (roll <= 20)
                    {
                        return new Werewolf(GetRandomName(_werewolfNames));
                    }
                    else
                    {
                        return new Goblin(GetRandomName(_goblinNames));
                    }
            }
        }

        public void ApplyScaling(Character character, int level)
        {
            if (character is Monster monster)
            {
                monster.Scale(level);
            }
        }

        private string GetRandomName(string[] names)
        {
            return names[_rnd.Next(names.Length)];
        }
    }
}
