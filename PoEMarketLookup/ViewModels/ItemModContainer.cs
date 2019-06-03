using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.ViewModels
{
    public class ItemModContainer : ItemField
    {
        public Mod Mod { get; }

        public ItemModContainer(Mod mod)
        {
            Mod = mod;
            Title = mod.ToString();
        }
    }
}
