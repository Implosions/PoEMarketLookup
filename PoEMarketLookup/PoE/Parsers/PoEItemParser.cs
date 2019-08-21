using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public class PoEItemParser<TPoEItem> : IPoEItemParser where TPoEItem : PoEItem, new()
    {
        private static readonly Regex RE_SECTION_SEPARATOR = new Regex(@"\s+" + new string('-', 8) + @"\s+");

        protected string[] _itemSections;
        protected Dictionary<string, string> _itemFields;
        protected TPoEItem _item;

        public PoEItemParser(string rawItemText)
        {
            _item = new TPoEItem();
            rawItemText = rawItemText.Trim();
            _itemSections = RE_SECTION_SEPARATOR.Split(rawItemText);
            _itemFields = Utils.GetItemFields(rawItemText);
        }

        public PoEItemParser(string rawItemText, PoEItemType type) : this(rawItemText)
        {
            _item.Category = type;
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
