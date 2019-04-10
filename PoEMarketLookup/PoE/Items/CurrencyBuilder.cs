namespace PoEMarketLookup.PoE.Items
{
    public class CurrencyBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Currency(this);
        }
    }
}
