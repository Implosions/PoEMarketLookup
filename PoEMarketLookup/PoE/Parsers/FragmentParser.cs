using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class FragmentParser : PoEItemParser<Fragment>
    {
        public FragmentParser(string rawItemText) : base(rawItemText)
        {
            _item = new Fragment();
        }
    }
}
