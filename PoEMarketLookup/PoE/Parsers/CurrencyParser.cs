using PoEMarketLookup.PoE.Items;
using System;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public class CurrencyParser : PoEItemParser
    {
        private static readonly Regex RE_STACK_SIZE = new Regex(@"Stack Size: (\d+)");

        public CurrencyParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new CurrencyBuilder();
        }

        public override IPoEItem Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return itemBuilder.Build();
        }

        private void ParseCurrencyData()
        {
            var match = RE_STACK_SIZE.Match(itemSections[1]);

            if (!match.Success)
            {
                throw new FormatException("Missing Stack Size field");
            }

            var valGroup = match.Groups[1];
            itemBuilder.SetStackSize(int.Parse(valGroup.Value));
        }
    }
}
