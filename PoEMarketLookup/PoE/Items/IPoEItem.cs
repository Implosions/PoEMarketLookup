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

    public interface IPoEItem
    {
        Rarity Rarity { get; }
        string Base { get; }
    }
}
