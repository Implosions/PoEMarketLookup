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
        private int reqLevel;

        public ArmorParser(string rawItemText) : base(rawItemText)
        {
        }

        public override IPoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();
            ParseItemRequirements();

            return new Armor(itemBase, armour, evasionRating, energyShield, quality, reqLevel);
        }

        private void ParseItemRequirements()
        {
            var fields = itemSections[2].Trim().Split('\n');

            foreach(string field in fields)
            {
                if (field.StartsWith("Requirements"))
                {
                    continue;
                }

                int fieldVal = int.Parse(ParseFieldValue(field));

                if (field.StartsWith("Level"))
                {
                    reqLevel = fieldVal;
                }
            }
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
