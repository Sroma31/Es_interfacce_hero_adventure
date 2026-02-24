using System;

namespace RpgGame.Items
{
    public record Weapon(string Name, int Damage, int Durability)
    {
        public int Durability { get; set; } = Durability;
    }
}