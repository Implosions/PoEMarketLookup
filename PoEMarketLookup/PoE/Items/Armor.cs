namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        public int GetNormalizedArmourValue()
        {
            float ar = Armour * 1.2f;

            return (int)ar;
        }

        public int GetNormalizedEvasionValue()
        {
            float ev = EvasionRating * 1.2f;

            return (int)ev;
        }

        public int GetNormalizedEnergyShieldValue()
        {
            float es = EnergyShield * 1.2f;

            return (int)es;
        }
    }
}
