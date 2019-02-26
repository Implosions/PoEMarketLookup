namespace PoEMarketLookup.PoE.Items
{
    public class Currency : IPoEItem
    {
        public Rarity Rarity { get; }
        public string Base { get; }
        public int StackSize { get; }

        public Currency(PoEItemBuilder builder)
        {
            Base = builder.Base;
            StackSize = builder.StackSize;
        }
    }
}
