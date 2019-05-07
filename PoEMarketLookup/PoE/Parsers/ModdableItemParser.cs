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
            int modsStartIndex = GetModsStartIndex();
            int remainingSections = GetPossibleModsSectionsCount(modsStartIndex);

            // @TODO: Find reliable way to get all enchants
            // Can only get enchants on items with an implicit this way since there is no indicator that a mod is an implicit or enchant
            if (remainingSections == 3)
            {
                item.Enchantment = Mod.Parse(itemSections[modsStartIndex]);
                modsStartIndex++;
            }

            bool hasImplicit =
                (rarity.Equals("Normal") && remainingSections >= 1)
                || remainingSections >= 2;

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
            }

            return parsedMods;
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
