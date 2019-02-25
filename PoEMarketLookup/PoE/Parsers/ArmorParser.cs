using System;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public class ArmorParser : PoEItemParser
    {
        public ArmorParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new ArmorBuilder();
        }

        public override IPoEItem Parse()
        {
            ParseInfoSection();
            ParseArmorValuesSection();
            ParseItemRequirements();
            ParseItemSockets();

            return itemBuilder.Build();
        }

        private void ParseItemRequirements()
        {
            var fieldsDict = ParseItemSectionFields(itemSections[2]);

            if (fieldsDict.ContainsKey("Level"))
            {
                var reqLevel = int.Parse(fieldsDict["Level"]);
                itemBuilder.SetLevelRequirement(reqLevel);
            }
            if (fieldsDict.ContainsKey("Str"))
            {
                var reqStr = int.Parse(fieldsDict["Str"]);
                itemBuilder.SetStrengthRequirement(reqStr);
            }
            if (fieldsDict.ContainsKey("Dex"))
            {
                var reqDex = int.Parse(fieldsDict["Dex"]);
                itemBuilder.SetDexterityRequirement(reqDex);
            }
            if (fieldsDict.ContainsKey("Int"))
            {
                var reqInt = int.Parse(fieldsDict["Int"]);
                itemBuilder.SetIntelligenceRequirement(reqInt);
            }
        }

        private void ParseArmorValuesSection()
        {
            var fieldsDict = ParseItemSectionFields(itemSections[1]);

            if (fieldsDict.ContainsKey("Quality"))
            {
                string qualVal = fieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                itemBuilder.SetQuality(int.Parse(qualVal));
            }
            if (fieldsDict.ContainsKey("Armour"))
            {
                var ar = int.Parse(fieldsDict["Armour"]);
                itemBuilder.SetArmour(ar);
            }
            if (fieldsDict.ContainsKey("Evasion Rating"))
            {
                var ev = int.Parse(fieldsDict["Evasion Rating"]);
                itemBuilder.SetEvasion(ev);
            }
            if (fieldsDict.ContainsKey("Energy Shield"))
            {
                var es = int.Parse(fieldsDict["Energy Shield"]);
                itemBuilder.SetEnergyShield(es);
            }
        }

        private void ParseItemSockets()
        {
            string sockets = ParseFieldValue(itemSections[3].Trim());
            itemBuilder.SetSocketGroup(SocketGroup.Parse(sockets));
        }
    }
}
