namespace PoEMarketLookup.PoE.Items.Builders
{
    public class GemBuilder : PoEItemBuilder
    {
        public int Level;

        public GemBuilder SetGemLevel(int level)
        {
            Level = level;

            return this;
        }

        public override PoEItem Build()
        {
            return new Gem(this);
        }
    }
}
