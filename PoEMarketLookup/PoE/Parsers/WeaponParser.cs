﻿using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class WeaponParser : ModdableItemParser<Weapon>
    {
        public WeaponParser(string rawItemText) : base(rawItemText)
        {
            item = new Weapon();
        }

        public WeaponParser(string rawItemText, PoEItemType itemCategory) 
            : this(rawItemText)
        {
            item.Category = itemCategory;
        }

        public override Weapon Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseWeaponType();
            ParsePhysicalDamage();
            ParseChaosDamage();
            ParseElementalDamage();
            ParseLocalCrit();
            ParseAPS();
            ParseWeaponRange();

            return item;
        }

        private void ParseWeaponType()
        {
            string stats = itemSections[1];
            item.Type = stats.Substring(0, stats.IndexOf('\r'));
        }

        private void ParsePhysicalDamage()
        {
            if (itemFields.ContainsKey("Physical Damage"))
            {
                var dmg = itemFields["Physical Damage"].Split('-');
                item.PhysicalDamage.BottomEnd = int.Parse(dmg[0]);
                item.PhysicalDamage.TopEnd = int.Parse(dmg[1]);
            }
        }

        private void ParseChaosDamage()
        {
            if (itemFields.ContainsKey("Chaos Damage"))
            {
                var dmg = itemFields["Chaos Damage"].Split('-');

                item.ChaosDamage.BottomEnd = int.Parse(dmg[0]);
                item.ChaosDamage.TopEnd = int.Parse(dmg[1]);
            }
        }

        private void ParseElementalDamage()
        {
            if (!itemFields.ContainsKey("Elemental Damage"))
            {
                return;
            }

            if(item.ExplicitMods != null)
            {
                AddElementalDamage(item.ExplicitMods);
            }

            if(item.ImplicitMods != null)
            {
                AddElementalDamage(item.ImplicitMods);
            }
            
        }

        private void ParseLocalCrit()
        {
            if(itemFields.ContainsKey("Critical Strike Chance"))
            {
                string val = itemFields["Critical Strike Chance"];
                val = val.Substring(0, val.Length - 1);

                item.CriticalStrikeChance = double.Parse(val);
            }
        }

        private void ParseAPS()
        {
            if(itemFields.ContainsKey("Attacks per Second"))
            {
                item.AttacksPerSecond = double.Parse(itemFields["Attacks per Second"]);
            }
        }

        private void ParseWeaponRange()
        {
            if(itemFields.ContainsKey("Weapon Range"))
            {
                item.WeaponRange = int.Parse(itemFields["Weapon Range"]);
            }
        }

        private void AddElementalDamage(Mod[] affixes)
        {
            foreach (var mod in affixes)
            {
                switch (mod.Affix)
                {
                    case "Adds # to # Fire Damage":
                        item.FireDamage.BottomEnd += (int)mod.AffixValues[0];
                        item.FireDamage.TopEnd += (int)mod.AffixValues[1];
                        break;

                    case "Adds # to # Cold Damage":
                        item.ColdDamage.BottomEnd += (int)mod.AffixValues[0];
                        item.ColdDamage.TopEnd += (int)mod.AffixValues[1];
                        break;

                    case "Adds # to # Lightning Damage":
                        item.LightningDamage.BottomEnd += (int)mod.AffixValues[0];
                        item.LightningDamage.TopEnd += (int)mod.AffixValues[1];
                        break;
                }
            }
        }
    }
}
