using System;
using System.Collections.Generic;

namespace PoEMarketLookup.PoE.Parsers
{
    static class Utils
    {
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
    }
}
