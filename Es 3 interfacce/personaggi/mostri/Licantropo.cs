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
            if (IsFullMoon == true)
            {
                diceFaces = 12;
            }
            else
            {
                diceFaces = 6;
            }


            int totalDamage = CalculatePhysicalDamage(diceFaces);

          
            Console.WriteLine($"{this.Name} si scaglia contro il bersaglio! (Luna piena: {(IsFullMoon ? "Sì" : "No")})");

            // Applica il danno al bersaglio; passa 'this' come attaccante per eventuali check
            target.TakeDamage(totalDamage, this);

            // Durante la luna piena i lupi mannari rigenerano forza: incrementiamo Strength di 1
            if (IsFullMoon == true)
            {
                int nuovaForza = this.Strength + 1;
                this.Strength = nuovaForza;
            }
        }
    }
}