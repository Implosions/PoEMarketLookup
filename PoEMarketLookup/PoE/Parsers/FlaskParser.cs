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
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            var match = _reChargeInfo.Match(_itemSections[1]);

            _item.MaxCharges = int.Parse(match.Groups[2].Value);
            _item.ChargesConsumedOnUse = int.Parse(match.Groups[1].Value);
        }

        protected override int GetPossibleModsSectionsCount(int index)
        {
            return base.GetPossibleModsSectionsCount(index) - 1;
        }
    }
}
