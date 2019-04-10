using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Items
{
    public class Accessory : ModdableItem
    {
        public int TalismanTier { get; }

        public Accessory(AccessoryBuilder builder) : base(builder)
        {
            TalismanTier = builder.TalismanTier;
        }
    }
}
