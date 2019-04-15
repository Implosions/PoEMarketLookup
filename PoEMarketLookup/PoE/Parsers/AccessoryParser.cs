using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class AccessoryParser : ModdableItemParser<Accessory>
    {
        public AccessoryParser(string rawItemText) : base(rawItemText)
        {
            item = new Accessory();
        }

        public override Accessory Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseTalismanTier();

            return item;
        }

        private void ParseTalismanTier()
        {
            if (itemFields.ContainsKey("Talisman Tier"))
            {
                var tier = itemFields["Talisman Tier"];
                item.TalismanTier = int.Parse(tier);
            }
        }
    }
}
