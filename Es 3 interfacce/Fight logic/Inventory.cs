using System;
using System.Collections.Generic;
using RpgGame.Items;
using RpgGame.Logic;

namespace RpgGame.Systems
{
    /// <summary>
    /// Manages a collection of loot items. 
    /// Follows SRP by separating item storage/management from the character's mental/physical state.
    /// </summary>
    public class Inventory
    {
        private List<ILootItem> _items = new List<ILootItem>();
        public IReadOnlyList<ILootItem> Items => _items.AsReadOnly();

        public void Add(ILootItem item)
        {
            if (item != null)
            {
                _items.Add(item);
            }
        }

        public void AddRange(IEnumerable<ILootItem> items)
        {
            if (items != null)
            {
                _items.AddRange(items);
            }
        }

        public void TransferFrom(Inventory other)
        {
            if (other != null && other._items.Count > 0)
            {
                AddRange(other._items);
                other.Clear();
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void PrintContents(string ownerName)
        {
            if (_items.Count > 0)
            {
                foreach (ILootItem item in _items)
                {
                    DisplayManager.PrintBattleLog($"[{ownerName}'s Inventory] Loot: {item.Name} (Value: {item.Value})", ConsoleColor.White);
                }
            }
            else
            {
                DisplayManager.PrintBattleLog($"[{ownerName}'s Inventory] Empty.", ConsoleColor.DarkGray);
            }
        }
    }
}
