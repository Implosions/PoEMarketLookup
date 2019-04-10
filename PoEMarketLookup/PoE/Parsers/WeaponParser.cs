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
    }
}
