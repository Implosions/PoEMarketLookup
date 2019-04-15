using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : ModdableItemParser<Armor>
    {
        public ArmorParser(string rawItemText) : base(rawItemText)
        {
            item = new Armor();
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
    }
}
