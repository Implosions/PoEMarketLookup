using PoEMarketLookup.PoE.Items;
using System;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public class CurrencyParser : PoEItemParser
    {
        private static readonly Regex RE_STACK_SIZE = new Regex(@"Stack Size: (\d+)");

        private int stackSize;

        public CurrencyParser(string rawItemText) : base(rawItemText)
        {
        }

        public override IPoEItem Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return new Currency(itemBase, stackSize);
        }

        private void ParseCurrencyData()
        {
            var match = RE_STACK_SIZE.Match(itemSections[1]);

            if (!match.Success)
            {
                throw new FormatException("Missing Stack Size field");
            }

            var valGroup = match.Groups[1];
            stackSize = int.Parse(valGroup.Value);
        }
    }
}
