using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : ModdableItemParser<Armor>
    {
        public ArmorParser(
            string rawItemText,
            PoEItemType itemCategory = (PoEItemType)400
            ) : base(rawItemText)
        {
            _item = new Armor()
            {
                Category = itemCategory
            };
        }

        protected override void ParseItem()
        {
            base.ParseItem();

            if (_itemFields.ContainsKey("Armour"))
            {
                _item.Armour = int.Parse(_itemFields["Armour"]);
            }
            if (_itemFields.ContainsKey("Evasion Rating"))
            {
                _item.EvasionRating = int.Parse(_itemFields["Evasion Rating"]);
            }
            if (_itemFields.ContainsKey("Energy Shield"))
            {
                _item.EnergyShield = int.Parse(_itemFields["Energy Shield"]);
            }
        }

        protected override int GetModsStartIndex()
        {
            int index = base.GetModsStartIndex();

            if(index < _itemSections.Length)
            {
                var possibleEnchant = Mod.Parse(_itemSections[index]);
                var statRepo = StatRepository.GetRepository();

                if (statRepo.IsEnchantment(possibleEnchant.Affix))
                {
                    _item.Enchantment = possibleEnchant;
                    index++;
                }
            }

            return index;
        }
    }
}
