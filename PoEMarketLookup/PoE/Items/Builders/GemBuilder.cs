namespace PoEMarketLookup.PoE.Items.Builders
{
    public class GemBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Gem(this);
        }
    }
}
