namespace PoEMarketLookup.PoE.Items
{
    public class Currency : IPoEItem
    {
        public string Rarity { get; }
        public string Base { get; }
        public int StackSize { get; }

        public Currency(string baseItem, int stackSize)
        {
            Base = baseItem;
            StackSize = stackSize;
        }
    }
}
