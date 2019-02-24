using PoEMarketLookup.PoE.Items;
using System;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class PoEItemParser
    {
        private static readonly string SECTION_SEPARATOR = new string('-', 8);

        protected string[] itemSections;
        protected string itemBase;

        public PoEItemParser(String rawItemText)
        {
            itemSections = rawItemText.Split(SECTION_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (itemSections.Length < 2)
            {
                throw new FormatException("Missing sections");
            }
        }

        public abstract IPoEItem Parse();

        protected void ParseInfoSection()
        {
            string[] itemInfoFields = itemSections[0].Trim()
                                                     .Split('\n');

            if (itemInfoFields.Length < 2)
            {
                throw new FormatException("Missing fields in item section 1");
            }

            itemBase = itemInfoFields[1].Trim();
        }

        protected string ParseFieldValue(string field)
        {
            int startIndex = field.LastIndexOf(':') + 1;
            int endIndex = field.LastIndexOf(' ');
            int len = endIndex > startIndex ? endIndex - startIndex : field.Length - startIndex;

            return field.Substring(startIndex, len);
        }
    }
}
