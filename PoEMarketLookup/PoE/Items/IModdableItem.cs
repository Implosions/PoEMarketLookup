using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public interface IModdableItem : IPoEItem
    {
        string Name { get; }
        int Quality { get; }
        int LevelRequirement { get; }
        int StrengthRequirement { get; }
        int DexterityRequirement { get; }
        int IntelligenceRequirement { get; }
        SocketGroup Sockets { get; }
        int ItemLevel { get; }
        Mod[] ImplicitMods { get; }
        Mod[] ExplicitMods { get; }
        bool Corrupted { get; }
        bool Shaper { get; }
        bool Elder { get; }
        bool Synthesised { get; }
        bool Mirrored { get; }
        Mod Enchantment { get; }
    }
}
