using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class ModdableItemParser<T> : PoEItemParser<T> 
        where T : ModdableItem
    {
        private static readonly Regex RE_RESISTANCE = new Regex(@"\+#% to (?:(?: and )?(Fire|Cold|Lightning|Chaos|all Elemental))+ Resistance");

        public ModdableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        protected void ParseModdableItemSections()
        {
            ParseInfoSection();
            ParseItemQuality();
            ParseItemRequirements();
            ParseItemSockets();
            ParseItemLevel();
            ParseItemMods();
        }

        protected override void ParseInfoSection()
        {
            if (itemFields.ContainsKey("Rarity"))
            {
                switch (itemFields["Rarity"])
                {
                    case "Normal": item.Rarity = Rarity.Normal; break;
                    case "Magic": item.Rarity = Rarity.Magic; break;
                    case "Rare": item.Rarity = Rarity.Rare; break;
                    case "Unique": item.Rarity = Rarity.Unique; break;
                }
            }

            string[] itemInfoFields = Utils.SplitItemSection(itemSections[0]);

            if((int)item.Rarity > 1)
            {
                item.Name = itemInfoFields[1];
                item.Base = itemInfoFields[2];
            }
            else
            {
                item.Base = itemInfoFields[1];
            }
        }

        private void ParseItemQuality()
        {
            if (itemFields.ContainsKey("Quality"))
            {
                string qualVal = itemFields["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                item.Quality = int.Parse(qualVal);
            }
        }

        private void ParseItemRequirements()
        {
            if (!itemFields.ContainsKey("Requirements"))
            {
                return;
            }
            if (itemFields.ContainsKey("Level"))
            {
                item.LevelRequirement = int.Parse(itemFields["Level"]);
            }
            if (itemFields.ContainsKey("Str"))
            {
                item.StrengthRequirement = int.Parse(itemFields["Str"]);
            }
            if (itemFields.ContainsKey("Dex"))
            {
                item.DexterityRequirement = int.Parse(itemFields["Dex"]);
            }
            if (itemFields.ContainsKey("Int"))
            {
                item.IntelligenceRequirement = int.Parse(itemFields["Int"]);
            }
        }

        private void ParseItemSockets()
        {
            if (itemFields.ContainsKey("Sockets"))
            {
                item.Sockets = SocketGroup.Parse(itemFields["Sockets"]);
            }
        }

        private void ParseItemLevel()
        {
            if (itemFields.ContainsKey("Item Level"))
            {
                item.ItemLevel = int.Parse(itemFields["Item Level"]);
            }
        }

        private void ParseItemMods()
        {
            string rarity = itemFields["Rarity"];
            int modsStartIndex = GetModsStartIndex();
            int remainingSections = GetPossibleModsSectionsCount(modsStartIndex);

            bool hasImplicit =
                (rarity.Equals("Normal") && remainingSections == 1)
                || remainingSections == 2;

            if (hasImplicit)
            {
                var mods = GetModsFromModSection(itemSections[modsStartIndex]);
                item.ImplicitMods = mods;
            }

            if (!rarity.Equals("Normal"))
            {
                int explicitModsIndex = hasImplicit ? modsStartIndex + 1 : modsStartIndex;
                var mods = GetModsFromModSection(itemSections[explicitModsIndex]);
                item.ExplicitMods = mods;
            }
        }

        private Mod[] GetModsFromModSection(string section)
        {
            string[] sectionTokens = Utils.SplitItemSection(section);
            Mod[] parsedMods = new Mod[sectionTokens.Length];

            for (int i = 0; i < parsedMods.Length; i++)
            {
                var rawMod = sectionTokens[i];
                var mod = Mod.Parse(rawMod);
                parsedMods[i] = mod;
                CheckForLifeAndResists(mod);
            }

            return parsedMods;
        }

        private void CheckForLifeAndResists(Mod mod)
        {
            var match = RE_RESISTANCE.Match(mod.Affix);

            if (match.Success)
            {
                int val = (int)mod.AffixValues[0];

                foreach(var capture in match.Groups[1].Captures)
                {
                    switch (capture.ToString())
                    {
                        case "Fire": item.FireResistance += val; break;
                        case "Cold": item.ColdResistance += val; break;
                        case "Lightning": item.LightningResistance += val; break;
                        case "Chaos": item.ChaosResistance += val; break;
                        case "all Elemental":
                            item.FireResistance += val;
                            item.ColdResistance += val;
                            item.LightningResistance += val;
                            break;
                    }
                }

                return;
            }

            if(mod.Affix == "+# to maximum Life")
            {
                item.TotalLife += (int)mod.AffixValues[0];
            }
            else if (mod.Affix == "+# to all Attributes"
                || mod.Affix.StartsWith("+# to Strength"))
            {
                item.TotalLife += (int)mod.AffixValues[0] / 2;
            }
        }

        /// <summary>
        /// Locates the item level index and returns the next index
        /// The item implicit/explicit mods are always after the item level section
        /// </summary>
        /// <returns>The index directly proceding the item level section index</returns>
        protected virtual int GetModsStartIndex()
        {
            int i;

            for (i = 0; i < itemSections.Length; i++)
            {
                if(itemSections[i].StartsWith("Item Level:"))
                {
                    break;
                }
            }

            return i + 1;
        }

        protected virtual int GetPossibleModsSectionsCount(int index)
        {
            int remainingSections = itemSections.Length - index;

            for(int i = index; i < itemSections.Length; i++)
            {
                if (!CheckPossibleModSection(itemSections[i]))
                {
                    remainingSections--;
                }
            }

            if (itemFields.ContainsKey("Note"))
            {
                remainingSections--;
            }

            if (itemFields["Rarity"].Equals("Unique"))
            {
                remainingSections--;
            }

            return remainingSections;
        }

        protected virtual bool CheckPossibleModSection(string itemSection)
        {
            bool isPossibleMod = false;

            if (itemSection.Equals("Corrupted"))
            {
                item.Corrupted = true;
            }
            else if (itemSection.Equals("Mirrored"))
            {
                item.Mirrored = true;
            }
            else if (itemSection.Equals("Shaper Item"))
            {
                item.Shaper = true;
            }
            else if (itemSection.Equals("Elder Item"))
            {
                item.Elder = true;
            }
            else if (itemSection.Equals("Synthesised Item"))
            {
                item.Synthesised = true;
            }
            else
            {
                isPossibleMod = true;
            }

            return isPossibleMod;
        }
    }
}
