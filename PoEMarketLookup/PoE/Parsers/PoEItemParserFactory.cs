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
            var itemFields = Utils.GetItemFields(_rawItem);

            if (itemFields["Rarity"].Equals("Currency"))
            {
                parser = new CurrencyParser(_rawItem);
            }
            else if (itemFields["Rarity"].Equals("Gem"))
            {
                parser = new GemParser(_rawItem);
            }

            return parser;
        }
    }
}
