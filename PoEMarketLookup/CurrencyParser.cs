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

            if(itemSections.Length < 2)
            {
                throw new FormatException("Missing sections");
            }
        }

        public Currency Parse() {
            ParseInfoSection();
            ParseCurrencyData();

            return new Currency(itemBase, stackSize);
        }

        private void ParseInfoSection()
        {
            string[] itemInfoFields = itemSections[0].Trim()
                                                     .Split('\n');

            if (itemInfoFields.Length < 2)
            {
                throw new FormatException("Missing fields in item section 1");
            }

            itemBase = itemInfoFields[1].Trim();
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
