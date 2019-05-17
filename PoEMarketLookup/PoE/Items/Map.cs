namespace PoEMarketLookup.PoE.Items
{
    public class Map : ModdableItem
    {
        public int Tier { get; set; }
        public int Quantity { get; set; }
        public int ItemRarity { get; set; }
        public int PackSize { get; set; }

        public Map()
        {
            Category = Components.PoEItemType.Map;
        }
    }
}
