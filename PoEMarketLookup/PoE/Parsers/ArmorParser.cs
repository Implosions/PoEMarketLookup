using System;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : PoEItemParser
    {
        private int armour;
        private int evasionRating;
        private int energyShield;
        private int quality;
        private int reqLevel;
        private int reqStr;
        private int reqDex;
        private int reqInt;

        public ArmorParser(string rawItemText) : base(rawItemText)
        {
        }

        public override IPoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();
            ParseItemRequirements();
            var sockets = ParseItemSockets();

            return new Armor(itemBase, armour, evasionRating, energyShield, quality, reqLevel,
                            reqStr, reqDex, reqInt, sockets);
        }

        private void ParseItemRequirements()
        {
            var fieldsDict = ParseItemSectionFields(itemSections[2]);

            if (fieldsDict.ContainsKey("Level"))
            {
                reqLevel = int.Parse(fieldsDict["Level"]);
            }
            if (fieldsDict.ContainsKey("Str"))
            {
                reqStr = int.Parse(fieldsDict["Str"]);
            }
            if (fieldsDict.ContainsKey("Dex"))
            {
                reqDex = int.Parse(fieldsDict["Dex"]);
            }
            if (fieldsDict.ContainsKey("Int"))
            {
                reqInt = int.Parse(fieldsDict["Int"]);
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

        private SocketGroup ParseItemSockets()
        {
            string sockets = ParseFieldValue(itemSections[3].Trim());

            return SocketGroup.Parse(sockets);
        }
    }
}
