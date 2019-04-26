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

        public int GetPhysicalDPS()
        {
            return CalculateDPS(PhysicalDamage.Combined);
        }

        public int GetElementalDPS()
        {
            int totalCombined =
                FireDamage.Combined +
                ColdDamage.Combined +
                LightningDamage.Combined;

            return CalculateDPS(totalCombined);
        }

        public int GetTotalDPS()
        {
            int totalCombined =
                PhysicalDamage.Combined +
                ChaosDamage.Combined +
                FireDamage.Combined +
                ColdDamage.Combined +
                LightningDamage.Combined;

            return CalculateDPS(totalCombined);
        }

        public DamageRange GetNormalizedPhysicalDamage()
        {
            DamageRange normalizedPD = new DamageRange()
            {
                BottomEnd = PhysicalDamage.BottomEnd,
                TopEnd = PhysicalDamage.TopEnd
            };

            normalizedPD.BottomEnd = (int)(normalizedPD.BottomEnd * 1.2);
            normalizedPD.TopEnd = (int)(normalizedPD.TopEnd * 1.2);

            return normalizedPD;
        }

        private int CalculateDPS(int combinedDamage)
        {
            return (int)(((double)combinedDamage / 2) * AttacksPerSecond);
        }
    }
}
