using PoEMarketLookup.PoE.Items;
using System;

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
            if(!itemFields.ContainsKey("Stack Size"))
            {
                throw new FormatException("Missing Stack Size field");
            }

            string stackVal = itemFields["Stack Size"];
            stackVal = stackVal.Substring(0, stackVal.IndexOf('/'));

            item.StackSize = int.Parse(stackVal);
        }
    }
}
