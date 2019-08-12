using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class GemParser : PoEItemParser<Gem>
    {
        public GemParser(string rawItemText) : base(rawItemText)
        {
            item = new Gem();
        }

        public override Gem Parse()
        {
            ParseInfoSection();
            ParseGemLevel();
            ParseGemQuality();
            ParseGemExperience();

            return item;
        }

        private void ParseGemLevel()
        {
            if (itemFields.ContainsKey("Level"))
            {
                item.Level = int.Parse(itemFields["Level"]);
            }
        }

        private void ParseGemQuality()
        {
            if (itemFields.ContainsKey("Quality"))
            {
                string qualVal = itemFields["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                item.Quality = int.Parse(qualVal);
            }
        }

        private void ParseGemExperience()
        {
            if (itemFields.ContainsKey("Experience"))
            {
                string fieldVal = itemFields["Experience"];
                item.Experience = long.Parse(fieldVal.Substring(0, fieldVal.IndexOf('/')));
            }
        }
    }
}
