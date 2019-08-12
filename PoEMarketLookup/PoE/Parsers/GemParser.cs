using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class GemParser : QualitableItemParser<Gem>
    {
        public GemParser(string rawItemText) : base(rawItemText)
        {
            _item = new Gem();
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            ParseGemLevel();
            ParseGemExperience();
        }

        private void ParseGemLevel()
        {
            if (_itemFields.ContainsKey("Level"))
            {
                _item.Level = int.Parse(_itemFields["Level"]);
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
