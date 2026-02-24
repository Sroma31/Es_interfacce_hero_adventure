using RpgGame.Characters;

namespace RpgGame.Items
{
    public interface ILootItem
    {
        string Name { get; }
        int Value { get; }
        void ApplyEffect(Character target);
    }
}
