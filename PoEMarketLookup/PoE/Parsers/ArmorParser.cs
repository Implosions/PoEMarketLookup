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
            var fields = itemSections[1].Trim().Split('\n');

            foreach (string field in fields)
            {
                int val = int.Parse(ParseFieldValue(field));

                if (field.StartsWith("Armour"))
                {
                    armour = val;
                }
                else if (field.StartsWith("Evasion Rating"))
                {
                    evasionRating = val;
                }
                else if (field.StartsWith("Energy Shield"))
                {
                    energyShield = val;
                }
            }
        }
    }
}
