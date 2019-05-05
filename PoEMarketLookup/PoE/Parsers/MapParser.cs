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

            if (itemFields.ContainsKey("Map Tier"))
            {
                item.Tier = int.Parse(itemFields["Map Tier"]);
            }

            if (itemFields.ContainsKey("Item Quantity"))
            {
                var quant = itemFields["Item Quantity"];
                quant = quant.Substring(1, quant.IndexOf('%') - 1);

                item.Quantity = int.Parse(quant);
            }

            return item;
        }
    }
}
