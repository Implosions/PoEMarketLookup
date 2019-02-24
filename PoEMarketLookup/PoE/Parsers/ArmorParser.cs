using System;
using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : PoEItemParser
    {
        private int armour;
        private int evasionRating;
        private int energyShield;

        public ArmorParser(string rawItemText) : base(rawItemText)
        {
        }

        public override IPoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();

            return new Armor(itemBase, armour, evasionRating, energyShield);
        }

        private void ParseArmorValuesSection()
        {
            string armorValuesSection = itemSections[1].Trim();

            int val = int.Parse(ParseFieldValue(armorValuesSection));

            if (armorValuesSection.StartsWith("Armour"))
            {
                armour = val;
            }
            else if (armorValuesSection.StartsWith("Evasion Rating"))
            {
                evasionRating = val;
            }
            else if (armorValuesSection.StartsWith("Energy Shield"))
            {
                energyShield = val;
            }
        }
    }
}
