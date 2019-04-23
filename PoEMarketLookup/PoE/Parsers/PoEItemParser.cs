﻿using PoEMarketLookup.PoE.Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class PoEItemParser<TPoEItem> : IPoEItemParser where TPoEItem : PoEItem
    {
        private static readonly Regex RE_SECTION_SEPARATOR = new Regex(@"\s+" + new string('-', 8) + @"\s+");

        protected string[] itemSections;
        protected Dictionary<string, string> itemFields;
        protected TPoEItem item;

        public PoEItemParser(string rawItemText)
        {
            rawItemText = rawItemText.Trim();
            itemSections = RE_SECTION_SEPARATOR.Split(rawItemText);
            itemFields = Utils.GetItemFields(rawItemText);

            if (itemSections.Length < 2)
            {
                throw new FormatException("Missing sections");
            }
        }

        public abstract TPoEItem Parse();

        protected void ParseInfoSection()
        {
            string[] itemInfoFields = Utils.SplitItemSection(itemSections[0]);
            string rarity = itemFields["Rarity"];

            if (rarity == "Rare" || rarity == "Unique")
            {
                item.Name = itemInfoFields[1];
                item.Base = itemInfoFields[2];
            }
            else
            {
                item.Base = itemInfoFields[1];
            }
        }

        PoEItem IPoEItemParser.Parse()
        {
            return Parse();
        }
    }
}
