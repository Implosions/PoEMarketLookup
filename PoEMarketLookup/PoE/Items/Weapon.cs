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

        public Weapon(WeaponBuilder builder) : base(builder)
        {
            Type = builder.Type;
            PhysicalDamage = builder.PhysicalDamage;
            ChaosDamage = builder.ChaosDamage;
            FireDamage = builder.FireDamage;
            ColdDamage = builder.ColdDamage;
        }
    }
}
