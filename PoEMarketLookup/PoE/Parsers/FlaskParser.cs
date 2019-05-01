using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class FlaskParser : ModdableItemParser<Flask>
    {
        public FlaskParser(string rawItemText) : base(rawItemText)
        {
            item = new Flask();
        }

        public override Flask Parse()
        {
            ParseInfoSection();

            return item;
        }
    }
}
