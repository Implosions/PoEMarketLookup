using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System;

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
            ParseItemRarity();
            ParseItemQuality();
            ParseItemRequirements();
            ParseItemSockets();
            ParseItemLevel();
            ParseItemMods();
        }

        private void ParseItemRarity()
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
            int modsStartIndex;
            int remainingSections = 0;

            for (modsStartIndex = itemSections.Length - 1; modsStartIndex > 0; modsStartIndex--)
            {
                var itemSection = itemSections[modsStartIndex];

                if (itemSection.StartsWith("Item Level:"))
                {
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
                // Jewel
                else if (itemSection.StartsWith("Place into an"))
                {
                    remainingSections--;
                }
                // Flask
                else if (itemSection.StartsWith("Right click to drink"))
                {
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

            if (itemFields.ContainsKey("Talisman Tier"))
            {
                modsStartIndex++;
                remainingSections--;
            }

            remainingSections += (itemSections.Length - modsStartIndex) - 1;

            if (itemFields.ContainsKey("Note"))
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
