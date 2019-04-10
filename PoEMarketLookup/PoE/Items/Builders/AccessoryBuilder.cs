namespace PoEMarketLookup.PoE.Items.Builders
{
    public class AccessoryBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Accessory(this);
        }
    }
}
