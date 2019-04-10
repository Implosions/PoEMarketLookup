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
    }
}
