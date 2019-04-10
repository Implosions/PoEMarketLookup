﻿using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public class WeaponBuilder : PoEItemBuilder
    {
        public WeaponBuilder()
        {
            PhysicalDamage = new DamageRange();
            ChaosDamage = new DamageRange();
            FireDamage = new DamageRange();
            ColdDamage = new DamageRange();
        }

        public override PoEItem Build()
        {
            return new Weapon(this);
        }
    }
}
