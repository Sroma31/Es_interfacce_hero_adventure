using System;

namespace RpgGame.Characters.Monsters
{
    public class Werewolf : Monster
    {

        public static bool IsFullMoon { get; set; } = false;


        public Werewolf(string name) : base(name, 12, 30)
        {

        }


        protected override void PerformSpecialAttack(ICharacter target)
        {
            int diceFaces;
            if (IsFullMoon)
            {
                diceFaces = 12;
            }
            else
            {
                diceFaces = 6;
            }

            int totalDamage = CalculatePhysicalDamage(diceFaces);

            string moonStatus;
            if (IsFullMoon)
            {
                moonStatus = "Yes";
            }
            else
            {
                moonStatus = "No";
            }
            Console.WriteLine($"{Name} lunges at the target! (Full moon: {moonStatus})");

            target.TakeDamage(totalDamage, this);

            // During the full moon, werewolves regenerate strength
            if (IsFullMoon)
            {
                CharacterStrength = CharacterStrength + 1;
            }
        }
    }
}