using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Items
{
    public class Gem : PoEItem
    {
        public int Level { get; }

        public Gem(GemBuilder builder) : base(builder)
        {
            Level = builder.Level;
        }
    }
}
