namespace PoEMarketLookup.PoE.Items.Builders
{
    public class AccessoryBuilder : ModdableItemBuilder
    {
        public int TalismanTier;

        public AccessoryBuilder SetTalismanTier(int tier)
        {
            TalismanTier = tier;

            return this;
        }

        public override PoEItem Build()
        {
            return new Accessory(this);
        }
    }
}
