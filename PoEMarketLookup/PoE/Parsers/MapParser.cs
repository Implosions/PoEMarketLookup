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
                item.Quantity = GetIntValueFromMapModString(itemFields["Item Quantity"]);
            }

            if (itemFields.ContainsKey("Item Rarity"))
            {
                item.ItemRarity = GetIntValueFromMapModString(itemFields["Item Rarity"]);
            }

            if (itemFields.ContainsKey("Monster Pack Size"))
            {
                item.PackSize = GetIntValueFromMapModString(itemFields["Monster Pack Size"]);
            }

            return item;
        }

        private int GetIntValueFromMapModString(string val)
        {
            return int.Parse(val.Substring(1, val.IndexOf('%') - 1));
        }

        protected override int GetPossibleModsSectionsCount(int index)
        {
            return base.GetPossibleModsSectionsCount(index) - 1;
        }
    }
}
