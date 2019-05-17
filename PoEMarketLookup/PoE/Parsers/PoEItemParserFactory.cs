using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class PoEItemParserFactory
    {
        private string _rawItem;

        public PoEItemParserFactory(string rawItemText)
        {
            _rawItem = rawItemText;
        }

        public IPoEItemParser GetParser()
        {
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
            else if ((int)itemCategory >= 200 && (int)itemCategory < 300)
            {
                parser = new WeaponParser(_rawItem, itemCategory);
            }
            else if ((int)itemCategory >= 300 && (int)itemCategory < 400)
            {
                parser = new AccessoryParser(_rawItem, itemCategory);
            }
            else if ((int)itemCategory >= 400 && (int)itemCategory < 500)
            {
                parser = new ArmorParser(_rawItem, itemCategory);
            }

            return parser;
        }
    }
}
