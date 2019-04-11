using PoEMarketLookup.PoE.Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class PoEItemParser<TPoEItem> where TPoEItem : PoEItem
    {
        private static readonly Regex RE_SECTION_SEPARATOR = new Regex(@"\s+" + new string('-', 8) + @"\s+");

        protected string[] itemSections;
        protected Dictionary<string, string> itemFieldsDict;
        protected TPoEItem item;

        public PoEItemParser(string rawItemText)
        {
            rawItemText = rawItemText.Trim();
            itemSections = RE_SECTION_SEPARATOR.Split(rawItemText);
            itemFieldsDict = ParseItemSectionFields(rawItemText);

            if (itemSections.Length < 2)
            {
                throw new FormatException("Missing sections");
            }
        }

        public abstract TPoEItem Parse();

        protected void ParseInfoSection()
        {
            string[] itemInfoFields = SplitItemSection(itemSections[0]);
            Rarity rarity;

            switch (itemFieldsDict["Rarity"])
            {
                case "Normal": rarity = Rarity.Normal; break;
                case "Magic": rarity = Rarity.Magic; break;
                case "Rare": rarity = Rarity.Rare; break;
                case "Unique": rarity = Rarity.Unique; break;
                case "Currency": rarity = Rarity.Currency; break;
                case "Gem": rarity = Rarity.Gem; break;
                default: rarity = Rarity.Unknown; break;
            }

            if (rarity == Rarity.Rare || rarity == Rarity.Unique)
            {
                item.Name = itemInfoFields[1];
                item.Base = itemInfoFields[2];
            }
            else
            {
                item.Base = itemInfoFields[1];
            }

            item.Rarity = rarity;
        }

        protected string[] SplitItemSection(string section)
        {
            return section.Split(new char[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        private string ParseFieldValue(string field)
        {
            int startIndex = field.IndexOf(':') + 2;

            if(startIndex >= field.Length)
            {
                return null;
            }
            if (!field.Contains("(augmented)"))
            {
                return field.Substring(startIndex);
            }
            
            int len = field.LastIndexOf(' ') - startIndex;

            return field.Substring(startIndex, len);
        }

        private string ParseFieldName(string field)
        {
            return field.Substring(0, field.IndexOf(":"));
        }

        private Dictionary<string, string> ParseItemSectionFields(string itemSection)
        {
            var dict = new Dictionary<string, string>();
            var fields = SplitItemSection(itemSection);

            foreach(string field in fields)
            {
                if (!field.Contains(":"))
                {
                    continue;
                }

                string name = ParseFieldName(field);

                if (dict.ContainsKey(name))
                {
                    continue;
                }

                string value = ParseFieldValue(field);

                dict.Add(name, value);
            }

            return dict;
        }
    }
}
