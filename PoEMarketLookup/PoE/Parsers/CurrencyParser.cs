using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class CurrencyParser : PoEItemParser<Currency>
    {
        public CurrencyParser(string rawItemText) : base(rawItemText)
        {
            item = new Currency();
        }

        public override Currency Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return item;
        }

        private void ParseCurrencyData()
        {
            string stackVal = itemFields["Stack Size"];
            stackVal = stackVal.Substring(0, stackVal.IndexOf('/'));

            item.StackSize = int.Parse(stackVal);
        }
    }
}
