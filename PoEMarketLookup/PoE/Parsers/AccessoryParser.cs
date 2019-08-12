using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class AccessoryParser : ModdableItemParser<Accessory>
    {
        public AccessoryParser(
            string rawItemText,
            PoEItemType itemCategory = (PoEItemType)300
            ) : base(rawItemText)
        {
            item = new Accessory()
            {
                Category = itemCategory
            };
        }

        public override Accessory Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseTalismanTier();

            return item;
        }

        private void ParseTalismanTier()
        {
            if (itemFields.ContainsKey("Talisman Tier"))
            {
                var tier = itemFields["Talisman Tier"];
                item.TalismanTier = int.Parse(tier);
            }
        }

        protected override int GetPossibleModsSectionsCount(int index)
        {
            int remaining = base.GetPossibleModsSectionsCount(index);

            if(itemFields.ContainsKey("Talisman Tier"))
            {
                remaining--;
            }

            return remaining;
        }

        protected override int GetModsStartIndex()
        {
            int index = base.GetModsStartIndex();

            if (itemFields.ContainsKey("Talisman Tier"))
            {
                index++;
            }

            return index;
        }

        protected override bool CheckPossibleModSection(string itemSection)
        {
            if(itemSection.StartsWith("Place into"))
            {
                return false;
            }
            else
            {
                return base.CheckPossibleModSection(itemSection);
            }
        }
    }
}
