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

            return item;
        }
    }
}
