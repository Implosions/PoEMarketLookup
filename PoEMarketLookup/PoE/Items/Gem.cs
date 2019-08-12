namespace PoEMarketLookup.PoE.Items
{
    public class Gem : CorruptableItem
    {
        public int Level { get; set; }
        public long Experience { get; set; }

        public Gem()
        {
            Category = Components.PoEItemType.Gem;
        }
    }
}
