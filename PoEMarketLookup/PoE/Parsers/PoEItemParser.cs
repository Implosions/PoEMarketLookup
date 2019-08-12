using PoEMarketLookup.PoE.Items;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class PoEItemParser<TPoEItem> : IPoEItemParser where TPoEItem : PoEItem
    {
        private static readonly Regex RE_SECTION_SEPARATOR = new Regex(@"\s+" + new string('-', 8) + @"\s+");

        protected string[] _itemSections;
        protected Dictionary<string, string> _itemFields;
        protected TPoEItem _item;

        public PoEItemParser(string rawItemText)
        {
            rawItemText = rawItemText.Trim();
            _itemSections = RE_SECTION_SEPARATOR.Split(rawItemText);
            _itemFields = Utils.GetItemFields(rawItemText);
        }

        public TPoEItem Parse()
        {
            ParseItem();

            return _item;
        }

        protected virtual void ParseItem()
        {
            ParseInfoSection();
        }

        protected virtual void ParseInfoSection()
        {
            string[] itemInfoFields = Utils.SplitItemSection(_itemSections[0]);
            _item.Base = itemInfoFields[1];
        }

        PoEItem IPoEItemParser.Parse()
        {
            return Parse();
        }
    }
}
