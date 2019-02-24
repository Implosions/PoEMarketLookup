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
        public int Intelligencerequirement { get; }

        public Armor(string baseItem, int armour, int evasion, int es, int quality, int reqLevel,
                    int reqStr, int reqDex, int reqInt)
        {
            Base = baseItem;
            Armour = armour;
            EvasionRating = evasion;
            EnergyShield = es;
            Quality = quality;
            LevelRequirement = reqLevel;
            StrengthRequirement = reqStr;
            DexterityRequirement = reqDex;
            Intelligencerequirement = reqInt;
        }
    }
}
