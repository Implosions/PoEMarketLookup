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
            ParseModdableItemSections();
            ParseFlaskInfo();

            return item;
        }

        private void ParseFlaskInfo()
        {
            var lines = Utils.SplitItemSection(itemSections[1]);

            var startIndex = lines[1].IndexOf(" of ") + 4;
            var endIndex = lines[1].IndexOf(" Charges");
            var maxCharges = lines[1].Substring(startIndex, endIndex - startIndex);

            item.MaxCharges = int.Parse(maxCharges);
        }
    }
}
