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
            var fieldsDict = ParseItemSectionFields(itemSections[2]);

            if (fieldsDict.ContainsKey("Level"))
            {
                reqLevel = int.Parse(fieldsDict["Level"]);
            }
        }

        private void ParseArmorValuesSection()
        {
            var fieldsDict = ParseItemSectionFields(itemSections[1]);

            if (fieldsDict.ContainsKey("Quality"))
            {
                string qualVal = fieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                quality = int.Parse(qualVal);
            }
            if (fieldsDict.ContainsKey("Armour"))
            {
                armour = int.Parse(fieldsDict["Armour"]);
            }
            if (fieldsDict.ContainsKey("Evasion Rating"))
            {
                evasionRating = int.Parse(fieldsDict["Evasion Rating"]);
            }
            if (fieldsDict.ContainsKey("Energy Shield"))
            {
                energyShield = int.Parse(fieldsDict["Energy Shield"]);
            }
        }
    }
}
