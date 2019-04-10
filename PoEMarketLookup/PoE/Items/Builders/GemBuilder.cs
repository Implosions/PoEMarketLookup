namespace PoEMarketLookup.PoE.Items.Builders
{
    public class GemBuilder : PoEItemBuilder
    {
        public int Level;
        public int Quality;

        public GemBuilder SetGemLevel(int level)
        {
            Level = level;

            return this;
        }

        public GemBuilder SetGemQuality(int quality)
        {
            Quality = quality;

            return this;
        }

        public override PoEItem Build()
        {
            return new Gem(this);
        }
    }
}
