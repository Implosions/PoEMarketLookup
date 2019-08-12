using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class QualitableItemParser<T> : PoEItemParser<T> 
        where T : QualitableItem
    {
        public QualitableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        protected void ParseItemQuality()
        {
            if (!itemFields.ContainsKey("Quality"))
            {
                return;
            }

            string qualVal = itemFields["Quality"];
            qualVal = qualVal.Substring(1, qualVal.Length - 2);
            item.Quality = int.Parse(qualVal);
        }
    }
}
