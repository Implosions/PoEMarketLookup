using PoEMarketLookup.PoE.Items.Components;

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
