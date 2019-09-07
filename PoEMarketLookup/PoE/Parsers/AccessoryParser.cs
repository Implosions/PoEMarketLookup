using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class AccessoryParser : EnchantableItemParser<Accessory>
    {
        public AccessoryParser(
            string rawItemText,
            PoEItemType itemCategory = (PoEItemType)300) : base(rawItemText, itemCategory)
        {
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            if (_itemFields.ContainsKey("Talisman Tier"))
            {
                var tier = _itemFields["Talisman Tier"];
                _item.TalismanTier = int.Parse(tier);
            }
        }

        protected override int GetPossibleModsSectionsCount(int index)
        {
            int remaining = base.GetPossibleModsSectionsCount(index);

            if(_itemFields.ContainsKey("Talisman Tier"))
            {
                remaining--;
            }

            return remaining;
        }

        protected override int GetModsStartIndex()
        {
            int index = base.GetModsStartIndex();

            if (_itemFields.ContainsKey("Talisman Tier"))
            {
                index++;
            }

            return index;
        }

        protected override bool CheckPossibleModSection(string itemSection)
        {
            if(itemSection.StartsWith("Place into"))
            {
                return false;
            }
            else
            {
                return base.CheckPossibleModSection(itemSection);
            }
        }
    }
}
