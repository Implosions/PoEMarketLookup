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
    }
}
