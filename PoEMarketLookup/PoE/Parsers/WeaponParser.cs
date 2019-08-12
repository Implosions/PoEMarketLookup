using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class WeaponParser : ModdableItemParser<Weapon>
    {
        public WeaponParser(
            string rawItemText,
            PoEItemType itemCategory = (PoEItemType)200
            ) : base(rawItemText)
        {
            _item = new Weapon()
            {
                Category = itemCategory
            };
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            ParseWeaponType();
            ParsePhysicalDamage();
            ParseChaosDamage();
            ParseElementalDamage();
            ParseLocalCrit();
            ParseAPS();
            ParseWeaponRange();
        }

        private void ParseWeaponType()
        {
            string stats = _itemSections[1];
            _item.Type = stats.Substring(0, stats.IndexOf('\r'));
        }

        private void ParsePhysicalDamage()
        {
            if (_itemFields.ContainsKey("Physical Damage"))
            {
                var dmg = _itemFields["Physical Damage"].Split('-');
                _item.PhysicalDamage.BottomEnd = int.Parse(dmg[0]);
                _item.PhysicalDamage.TopEnd = int.Parse(dmg[1]);
            }
        }

        private void ParseChaosDamage()
        {
            if (_itemFields.ContainsKey("Chaos Damage"))
            {
                var dmg = _itemFields["Chaos Damage"].Split('-');

                _item.ChaosDamage.BottomEnd = int.Parse(dmg[0]);
                _item.ChaosDamage.TopEnd = int.Parse(dmg[1]);
            }
        }

        private void ParseElementalDamage()
        {
            if (!_itemFields.ContainsKey("Elemental Damage"))
            {
                return;
            }

            if(_item.ExplicitMods != null)
            {
                AddElementalDamage(_item.ExplicitMods);
            }

            if(_item.ImplicitMods != null)
            {
                AddElementalDamage(_item.ImplicitMods);
            }
            
        }

        private void ParseLocalCrit()
        {
            if(_itemFields.ContainsKey("Critical Strike Chance"))
            {
                string val = _itemFields["Critical Strike Chance"];
                val = val.Substring(0, val.Length - 1);

                _item.CriticalStrikeChance = double.Parse(val);
            }
        }

        private void ParseAPS()
        {
            if(_itemFields.ContainsKey("Attacks per Second"))
            {
                _item.AttacksPerSecond = double.Parse(_itemFields["Attacks per Second"]);
            }
        }

        private void ParseWeaponRange()
        {
            if(_itemFields.ContainsKey("Weapon Range"))
            {
                _item.WeaponRange = int.Parse(_itemFields["Weapon Range"]);
            }
        }

        private void AddElementalDamage(Mod[] affixes)
        {
            foreach (var mod in affixes)
            {
                switch (mod.Affix)
                {
                    case "Adds # to # Fire Damage":
                        _item.FireDamage.BottomEnd += (int)mod.AffixValues[0];
                        _item.FireDamage.TopEnd += (int)mod.AffixValues[1];
                        break;

                    case "Adds # to # Cold Damage":
                        _item.ColdDamage.BottomEnd += (int)mod.AffixValues[0];
                        _item.ColdDamage.TopEnd += (int)mod.AffixValues[1];
                        break;

                    case "Adds # to # Lightning Damage":
                        _item.LightningDamage.BottomEnd += (int)mod.AffixValues[0];
                        _item.LightningDamage.TopEnd += (int)mod.AffixValues[1];
                        break;
                }
            }
        }
    }
}
