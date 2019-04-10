using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Items
{
    public class Currency : PoEItem
    {
        public int StackSize { get; }

        public Currency(CurrencyBuilder builder) : base(builder)
        {
            StackSize = builder.StackSize;
        }
    }
}
