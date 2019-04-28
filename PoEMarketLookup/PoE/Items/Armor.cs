using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        public int GetNormalizedArmourValue()
        {
            return NormalizeDefenseValue(Armour, GetTotalIncreasedDefense(ArmorDefenseMods.AR));
        }

        public int GetNormalizedEvasionValue()
        {
            return NormalizeDefenseValue(EvasionRating, GetTotalIncreasedDefense(ArmorDefenseMods.EV));
        }

        public int GetNormalizedEnergyShieldValue()
        {
            return NormalizeDefenseValue(EnergyShield, GetTotalIncreasedDefense(ArmorDefenseMods.ES));
        }

        private int GetTotalIncreasedDefense(string affix)
        {
            if (ExplicitMods == null)
            {
                return 0;
            }

            int totalIncreased = 0;
            
            foreach (Mod mod in ExplicitMods)
            {
                if (mod.Affix.Equals(affix))
                {
                    totalIncreased += mod.AffixValues[0];
                }
            }

            return totalIncreased;
        }

        private int NormalizeDefenseValue(int val, int increasedDefense)
        {
            if(Quality >= 20)
            {
                return val;
            }

            float modIncreased = increasedDefense / 100f;
            float increasedFromQuality = Quality / 100f;
            return (int)((val / (1 + increasedFromQuality + modIncreased)) * (1.2f + modIncreased));
        }

        private static class ArmorDefenseMods
        {
            public static string AR => "#% Increased Armour";
            public static string EV => "#% Increased Evasion Rating";
            public static string ES => "#% Increased Energy Shield";
        }
    }
}
