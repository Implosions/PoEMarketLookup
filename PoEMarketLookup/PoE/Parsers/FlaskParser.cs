using PoEMarketLookup.PoE.Items;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public class FlaskParser : ModdableItemParser<Flask>
    {
        private static readonly Regex _reChargeInfo =
            new Regex(@"Consumes (\d+)(?: \(augmented\))? of (\d+) Charges on use");

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
            var match = _reChargeInfo.Match(itemSections[1]);

            item.MaxCharges = int.Parse(match.Groups[2].Value);
            item.ChargesConsumedOnUse = int.Parse(match.Groups[1].Value);
        }

        protected override int GetPossibleModsSectionsCount(int index)
        {
            return base.GetPossibleModsSectionsCount(index) - 1;
        }
    }
}
