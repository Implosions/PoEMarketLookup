using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class ModdableItemParser<T> : PoEItemParser<T> 
        where T : ModdableItem
    {
        public ModdableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        protected void ParseModdableItemSections()
        {
            ParseItemQuality();
            ParseItemRequirements();
            ParseItemSockets();
            ParseItemLevel();
            ParseItemMods();
        }

        private void ParseItemQuality()
        {
            if (itemFieldsDict.ContainsKey("Quality"))
            {
                string qualVal = itemFieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                item.Quality = int.Parse(qualVal);
            }
        }

        private void ParseItemRequirements()
        {
            if (!itemFieldsDict.ContainsKey("Requirements"))
            {
                return;
            }
            if (itemFieldsDict.ContainsKey("Level"))
            {
                item.LevelRequirement = int.Parse(itemFieldsDict["Level"]);
            }
            if (itemFieldsDict.ContainsKey("Str"))
            {
                item.StrengthRequirement = int.Parse(itemFieldsDict["Str"]);
            }
            if (itemFieldsDict.ContainsKey("Dex"))
            {
                item.DexterityRequirement = int.Parse(itemFieldsDict["Dex"]);
            }
            if (itemFieldsDict.ContainsKey("Int"))
            {
                item.IntelligenceRequirement = int.Parse(itemFieldsDict["Int"]);
            }
        }

        private void ParseItemSockets()
        {
            if (itemFieldsDict.ContainsKey("Sockets"))
            {
                item.Sockets = SocketGroup.Parse(itemFieldsDict["Sockets"]);
            }
        }

        private void ParseItemLevel()
        {
            if (itemFieldsDict.ContainsKey("Item Level"))
            {
                item.ItemLevel = int.Parse(itemFieldsDict["Item Level"]);
            }
        }

        private void ParseItemMods()
        {
            string rarity = itemFieldsDict["Rarity"];
            int modsStartIndex;
            int remainingSections = 0;

            for (modsStartIndex = itemSections.Length - 1; modsStartIndex > 0; modsStartIndex--)
            {
                var itemSection = itemSections[modsStartIndex];

                if (itemSection.Contains("Item Level:"))
                {
                    break;
                }
                else if (itemSection.Contains("Talisman Tier:"))
                {
                    remainingSections--;
                    break;
                }
                else if (!item.Corrupted && itemSection.Equals("Corrupted"))
                {
                    item.Corrupted = true; ;
                    remainingSections--;
                }
                else if (!item.Mirrored && itemSection.Equals("Mirrored"))
                {
                    item.Mirrored = true;
                    remainingSections--;
                }
                else if (!item.Shaper && !item.Elder && !item.Synthesised)
                {
                    bool updated = false;

                    switch (itemSection)
                    {
                        case "Shaper Item":
                            item.Shaper = true;
                            updated = true;
                            break;

                        case "Elder Item":
                            item.Elder = true;
                            updated = true;
                            break;

                        case "Synthesised Item":
                            item.Synthesised = true;
                            updated = true;
                            break;
                    }

                    if (updated)
                    {
                        remainingSections--;
                    }
                }
            }

            remainingSections += (itemSections.Length - modsStartIndex) - 1;

            if (itemFieldsDict.ContainsKey("Note"))
            {
                remainingSections--;
            }

            if (rarity.Equals("Unique"))
            {
                remainingSections--;
            }

            // @TODO: Find reliable way to get all enchants
            // Can only get enchants on items with an implicit this way since there is no indicator that a mod is an implicit or enchant
            if (remainingSections == 3)
            {
                item.Enchantment = Mod.Parse(itemSections[modsStartIndex + 1]);
                modsStartIndex++;
            }

            bool hasImplicit =
                (rarity.Equals("Normal") && remainingSections >= 1)
                || remainingSections >= 2;

            if (hasImplicit)
            {
                var mods = GetModsFromModSection(itemSections[modsStartIndex + 1]);
                item.ImplicitMods = mods;
            }

            if (!rarity.Equals("Normal"))
            {
                int explicitModsIndex = hasImplicit ? modsStartIndex + 2 : modsStartIndex + 1;
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
            }

            return parsedMods;
        }
    }
}
