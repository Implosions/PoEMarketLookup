using PoEMarketLookup.PoE.Items;
using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public string ItemBase { get; private set; }
        public string ItemName { get; private set; }

        public ItemModContainer ItemEnchant { get; private set; }
        public IList<ItemStat> ItemStats { get; private set; }
        public IList<ItemModContainer> ItemImplicits { get; private set; }
        public IList<ItemModContainer> ItemExplicits { get; private set; }

        private ItemViewModel() { }

        public static ItemViewModel CreateViewModel(PoEItem item)
        {
            return new ItemViewModel()
            {
                ItemBase = item.Base
            };
        }
    }
}
