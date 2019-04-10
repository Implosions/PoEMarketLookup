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
        public Rarity Rarity { get; }
        public string Base { get; }

        public PoEItem(PoEItemBuilder builder)
        {
            Rarity = builder.Rarity;
            Base = builder.Base;
        }
    }
}
