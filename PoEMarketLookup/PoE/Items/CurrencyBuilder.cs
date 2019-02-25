namespace PoEMarketLookup.PoE.Items
{
    public class CurrencyBuilder : PoEItemBuilder
    {
        public override IPoEItem Build()
        {
            return new Currency(this);
        }
    }
}
