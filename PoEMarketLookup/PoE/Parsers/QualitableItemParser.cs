using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class QualitableItemParser<T> : PoEItemParser<T> 
        where T : QualitableItem, new()
    {
        public QualitableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        public QualitableItemParser(string rawItemText, PoEItemType type) : base(rawItemText, type)
        {
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            if (!_itemFields.ContainsKey("Quality"))
            {
                return;
            }

            string qualVal = _itemFields["Quality"];
            qualVal = qualVal.Substring(1, qualVal.Length - 2);
            _item.Quality = int.Parse(qualVal);
        }
    }
}
