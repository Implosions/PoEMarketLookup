using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class EnchantableItem : ModdableItem
    {
        public Mod[] Enchantments { get; set; }
    }
}
