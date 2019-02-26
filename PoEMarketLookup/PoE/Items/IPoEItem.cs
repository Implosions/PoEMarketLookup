namespace PoEMarketLookup.PoE.Items
{
    public enum Rarity
    {
        Normal,
        Magic,
        Rare,
        Unique,
        Currency,
        Unknown
    }

    public interface IPoEItem
    {
        Rarity Rarity { get; }
        string Base { get; }
    }
}
