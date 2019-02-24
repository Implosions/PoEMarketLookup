using PoEMarketLookup.PoE.Items;
using System;
using System.Collections.Generic;

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

            if(startIndex == field.Length - 1)
            {
                return null;
            }

            int endIndex = field.LastIndexOf(' ');
            int len = endIndex > startIndex ? endIndex - startIndex : field.Length - startIndex;

            return field.Substring(startIndex, len);
        }

        protected string ParseFieldName(string field)
        {
            return field.Substring(0, field.IndexOf(":"));
        }

        protected Dictionary<string, string> ParseItemSectionFields(string itemSection)
        {
            var dict = new Dictionary<string, string>();
            var fields = itemSection.Trim().Split('\n');

            foreach(string field in fields)
            {
                if (!field.Contains(":"))
                {
                    continue;
                }

                string name = ParseFieldName(field);
                string value = ParseFieldValue(field);

                dict.Add(name, value);
            }

            return dict;
        }
    }
}
