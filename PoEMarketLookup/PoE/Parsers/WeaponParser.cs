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

            return itemBuilder.Build();
        }
    }
}
