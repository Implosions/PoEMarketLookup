using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public class Weapon : ModdableItem
    {
        public string Type { get; set; }
        public DamageRange PhysicalDamage { get; set; }
        public DamageRange ChaosDamage { get; set; }
        public DamageRange FireDamage { get; set; }
        public DamageRange ColdDamage { get; set; }
        public DamageRange LightningDamage { get; set; }
        public double CriticalStrikeChance { get; set; }
        public double AttacksPerSecond { get; set; }
        public int WeaponRange { get; set; }

        public int GetPhysicalDPS(bool normalizeQuality = false)
        {
            int dmg = normalizeQuality ?
                GetNormalizedPhysicalDamage().Combined : PhysicalDamage.Combined;
            return CalculateDPS(dmg);
        }

        public int GetElementalDPS()
        {
            int totalCombined =
                FireDamage.Combined +
                ColdDamage.Combined +
                LightningDamage.Combined;

            return CalculateDPS(totalCombined);
        }

        public int GetTotalDPS(bool normalizePhysicalDamage = false)
        {
            int totalCombined =
                ChaosDamage.Combined +
                FireDamage.Combined +
                ColdDamage.Combined +
                LightningDamage.Combined +
                (normalizePhysicalDamage 
                    ? GetNormalizedPhysicalDamage().Combined : PhysicalDamage.Combined);

            return CalculateDPS(totalCombined);
        }

        public DamageRange GetNormalizedPhysicalDamage()
        {
            if(Quality >= 20)
            {
                return PhysicalDamage;
            }

            float modIncreased = (GetTotalIncreasedPhysicalDamage(ExplicitMods) + GetTotalIncreasedPhysicalDamage(ImplicitMods)) / 100f;
            float qualIncreased = Quality / 100f;
            float bottomEnd = (PhysicalDamage.BottomEnd / (1f + qualIncreased + modIncreased)) * (1.2f + modIncreased);
            float topEnd = (PhysicalDamage.TopEnd / (1f + qualIncreased + modIncreased)) * (1.2f + modIncreased);

            return new DamageRange()
            {
                BottomEnd = (int)bottomEnd,
                TopEnd = (int)topEnd
            };
        }

        private int GetTotalIncreasedPhysicalDamage(Mod[] mods)
        {
            if(mods == null)
            {
                return 0;
            }

            int total = 0;

            foreach(Mod mod in mods)
            {
                if(mod.Affix.Equals("#% Increased Physical Damage"))
                {
                    total += (int)mod.AffixValues[0];
                }
            }

            return total;
        }

        private int CalculateDPS(int combinedDamage)
        {
            return (int)(((double)combinedDamage / 2) * AttacksPerSecond);
        }
    }
}
