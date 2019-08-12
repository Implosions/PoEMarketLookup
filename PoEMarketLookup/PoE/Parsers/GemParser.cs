using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class GemParser : PoEItemParser<Gem>
    {
        public GemParser(string rawItemText) : base(rawItemText)
        {
            _item = new Gem();
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            ParseGemLevel();
            ParseGemQuality();
            ParseGemExperience();
        }

        private void ParseGemLevel()
        {
            if (_itemFields.ContainsKey("Level"))
            {
                _item.Level = int.Parse(_itemFields["Level"]);
            }
        }

        private void ParseGemQuality()
        {
            if (_itemFields.ContainsKey("Quality"))
            {
                string qualVal = _itemFields["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                _item.Quality = int.Parse(qualVal);
            }
        }

        private void ParseGemExperience()
        {
            if (_itemFields.ContainsKey("Experience"))
            {
                string fieldVal = _itemFields["Experience"];
                _item.Experience = long.Parse(fieldVal.Substring(0, fieldVal.IndexOf('/')));
            }
        }
    }
}
