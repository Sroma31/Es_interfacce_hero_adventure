using System;

namespace RpgGame.Items
{
    public class Weapon (string Name, int Damage, int Durability)
    {
        public string Name { get; } = Name;
        public int Damage { get; } = Damage;
        public int Durability { get; set; } = Durability;
    }
}