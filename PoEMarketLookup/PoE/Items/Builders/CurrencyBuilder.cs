namespace PoEMarketLookup.PoE.Items.Builders
{
    public class CurrencyBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Currency(this);
        }
    }
}
