using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class MapParser : ModdableItemParser<Map>
    {
        public MapParser(string rawItem) : base(rawItem)
        {
            item = new Map();
        }

        public override Map Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseMapTier();

            return item;
        }

        private void ParseMapTier()
        {
            if (itemFields.ContainsKey("Map Tier"))
            {
                item.Tier = int.Parse(itemFields["Map Tier"]);
            }
        }
    }
}
