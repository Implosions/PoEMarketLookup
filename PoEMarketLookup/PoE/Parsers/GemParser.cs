using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Parsers
{
    public class GemParser : PoEItemParser<GemBuilder>
    {
        public GemParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new GemBuilder();
        }

        public override PoEItem Parse()
        {
            ParseInfoSection();
            ParseGemLevel();
            ParseGemQuality();
            ParseGemExperience();

            return itemBuilder.Build();
        }

        private void ParseGemLevel()
        {
            if (itemFieldsDict.ContainsKey("Level"))
            {
                var val = int.Parse(itemFieldsDict["Level"]);

                itemBuilder.SetGemLevel(val);
            }
        }

        private void ParseGemQuality()
        {
            if (itemFieldsDict.ContainsKey("Quality"))
            {
                string qualVal = itemFieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                itemBuilder.SetGemQuality(int.Parse(qualVal));
            }
        }

        private void ParseGemExperience()
        {
            if (itemFieldsDict.ContainsKey("Experience"))
            {
                string fieldVal = itemFieldsDict["Experience"];
                var exp = long.Parse(fieldVal.Substring(0, fieldVal.IndexOf('/')));
                itemBuilder.SetGemExperience(exp);
            }
        }
    }
}
