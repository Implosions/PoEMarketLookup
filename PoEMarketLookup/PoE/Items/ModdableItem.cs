using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class ModdableItem : PoEItem
    {
        public string Name { get; }
        public int Quality { get; }
        public int LevelRequirement { get; }
        public int StrengthRequirement { get; }
        public int DexterityRequirement { get; }
        public int IntelligenceRequirement { get; }
        public SocketGroup Sockets { get; }
        public int ItemLevel { get; }
        public Mod[] ImplicitMods { get; }
        public Mod[] ExplicitMods { get; }
        public bool Corrupted { get; }
        public bool Shaper { get; }
        public bool Elder { get; }
        public bool Synthesised { get; }
        public bool Mirrored { get; }
        public Mod Enchantment { get; }

        public ModdableItem(PoEItemBuilder builder) : base(builder)
        {
            Quality = builder.Quality;
            LevelRequirement = builder.LevelRequirement;
            StrengthRequirement = builder.StrengthRequirement;
            DexterityRequirement = builder.DexterityRequirement;
            IntelligenceRequirement = builder.IntelligenceRequirement;
            Sockets = builder.Sockets;
            ItemLevel = builder.ItemLevel;
            ImplicitMods = builder.ImplicitMods;
            ExplicitMods = builder.ExplicitMods;
            Corrupted = builder.Corrupted;
            Shaper = builder.Shaper;
            Elder = builder.Elder;
            Synthesised = builder.Synthesised;
            Mirrored = builder.Mirrored;
            Enchantment = builder.Enchantment;
        }
    }
}
