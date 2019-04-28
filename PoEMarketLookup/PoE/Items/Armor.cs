using PoEMarketLookup.PoE.Items.Components;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        private static Regex _reIncreasedArmour = new Regex(@"^#% Increased Armour\b");

        public int GetNormalizedArmourValue()
        {
            int totalIncreased = 0;

            if(ExplicitMods == null)
            {
                return NormalizeDefenseValue(Armour);
            }

            foreach(Mod mod in ExplicitMods)
            {
                if (_reIncreasedArmour.IsMatch(mod.Affix))
                {
                    totalIncreased += mod.AffixValues[0];
                }
            }

            return NormalizeDefenseValue(Armour, totalIncreased);
        }

        public int GetNormalizedEvasionValue()
        {
            return NormalizeDefenseValue(EvasionRating);
        }

        public int GetNormalizedEnergyShieldValue()
        {
            return NormalizeDefenseValue(EnergyShield);
        }

        private int NormalizeDefenseValue(int val, int increasedDefense = 0)
        {
            if(Quality >= 20)
            {
                return val;
            }

            float modIncreased = increasedDefense / 100f;
            float increasedFromQuality = Quality / 100f;
            return (int)((val / (1 + increasedFromQuality + modIncreased)) * (1.2f + modIncreased));
        }
    }
}
