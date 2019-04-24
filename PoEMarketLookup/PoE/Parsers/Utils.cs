using PoEMarketLookup.PoE.Items.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public static class Utils
    {
        private static Regex _reAmulet = new Regex(@"\bAmulet\b");

        public static string[] SplitItemSection(string section)
        {
            return section.Split(new char[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ParseFieldValue(string field)
        {
            int startIndex = field.IndexOf(':') + 2;

            if (startIndex >= field.Length)
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

        public static string ParseFieldName(string field)
        {
            return field.Substring(0, field.IndexOf(":"));
        }

        public static Dictionary<string, string> GetItemFields(string itemSection)
        {
            var fields = new Dictionary<string, string>();
            var lines = SplitItemSection(itemSection);

            foreach (string line in lines)
            {
                if (!line.Contains(":"))
                {
                    continue;
                }

                string name = ParseFieldName(line);

                if (fields.ContainsKey(name))
                {
                    continue;
                }

                string value = ParseFieldValue(line);

                fields.Add(name, value);
            }

            return fields;
        }

        public static PoEItemType FindItemType(string item)
        {
            PoEItemType type = PoEItemType.Unknown;
            var fields = GetItemFields(item);
            string rarity = string.Empty;

            if (fields.ContainsKey("Rarity"))
            {
                rarity = fields["Rarity"];
            }
            else
            {
                return type;
            }

            if (rarity.Equals("Currency"))
            {
                type = PoEItemType.Currency;
            }
            else if (rarity.Equals("Gem"))
            {
                type = PoEItemType.Gem;
            }
            else if(fields.ContainsKey("Attacks per Second"))
            {
                int firstSectionEndIndex = item.IndexOf(new string('-', 8)) + 8;
                // ignore first 2 characters of the section
                int endIndex = item.IndexOf('\n', firstSectionEndIndex + 2);
                string weaponType = item.Substring(firstSectionEndIndex, endIndex - firstSectionEndIndex).Trim();
                
                type = PoEItemTypeExtensions.GetValueFromDescription(weaponType);
            }
            else
            {
                var lines = SplitItemSection(item);
                string itemBase;

                if(rarity.Equals("Rare") || rarity.Equals("Unique"))
                {
                    itemBase = lines[2];
                }
                else
                {
                    itemBase = lines[1];
                }

                if (_reAmulet.IsMatch(itemBase))
                {
                    type = PoEItemType.Amulet;
                }
            }

            return type;
        }
    }
}
