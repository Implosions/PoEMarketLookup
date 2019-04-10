using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items.Builders
{
    public class WeaponBuilder : ModdableItemBuilder
    {
        public string Type;
        public DamageRange PhysicalDamage;
        public DamageRange ChaosDamage;
        public DamageRange FireDamage;
        public DamageRange ColdDamage;
        public DamageRange LightningDamage;
        public double CriticalStrikeChance;
        public double AttacksPerSecond;
        public int WeaponRange;

        public WeaponBuilder()
        {
            PhysicalDamage = new DamageRange();
            ChaosDamage = new DamageRange();
            FireDamage = new DamageRange();
            ColdDamage = new DamageRange();
            LightningDamage = new DamageRange();
        }

        public PoEItemBuilder SetType(string type)
        {
            Type = type;

            return this;
        }

        public PoEItemBuilder SetPhysicalDamage(int bottom, int top)
        {
            PhysicalDamage.BottomEnd = bottom;
            PhysicalDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetChaosDamage(int bottom, int top)
        {
            ChaosDamage.BottomEnd = bottom;
            ChaosDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetFireDamage(int bottom, int top)
        {
            FireDamage.BottomEnd = bottom;
            FireDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetColdDamage(int bottom, int top)
        {
            ColdDamage.BottomEnd = bottom;
            ColdDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetLightningDamage(int bottom, int top)
        {
            LightningDamage.BottomEnd = bottom;
            LightningDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetCritChance(double crit)
        {
            CriticalStrikeChance = crit;

            return this;
        }

        public PoEItemBuilder SetAttacksPerSecond(double aps)
        {
            AttacksPerSecond = aps;

            return this;
        }

        public PoEItemBuilder SetWeaponRange(int range)
        {
            WeaponRange = range;

            return this;
        }

        public override PoEItem Build()
        {
            return new Weapon(this);
        }
    }
}
