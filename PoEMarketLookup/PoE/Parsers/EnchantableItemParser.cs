using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class EnchantableItemParser<T> : ModdableItemParser<T>
        where T : EnchantableItem, new()
    {
        public EnchantableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        public EnchantableItemParser(string rawItemText, PoEItemType type) : base(rawItemText, type)
        {
        }

        protected override int GetModsStartIndex()
        {
            int index = base.GetModsStartIndex();

            if (index < _itemSections.Length)
            {
                var possibleEnchant = Mod.Parse(_itemSections[index]);
                var statRepo = StatRepository.GetRepository();

                if (statRepo.IsEnchantment(possibleEnchant.Affix))
                {
                    _item.Enchantments = new Mod[] { possibleEnchant };
                    index++;
                }
            }

            return index;
        }
    }
}
