using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class ModdableItem : PoEItem
    {
        public string Name { get; set; }
        public Rarity Rarity { get; set; }
        public int Quality { get; set; }
        public int LevelRequirement { get; set; }
        public int StrengthRequirement { get; set; }
        public int DexterityRequirement { get; set; }
        public int IntelligenceRequirement { get; set; }
        public SocketGroup Sockets { get; set; }
        public int ItemLevel { get; set; }
        public Mod[] ImplicitMods { get; set; }
        public Mod[] ExplicitMods { get; set; }
        public bool Corrupted { get; set; }
        public bool Shaper { get; set; }
        public bool Elder { get; set; }
        public bool Synthesised { get; set; }
        public bool Fractured { get; set; }
        public bool Mirrored { get; set; }
        public int TotalLife { get; set; }
        public int ColdResistance { get; set; }
        public int FireResistance { get; set; }
        public int LightningResistance { get; set; }
        public int ChaosResistance { get; set; }
    }
}
