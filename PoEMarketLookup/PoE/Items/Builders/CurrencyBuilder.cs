namespace PoEMarketLookup.PoE.Items.Builders
{
    public class CurrencyBuilder : PoEItemBuilder
    {
        public int StackSize;

        public CurrencyBuilder SetStackSize(int size)
        {
            StackSize = size;

            return this;
        }

        public override PoEItem Build()
        {
            return new Currency(this);
        }
    }
}
