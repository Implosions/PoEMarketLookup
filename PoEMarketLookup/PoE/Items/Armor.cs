using PoEMarketLookup.PoE.Items.Components;
using System;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : IPoEItem
    {
        public Rarity Rarity { get; }
        public string Base { get; }
        public string Name { get; }
        public int Armour { get; }
        public int EvasionRating { get; }
        public int EnergyShield { get; }
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
        public int TalismanTier { get; }
        public bool Shaper { get; }
        public bool Elder { get; }
        public bool Synthesised { get; }
        public bool Mirrored { get; }

        public Armor(PoEItemBuilder builder)
        {
            Rarity = builder.Rarity;
            Base = builder.Base;
            Name = builder.Name;
            Armour = builder.Armour;
            EvasionRating = builder.EvasionRating;
            EnergyShield = builder.EnergyShield;
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
            TalismanTier = builder.TalismanTier;
            Shaper = builder.Shaper;
            Elder = builder.Elder;
            Synthesised = builder.Synthesised;
            Mirrored = builder.Mirrored;
        }
    }
}
