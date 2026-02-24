using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es_3_interfacce.Fight_logic
{
    public class LootItem
    {
        protected static Random Dice = new Random();
        public LootNames Name { get; set; }
        public int Value { get; set; }

        public LootItem()
        {
            int NameValue= RollDice(LootNames.GetNames(typeof(LootNames)).Length);
            if(NameValue == 0) NameValue = 1; // Ensure we don't get 0  
            if (NameValue == 1)
            {
                Name = LootNames.Denaro;
            }else if (NameValue == 2)
            {
                Name = LootNames.Totem;
            }
            else if (NameValue == 3)
            {
                Name = LootNames.Smeraldo;

            }

            Value = RollDice(20);
        }

        public LootNames GetName()
        {
            return Name;
        }

        public int GetValue()
        {
            return Value;
        }   



        protected int RollDice(int faces)
        {
            if (faces <= 0) return 0;
            return Dice.Next(1, faces + 1);
        }
    }
}
