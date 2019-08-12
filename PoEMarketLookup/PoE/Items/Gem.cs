namespace PoEMarketLookup.PoE.Items
{
    public class Gem : PoEItem
    {
        public int Level { get; set; }
        public int Quality { get; set; }
        public long Experience { get; set; }

        public Gem()
        {
            Category = Components.PoEItemType.Gem;
        }
    }
}
