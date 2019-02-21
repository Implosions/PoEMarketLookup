namespace PoEMarketLookup
{
    public class Currency : PoEItem
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
