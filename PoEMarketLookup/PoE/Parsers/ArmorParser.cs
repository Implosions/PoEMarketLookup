using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : ModdableItemParser
    {
        public ArmorParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new ArmorBuilder();
        }

        public override PoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();
            ParseModdableItemSections();

            return itemBuilder.Build();
        }

        private void ParseArmorValuesSection()
        {
            if (itemFieldsDict.ContainsKey("Armour"))
            {
                var ar = int.Parse(itemFieldsDict["Armour"]);
                itemBuilder.SetArmour(ar);
            }
            if (itemFieldsDict.ContainsKey("Evasion Rating"))
            {
                var ev = int.Parse(itemFieldsDict["Evasion Rating"]);
                itemBuilder.SetEvasion(ev);
            }
            if (itemFieldsDict.ContainsKey("Energy Shield"))
            {
                var es = int.Parse(itemFieldsDict["Energy Shield"]);
                itemBuilder.SetEnergyShield(es);
            }
        }
    }
}
