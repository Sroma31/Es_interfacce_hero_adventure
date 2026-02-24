using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Items
{
    public class LootItem
    {
        protected static Random Dice = new Random();
        public LootNames Name { get; set; }
        public int Value { get; set; }

        public LootItem()
        {
            // Simplified random enum selection
            var values = Enum.GetValues<LootNames>();
            Name = values[Dice.Next(values.Length)];
            Value = Dice.Next(1, 21);
        }

        public LootNames GetName() => Name;
        public int GetValue() => Value;   
    }
}
