using PoEMarketLookup.PoE.Items.Components;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        private static Regex _reIncreasedArmour = new Regex(@"^#% Increased (Armour|Evasion Rating|Energy Shield)\b");

        public int GetNormalizedArmourValue()
        {
            return NormalizeDefenseValue(Armour, GetTotalIncreasedDefense());
        }

        public int GetNormalizedEvasionValue()
        {
            return NormalizeDefenseValue(EvasionRating, GetTotalIncreasedDefense());
        }

        public int GetNormalizedEnergyShieldValue()
        {
            return NormalizeDefenseValue(EnergyShield, GetTotalIncreasedDefense());
        }

        private int GetTotalIncreasedDefense()
        {
            if (ExplicitMods == null)
            {
                return 0;
            }

            int totalIncreased = 0;

            foreach (Mod mod in ExplicitMods)
            {
                if (_reIncreasedArmour.IsMatch(mod.Affix))
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
    }
}
