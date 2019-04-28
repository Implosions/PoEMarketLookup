namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        public int GetNormalizedArmourValue()
        {
            return NormalizeDefenseValue(Armour);
        }

        public int GetNormalizedEvasionValue()
        {
            return NormalizeDefenseValue(EvasionRating);
        }

        public int GetNormalizedEnergyShieldValue()
        {
            return NormalizeDefenseValue(EnergyShield);
        }

        private int NormalizeDefenseValue(int val)
        {
            float increasedFromQuality = 1 + (Quality / 100f);
            return (int)((val / increasedFromQuality) * 1.2f);
        }
    }
}
