using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : ModdableItemParser<Armor>
    {
        private Enchantments enchantChecker = Enchantments.LoadEnchantments();

        public ArmorParser(string rawItemText) : base(rawItemText)
        {
            item = new Armor();
        }

        public ArmorParser(string rawItemText, PoEItemType itemCategory) 
            : this(rawItemText)
        {
            item.Category = itemCategory;
        }

        public override Armor Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();
            ParseModdableItemSections();

            return item;
        }

        private void ParseArmorValuesSection()
        {
            if (itemFields.ContainsKey("Armour"))
            {
                item.Armour = int.Parse(itemFields["Armour"]);
            }
            if (itemFields.ContainsKey("Evasion Rating"))
            {
                item.EvasionRating = int.Parse(itemFields["Evasion Rating"]);
            }
            if (itemFields.ContainsKey("Energy Shield"))
            {
                item.EnergyShield = int.Parse(itemFields["Energy Shield"]);
            }
        }

        protected override int GetModsStartIndex()
        {
            int index = base.GetModsStartIndex();

            if(index < itemSections.Length)
            {
                var possibleEnchant = Mod.Parse(itemSections[index]);

                if (enchantChecker.IsEnchantment(possibleEnchant.Affix))
                {
                    item.Enchantment = possibleEnchant;
                    index++;
                }
            }

            return index;
        }
    }
}
