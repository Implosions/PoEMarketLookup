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

        public Armor(string baseItem, int armour, int evasion, int es)
        {
            Base = baseItem;
            Armour = armour;
            EvasionRating = evasion;
            EnergyShield = es;
        }
    }
}
