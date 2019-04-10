using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Builders;
using System;

namespace PoEMarketLookup.PoE.Parsers
{
    public class CurrencyParser : PoEItemParser<CurrencyBuilder>
    {
        public CurrencyParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new CurrencyBuilder();
        }

        public override PoEItem Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return itemBuilder.Build();
        }

        private void ParseCurrencyData()
        {
            if(!itemFieldsDict.ContainsKey("Stack Size"))
            {
                throw new FormatException("Missing Stack Size field");
            }

            string stackVal = itemFieldsDict["Stack Size"];
            stackVal = stackVal.Substring(0, stackVal.IndexOf('/'));

            itemBuilder.SetStackSize(int.Parse(stackVal));
        }
    }
}
