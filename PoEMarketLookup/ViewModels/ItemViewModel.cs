using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public string ItemBase { get; set; }
        public string ItemName { get; set; }

        public ItemModContainer ItemEnchant { get; set; }
        public IList<ItemStat> ItemStats { get; set; }
        public IList<ItemModContainer> ItemImplicits { get; set; }
        public IList<ItemModContainer> ItemExplicits { get; set; }
    }
}
