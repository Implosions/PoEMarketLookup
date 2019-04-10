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

            return itemBuilder.Build();
        }

        private void ParseWeaponType()
        {
            string stats = itemSections[1];
            string type = stats.Substring(0, stats.IndexOf('\r'));

            itemBuilder.SetType(type);
        }
    }
}
