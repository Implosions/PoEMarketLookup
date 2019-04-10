namespace PoEMarketLookup.PoE.Items
{
    class AccessoryBuilder : PoEItemBuilder
    {
        public override IPoEItem Build()
        {
            return new Accessory(this);
        }
    }
}
