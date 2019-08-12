using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class QualitableItemParser<T> : PoEItemParser<T> 
        where T : QualitableItem
    {
        public QualitableItemParser(string rawItemText) : base(rawItemText)
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
