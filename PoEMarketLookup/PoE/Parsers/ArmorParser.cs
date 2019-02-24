using System;
using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : PoEItemParser
    {
        private int armour;
        private int evasionRating;
        private int energyShield;
        private int quality;

        public ArmorParser(string rawItemText) : base(rawItemText)
        {
        }

        public override IPoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();

            return new Armor(itemBase, armour, evasionRating, energyShield, quality);
        }

        private void ParseArmorValuesSection()
        {
            var fields = itemSections[1].Trim().Split('\n');

            foreach (string field in fields)
            {
                string fieldVal = ParseFieldValue(field);

                if (field.StartsWith("Quality"))
                {
                    string qualVal = fieldVal.Substring(1, fieldVal.Length - 2);
                    quality = int.Parse(qualVal);
                    continue;
                }

                int numericVal = int.Parse(fieldVal);

                if (field.StartsWith("Armour"))
                {
                    armour = numericVal;
                }
                else if (field.StartsWith("Evasion Rating"))
                {
                    evasionRating = numericVal;
                }
                else if (field.StartsWith("Energy Shield"))
                {
                    energyShield = numericVal;
                }
            }
        }
    }
}
