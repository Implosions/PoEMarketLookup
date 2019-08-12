using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class CurrencyParser : PoEItemParser<Currency>
    {
        public CurrencyParser(string rawItemText) : base(rawItemText)
        {
            _item = new Currency();
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            string stackVal = _itemFields["Stack Size"];
            stackVal = stackVal.Substring(0, stackVal.IndexOf('/'));

            _item.StackSize = int.Parse(stackVal);
        }
    }
}
