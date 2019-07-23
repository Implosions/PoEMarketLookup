using PoEMarketLookup.PoE.Items.Components;
using System;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public class PoEItemParserFactory
    {
        private static readonly Regex _reItemFormat = 
            new Regex(@"^Rarity: (?:\b.*\n){2,3}-{8}");

        private readonly string _rawItem;

        public PoEItemParserFactory(string rawItemText)
        {
            _rawItem = rawItemText;
        }

        public IPoEItemParser GetParser()
        {
            if (!_reItemFormat.IsMatch(_rawItem))
            {
                throw new FormatException("Item text is not in the correct format");
            }

            IPoEItemParser parser = null;
            PoEItemType itemCategory = Utils.FindItemType(_rawItem);

            if (itemCategory == PoEItemType.Currency)
            {
                parser = new CurrencyParser(_rawItem);
            }
            else if (itemCategory == PoEItemType.Gem)
            {
                parser = new GemParser(_rawItem);
            }
            else if(itemCategory == PoEItemType.Flask)
            {
                parser = new FlaskParser(_rawItem);
            }
            else if(itemCategory == PoEItemType.Map)
            {
                parser = new MapParser(_rawItem);
            }
            else if ((int)itemCategory >= 400)
            {
                parser = new ArmorParser(_rawItem, itemCategory);
            }
            else if ((int)itemCategory >= 300)
            {
                parser = new AccessoryParser(_rawItem, itemCategory);
            }
            else if ((int)itemCategory >= 200)
            {
                parser = new WeaponParser(_rawItem, itemCategory);
            }
            
            return parser;
        }
    }
}
