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
            else if ((int)itemCategory >= 200 && (int)itemCategory < 300)
            {
                parser = new WeaponParser(_rawItem, itemCategory);
            }

            return parser;
        }
    }
}
