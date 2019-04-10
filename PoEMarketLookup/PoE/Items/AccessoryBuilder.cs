namespace PoEMarketLookup.PoE.Items
{
    public class AccessoryBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Accessory(this);
        }
    }
}
