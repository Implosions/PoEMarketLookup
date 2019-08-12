using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class MapParser : ModdableItemParser<Map>
    {
        public MapParser(string rawItem) : base(rawItem)
        {
            _item = new Map();
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            if (_itemFields.ContainsKey("Map Tier"))
            {
                _item.Tier = int.Parse(_itemFields["Map Tier"]);
            }

            if (_itemFields.ContainsKey("Item Quantity"))
            {
                _item.Quantity = GetIntValueFromMapModString(_itemFields["Item Quantity"]);
            }

            if (_itemFields.ContainsKey("Item Rarity"))
            {
                _item.ItemRarity = GetIntValueFromMapModString(_itemFields["Item Rarity"]);
            }

            if (_itemFields.ContainsKey("Monster Pack Size"))
            {
                _item.PackSize = GetIntValueFromMapModString(_itemFields["Monster Pack Size"]);
            }
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
