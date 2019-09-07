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
                string[] sectionTokens = Utils.SplitItemSection(_itemSections[index]);
                var parsedMods = new Mod[sectionTokens.Length];

                for (int i = 0; i < parsedMods.Length; i++)
                {
                    parsedMods[i] = Mod.Parse(sectionTokens[i]);
                }
                
                var statRepo = StatRepository.GetRepository();

                if (statRepo.IsEnchantment(parsedMods[0].Affix))
                {
                    _item.Enchantments = parsedMods;
                    index++;
                }
            }

            return index;
        }
    }
}
