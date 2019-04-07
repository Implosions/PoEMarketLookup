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
            ParseItemLevel();
            ParseTalismanTier();
            ParseItemMods();

            return itemBuilder.Build();
        }

        private void ParseItemRequirements()
        {
            if (!itemFieldsDict.ContainsKey("Requirements"))
            {
                return;
            }
            if (itemFieldsDict.ContainsKey("Level"))
            {
                var reqLevel = int.Parse(itemFieldsDict["Level"]);
                itemBuilder.SetLevelRequirement(reqLevel);
            }
            if (itemFieldsDict.ContainsKey("Str"))
            {
                var reqStr = int.Parse(itemFieldsDict["Str"]);
                itemBuilder.SetStrengthRequirement(reqStr);
            }
            if (itemFieldsDict.ContainsKey("Dex"))
            {
                var reqDex = int.Parse(itemFieldsDict["Dex"]);
                itemBuilder.SetDexterityRequirement(reqDex);
            }
            if (itemFieldsDict.ContainsKey("Int"))
            {
                var reqInt = int.Parse(itemFieldsDict["Int"]);
                itemBuilder.SetIntelligenceRequirement(reqInt);
            }
        }

        private void ParseArmorValuesSection()
        {
            if (itemFieldsDict.ContainsKey("Quality"))
            {
                string qualVal = itemFieldsDict["Quality"];
                qualVal = qualVal.Substring(1, qualVal.Length - 2);
                itemBuilder.SetQuality(int.Parse(qualVal));
            }
            if (itemFieldsDict.ContainsKey("Armour"))
            {
                var ar = int.Parse(itemFieldsDict["Armour"]);
                itemBuilder.SetArmour(ar);
            }
            if (itemFieldsDict.ContainsKey("Evasion Rating"))
            {
                var ev = int.Parse(itemFieldsDict["Evasion Rating"]);
                itemBuilder.SetEvasion(ev);
            }
            if (itemFieldsDict.ContainsKey("Energy Shield"))
            {
                var es = int.Parse(itemFieldsDict["Energy Shield"]);
                itemBuilder.SetEnergyShield(es);
            }
        }

        private void ParseItemSockets()
        {
            if (itemFieldsDict.ContainsKey("Sockets"))
            {
                itemBuilder.SetSocketGroup(SocketGroup.Parse(itemFieldsDict["Sockets"]));
            }
        }

        private void ParseItemLevel()
        {
            if(itemFieldsDict.ContainsKey("Item Level"))
            {
                var ilvl = itemFieldsDict["Item Level"];
                itemBuilder.SetItemLevel(int.Parse(ilvl));
            }
        }

        private void ParseTalismanTier()
        {
            if (itemFieldsDict.ContainsKey("Talisman Tier"))
            {
                var tier = itemFieldsDict["Talisman Tier"];
                itemBuilder.SetTalismanTier(int.Parse(tier));
            }
        }

        private void ParseItemMods()
        {
            int modsStartIndex;
            int remainingSections = 0;

            for(modsStartIndex = itemSections.Length - 1; modsStartIndex > 0; modsStartIndex--)
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
                else if (!itemBuilder.Corrupted && itemSection.Equals("Corrupted"))
                {
                    itemBuilder.SetCorrupted();
                    remainingSections--;
                }
                else if (!itemBuilder.Mirrored && itemSection.Equals("Mirrored"))
                {
                    itemBuilder.SetMirrored();
                    remainingSections--;
                }
                else if (!itemBuilder.Shaper && !itemBuilder.Elder && !itemBuilder.Synthesised)
                {
                    bool updated = false;

                    switch (itemSection)
                    {
                        case "Shaper Item":
                            itemBuilder.SetShaper();
                            updated = true;
                            break;

                        case "Elder Item":
                            itemBuilder.SetElder();
                            updated = true;
                            break;

                        case "Synthesised Item":
                            itemBuilder.SetSynthesised();
                            updated = true;
                            break;
                    }

                    if(updated)
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

            // @TODO: Find reliable way to get all enchants
            // Can only get enchants on items with an implicit this way since there is no indicator that a mod is an implicit or enchant
            if(remainingSections == 3)
            {
                var enchant = Mod.Parse(itemSections[modsStartIndex + 1]);

                itemBuilder.SetEnchantment(enchant);
                modsStartIndex++;
            }

            bool hasImplicit = 
                (itemBuilder.Rarity == Rarity.Normal && remainingSections >= 1)
                || remainingSections >= 2;

            if (hasImplicit)
            {
                var mods = GetModsFromModSection(itemSections[modsStartIndex + 1]);
                itemBuilder.SetImplicitMods(mods);
            }

            if(itemBuilder.Rarity != Rarity.Normal)
            {
                int explicitModsIndex = hasImplicit ? modsStartIndex + 2 : modsStartIndex + 1;
                var mods = GetModsFromModSection(itemSections[explicitModsIndex]);
                itemBuilder.SetExplicitMods(mods);
            }
        }

        private Mod[] GetModsFromModSection(string section)
        {
            string[] sectionTokens = SplitItemSection(section);
            Mod[] parsedMods = new Mod[sectionTokens.Length];

            for(int i = 0; i < parsedMods.Length; i++)
            {
                var rawMod = sectionTokens[i];
                var mod = Mod.Parse(rawMod);
                parsedMods[i] = mod;
            }

            return parsedMods;
        }
    }
}
