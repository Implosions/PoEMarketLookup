using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public class Weapon : ModdableItem
    {
        public string Type { get; }
        public DamageRange PhysicalDamage { get; }
        public DamageRange ChaosDamage { get; }
        public DamageRange FireDamage { get; }
        public DamageRange ColdDamage { get; }
        public DamageRange LightningDamage { get; }
        public double CriticalStrikeChance { get; }
        public double AttacksPerSecond { get; }

        public Weapon(WeaponBuilder builder) : base(builder)
        {
            Type = builder.Type;
            PhysicalDamage = builder.PhysicalDamage;
            ChaosDamage = builder.ChaosDamage;
            FireDamage = builder.FireDamage;
            ColdDamage = builder.ColdDamage;
            LightningDamage = builder.LightningDamage;
            CriticalStrikeChance = builder.CriticalStrikeChance;
            AttacksPerSecond = builder.AttacksPerSecond;
        }
    }
}
