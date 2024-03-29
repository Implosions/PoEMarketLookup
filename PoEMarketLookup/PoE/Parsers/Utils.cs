﻿using PoEMarketLookup.PoE.Items.Components;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public static class Utils
    {
        private static Regex _reAmulet = new Regex(@"\b(?:Amulet|Talisman)\b");
        private static Regex _reRing = new Regex(@"\b(?:Ring)\b");
        private static Regex _reBelt = new Regex(@"\b(?:Belt|Rustic Sash|Stygian Vise)\b");
        private static Regex _reQuiver = new Regex(@"\b(?:Quiver)\b");
        private static Regex _reJewel = new Regex(@"\b(?:Jewel)\b");
        private static Regex _reHelmet = 
            new Regex(@"\b(?:Helmet|Hat|Burgonet|Cap|Tricorne|Hood|Pelt|Circlet|Cage|Helm|Sallet|Bascinet|Coif|Crown|Mask)\b");
        private static Regex _reGloves = new Regex(@"\b(?:Gloves|Gauntlets|Mitts)\b");
        private static Regex _reBoots = new Regex(@"\b(?:Boots|Greaves|Shoes|Slippers)\b");
        private static Regex _reBodyArmor = 
            new Regex(@"\b(?:Vest|Chestplate|Plate|Jerkin|Leather|Tunic|Garb|Robe|Vestment|Regalia|Wrap|Brigandine|Doublet|Armour|Lamellar|Wyrmscale|Dragonscale|Coat|Ringmail|Chainmail|Hauberk|Jacket|Raiment)\b");
        private static Regex _reFlask = new Regex(@"\b(?:Flask)\b");
        private static Regex _reMap = new Regex(@"\b(?:Map)\b");

        public static string[] SplitItemSection(string section)
        {
            return section.Split(new char[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ParseFieldValue(string field)
        {
            int startIndex = field.IndexOf(':') + 2;

            if (startIndex >= field.Length)
            {
                return null;
            }
            if (!field.EndsWith(")"))
            {
                return field.Substring(startIndex);
            }

            int len = field.LastIndexOf(' ') - startIndex;

            return field.Substring(startIndex, len);
        }

        public static string ParseFieldName(string field)
        {
            return field.Substring(0, field.IndexOf(":"));
        }

        public static Dictionary<string, string> GetItemFields(string itemSection)
        {
            var fields = new Dictionary<string, string>();
            var lines = SplitItemSection(itemSection);

            foreach (string line in lines)
            {
                if (!line.Contains(":"))
                {
                    continue;
                }

                string name = ParseFieldName(line);

                if (fields.ContainsKey(name))
                {
                    continue;
                }

                string value = ParseFieldValue(line);

                fields.Add(name, value);
            }

            return fields;
        }

        public static PoEItemType FindItemType(string item)
        {
            PoEItemType type = PoEItemType.Fragment;
            var fields = GetItemFields(item);

            if (!fields.ContainsKey("Rarity"))
            {
                return PoEItemType.Unknown;
            }

            string rarity = fields["Rarity"];

            if (rarity.Equals("Currency"))
            {
                type = PoEItemType.Currency;
            }
            else if (rarity.Equals("Gem"))
            {
                type = PoEItemType.Gem;
            }
            else if(fields.ContainsKey("Attacks per Second"))
            {
                int firstSectionEndIndex = item.IndexOf(new string('-', 8)) + 8;
                // ignore first 2 characters of the section
                int endIndex = item.IndexOf('\n', firstSectionEndIndex + 2);
                string weaponType = item.Substring(firstSectionEndIndex, endIndex - firstSectionEndIndex).Trim();
                
                type = ItemTypeUtils.StringToItemType(weaponType);
            }
            else if (fields.ContainsKey("Chance to Block"))
            {
                type = PoEItemType.Shield;
            }
            else if (item.Contains("Right-click to add this prophecy to your character"))
            {
                type = PoEItemType.Prophecy;
            }
            else
            {
                var lines = SplitItemSection(item);
                string itemBase;

                // If the third line is a section separator
                if (lines[2][0] == '-') 
                {
                    itemBase = lines[1];
                }
                else
                {
                    itemBase = lines[2];
                }

                if (_reAmulet.IsMatch(itemBase))
                {
                    type = PoEItemType.Amulet;
                }
                else if (_reRing.IsMatch(itemBase))
                {
                    type = PoEItemType.Ring;
                }
                else if (_reBelt.IsMatch(itemBase))
                {
                    type = PoEItemType.Belt;
                }
                else if (_reQuiver.IsMatch(itemBase))
                {
                    type = PoEItemType.Quiver;
                }
                else if (_reJewel.IsMatch(itemBase))
                {
                    type = PoEItemType.Jewel;
                }
                else if (_reHelmet.IsMatch(itemBase))
                {
                    type = PoEItemType.Helmet;
                }
                else if (_reGloves.IsMatch(itemBase))
                {
                    type = PoEItemType.Gloves;
                }
                else if (_reBoots.IsMatch(itemBase))
                {
                    type = PoEItemType.Boots;
                }
                else if (_reBodyArmor.IsMatch(itemBase))
                {
                    type = PoEItemType.BodyArmor;
                }
                else if (_reFlask.IsMatch(itemBase))
                {
                    type = PoEItemType.Flask;
                }
                else if (_reMap.IsMatch(itemBase))
                {
                    type = PoEItemType.Map;
                }
            }

            return type;
        }
    }
}
