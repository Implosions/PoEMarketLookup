using PoEMarketLookup.PoE.Items.Components;
using System;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : IPoEItem
    {
        public string Rarity { get; }
        public string Base { get; }
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

        public Armor(PoEItemBuilder builder)
        {
            Base = builder.Base;
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
        }
    }
}
