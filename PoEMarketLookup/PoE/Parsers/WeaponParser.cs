using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class WeaponParser : ModdableItemParser
    {
        public WeaponParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new WeaponBuilder();
        }

        public override PoEItem Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseWeaponType();
            ParsePhysicalDamage();
            ParseChaosDamage();
            ParseElementalDamage();
            ParseLocalCrit();
            ParseAPS();

            return itemBuilder.Build();
        }

        private void ParseWeaponType()
        {
            string stats = itemSections[1];
            string type = stats.Substring(0, stats.IndexOf('\r'));

            itemBuilder.SetType(type);
        }

        private void ParsePhysicalDamage()
        {
            if(itemFieldsDict.ContainsKey("Physical Damage"))
            {
                var dmg = itemFieldsDict["Physical Damage"].Split('-');
                int bottom = int.Parse(dmg[0]);
                int top = int.Parse(dmg[1]);

                itemBuilder.SetPhysicalDamage(bottom, top);
            }
        }

        private void ParseChaosDamage()
        {
            if (itemFieldsDict.ContainsKey("Chaos Damage"))
            {
                var dmg = itemFieldsDict["Chaos Damage"].Split('-');

                int bottom = int.Parse(dmg[0]);
                int top = int.Parse(dmg[1]);

                itemBuilder.SetChaosDamage(bottom, top);
            }
        }

        private void ParseElementalDamage()
        {
            if (!itemFieldsDict.ContainsKey("Elemental Damage"))
            {
                return;
            }

            foreach (var mod in itemBuilder.ExplicitMods)
            {
                switch (mod.Affix)
                {
                    case "Adds # to # Fire Damage":
                        itemBuilder.SetFireDamage(mod.AffixValues[0], mod.AffixValues[1]); break;

                    case "Adds # to # Cold Damage":
                        itemBuilder.SetColdDamage(mod.AffixValues[0], mod.AffixValues[1]); break;

                    case "Adds # to # Lightning Damage":
                        itemBuilder.SetLightningDamage(mod.AffixValues[0], mod.AffixValues[1]); break;
                }
            }
        }

        private void ParseLocalCrit()
        {
            if(itemFieldsDict.ContainsKey("Critical Strike Chance"))
            {
                string val = itemFieldsDict["Critical Strike Chance"];
                val = val.Substring(0, val.Length - 1);

                itemBuilder.SetCritChance(double.Parse(val));
            }
        }

        private void ParseAPS()
        {
            if(itemFieldsDict.ContainsKey("Attacks per Second"))
            {
                var aps = double.Parse(itemFieldsDict["Attacks per Second"]);

                itemBuilder.SetAttacksPerSecond(aps);
            }
        }
    }
}
