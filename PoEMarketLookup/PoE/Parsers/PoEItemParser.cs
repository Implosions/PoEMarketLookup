﻿using PoEMarketLookup.PoE.Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class PoEItemParser
    {
        private static readonly Regex RE_SECTION_SEPARATOR = new Regex(new string('-', 8));

        protected string[] itemSections;
        protected Dictionary<string, string> itemFieldsDict;
        protected PoEItemBuilder itemBuilder;

        public PoEItemParser(String rawItemText)
        {
            itemSections = RE_SECTION_SEPARATOR.Split(rawItemText);
            itemFieldsDict = ParseItemSectionFields(rawItemText);

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

            itemBuilder.SetBase(itemInfoFields[1].Trim());
        }

        private string ParseFieldValue(string field)
        {
            int startIndex = field.IndexOf(':') + 1;

            if(startIndex == field.Length - 1)
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
