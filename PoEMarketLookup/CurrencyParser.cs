using System;
using System.Text.RegularExpressions;

namespace PoEMarketLookup
{
    public class CurrencyParser
    {
        private static readonly string SECTION_SEPARATOR = new string('-', 8);
        private static readonly Regex RE_STACK_SIZE = new Regex(@"Stack Size: (\d+)");

        private string[] itemSections;
        private string itemBase;
        private int stackSize;

        public CurrencyParser(string rawItemText)
        {
            itemSections = rawItemText.Split(SECTION_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public Currency Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return new Currency(itemBase, stackSize);
        }

        private void ParseInfoSection()
        {
            string[] itemInfo = itemSections[0].Split('\n');
            itemBase = itemInfo[1].Trim();
        }

        private void ParseCurrencyData()
        {
            var match = RE_STACK_SIZE.Match(itemSections[1]);
            var valGroup = match.Groups[1];
            stackSize = int.Parse(valGroup.Value);
        }
    }
}
