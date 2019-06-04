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
            var vm = new ItemViewModel
            {
                ItemBase = item.Base
            };

            if(item.GetType().IsSubclassOf(typeof(ModdableItem)))
            {
                var moddedItem = (ModdableItem)item;
                var implicits = new List<ItemModContainer>();
                var explicits = new List<ItemModContainer>();

                if(moddedItem.ImplicitMods != null)
                {
                    foreach (var mod in moddedItem.ImplicitMods)
                    {
                        implicits.Add(new ItemModContainer(mod));
                    }
                }

                if(moddedItem.ExplicitMods != null)
                {
                    foreach (var mod in moddedItem.ExplicitMods)
                    {
                        explicits.Add(new ItemModContainer(mod));
                    }
                }

                vm.ItemImplicits = implicits;
                vm.ItemExplicits = explicits;
            }

            return vm;
        }
    }
}
