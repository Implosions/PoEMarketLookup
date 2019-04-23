using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public enum Rarity
    {
        Unknown,
        Normal,
        Magic,
        Rare,
        Unique,
        Currency,
        Gem
    }

    public abstract class PoEItem
    {
        public PoEItemType Category { get; set; }
        public string Base { get; set; }
        public string Name { get; set; }
    }
}
