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
            if (itemFieldsDict.ContainsKey("Armour"))
            {
                item.Armour = int.Parse(itemFieldsDict["Armour"]);
            }
            if (itemFieldsDict.ContainsKey("Evasion Rating"))
            {
                item.EvasionRating = int.Parse(itemFieldsDict["Evasion Rating"]);
            }
            if (itemFieldsDict.ContainsKey("Energy Shield"))
            {
                item.EnergyShield = int.Parse(itemFieldsDict["Energy Shield"]);
            }
        }
    }
}
