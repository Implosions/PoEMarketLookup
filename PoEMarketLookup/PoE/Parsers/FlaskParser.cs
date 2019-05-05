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
            var chargeInfoIndex = itemFields.ContainsKey("Quality") ? 2 : 1;

            var endIndex = lines[chargeInfoIndex].IndexOf(" Charges");
            var charges = lines[chargeInfoIndex].Substring(9, endIndex - 9);
            var consumedCharges = charges.Substring(0, charges.IndexOf(' '));
            var maxCharges = charges.Substring(charges.LastIndexOf(' '));

            item.MaxCharges = int.Parse(maxCharges);
            item.ChargesConsumedOnUse = int.Parse(consumedCharges);
        }
    }
}
