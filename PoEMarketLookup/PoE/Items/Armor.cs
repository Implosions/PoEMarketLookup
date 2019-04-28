using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;

namespace PoEMarketLookup.PoE.Items
{
    public class Armor : ModdableItem
    {
        public int Armour { get; set; }
        public int EvasionRating { get; set; }
        public int EnergyShield { get; set; }

        private delegate bool IsDefenseModifier(string mod);

        public int GetNormalizedArmourValue()
        {
            var incArmour = GetTotalIncreasedDefense(ArmorDefenseMods.DefenseStat.Armour);

            return NormalizeDefenseValue(Armour, incArmour);
        }

        public int GetNormalizedEvasionValue()
        {
            var incEvasion = GetTotalIncreasedDefense(ArmorDefenseMods.DefenseStat.Evasion);

            return NormalizeDefenseValue(EvasionRating, incEvasion);
        }

        public int GetNormalizedEnergyShieldValue()
        {
            var incES = GetTotalIncreasedDefense(ArmorDefenseMods.DefenseStat.EnergyShield);

            return NormalizeDefenseValue(EnergyShield, incES);
        }

        private int GetTotalIncreasedDefense(ArmorDefenseMods.DefenseStat stat)
        {
            if (ExplicitMods == null)
            {
                return 0;
            }
            
            int totalIncreased = 0;

            IsDefenseModifier IsValid = null;

            switch (stat)
            {
                case ArmorDefenseMods.DefenseStat.Armour: IsValid = ArmorDefenseMods.ArmourModifiers.Contains; break;
                case ArmorDefenseMods.DefenseStat.Evasion: IsValid = ArmorDefenseMods.EvasionModifiers.Contains; break;
                case ArmorDefenseMods.DefenseStat.EnergyShield: IsValid = ArmorDefenseMods.EnergyShieldModifiers.Contains; break;
            }
            
            foreach (Mod mod in ExplicitMods)
            {
                if (IsValid(mod.Affix))
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
            public enum DefenseStat
            {
                Armour,
                Evasion,
                EnergyShield
            }

            public static string AR => "#% Increased Armour";
            public static string EV => "#% Increased Evasion Rating";
            public static string ES => "#% Increased Energy Shield";
            public static string AR_EV => "#% Increased Armour and Evasion Rating";
            public static string AR_ES => "#% Increased Armour and Energy Shield";
            public static string EV_ES => "#% Increased Evasion Rating and Energy Shield";
            public static string AR_EV_ES => "#% Increased Armour, Evasion and Energy Shield";

            public static ISet<string> ArmourModifiers = new HashSet<string>()
            {
                AR,
                AR_EV,
                AR_ES,
                AR_EV_ES
            };

            public static ISet<string> EvasionModifiers = new HashSet<string>()
            {
                EV,
                EV_ES,
                AR_EV,
                AR_EV_ES
            };

            public static ISet<string> EnergyShieldModifiers = new HashSet<string>()
            {
                ES,
                AR_ES,
                EV_ES,
                AR_EV_ES
            };

        }
    }
}
