using PoEMarketLookup.PoE.Items.Components;
using System;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; }
        public int EvasionRating { get; }
        public int EnergyShield { get; }

        public Armor(ArmorBuilder builder) : base(builder)
        {
            Armour = builder.Armour;
            EvasionRating = builder.EvasionRating;
            EnergyShield = builder.EnergyShield;
        }
    }
}
