namespace PoEMarketLookup.PoE.Items
{
    public class Currency : PoEItem
    {
        public int StackSize { get; set; }

        public Currency()
        {
            Category = Components.PoEItemType.Currency;
        }
    }
}
