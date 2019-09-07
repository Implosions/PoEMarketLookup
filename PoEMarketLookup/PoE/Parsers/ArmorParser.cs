using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : EnchantableItemParser<Armor>
    {
        public ArmorParser(
            string rawItemText,
            PoEItemType itemCategory = (PoEItemType)400) : base(rawItemText, itemCategory)
        {
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
    }
}
