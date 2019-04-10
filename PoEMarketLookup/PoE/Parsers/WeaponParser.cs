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
                itemBuilder.PhysicalDamage.BottomEnd = int.Parse(dmg[0]);
                itemBuilder.PhysicalDamage.TopEnd = int.Parse(dmg[1]);
            }
        }
    }
}
