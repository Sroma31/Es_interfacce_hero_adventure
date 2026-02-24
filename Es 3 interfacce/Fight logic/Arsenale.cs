using System;
using System.Collections.Generic;
using RpgGame.Items;

namespace RpgGame.Systems
{
    public static class Arsenal
    {
        // Private list of weapon templates
        private static List<Weapon> _weaponTemplates = new List<Weapon>()
        {
            new Weapon("Pistol", 10, 100),
            new Weapon("Shotgun", 15, 120),
            new Weapon("Knife", 5, 80),
            new Weapon("Crossbow", 12, 90),
            new Weapon("Battle Axe", 8, 110),
            new Weapon("Wooden Stake", 5, 20) // Special weapon for Vampires
        };

        private static Random _rnd = new Random();

        public static Weapon GetRandomWeapon()
        {
            int index = _rnd.Next(_weaponTemplates.Count);
            Weapon template = _weaponTemplates[index];

            // Return a NEW instance to ensure unique durability per character
            return new Weapon(template.Name, template.Damage, template.Durability);
        }
    }
}