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
        public Rarity Rarity { get; set; }
        public string Base { get; set; }
        public string Name { get; set; }
    }
}
