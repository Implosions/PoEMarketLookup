using PoEMarketLookup.PoE.Items;
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
            if (_rawItem == null || !_reItemFormat.IsMatch(_rawItem))
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
            else if (itemCategory.IsArmor())
            {
                parser = new ArmorParser(_rawItem, itemCategory);
            }
            else if (itemCategory.IsAccessory())
            {
                parser = new AccessoryParser(_rawItem, itemCategory);
            }
            else if (itemCategory.IsWeapon())
            {
                parser = new WeaponParser(_rawItem, itemCategory);
            }
            else if (itemCategory == PoEItemType.Fragment)
            {
                parser = new PoEItemParser<PoEItem>(_rawItem, itemCategory);
            }
            
            return parser;
        }
    }
}
