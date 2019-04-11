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
            if (itemFieldsDict.ContainsKey("Level"))
            {
                item.Level = int.Parse(itemFieldsDict["Level"]);
            }
        }

        private void ParseGemQuality()
        {
            if (itemFieldsDict.ContainsKey("Quality"))
            {
                string qualVal = itemFieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                item.Quality = int.Parse(qualVal);
            }
        }

        private void ParseGemExperience()
        {
            if (itemFieldsDict.ContainsKey("Experience"))
            {
                string fieldVal = itemFieldsDict["Experience"];
                item.Experience = long.Parse(fieldVal.Substring(0, fieldVal.IndexOf('/')));
            }
        }
    }
}
