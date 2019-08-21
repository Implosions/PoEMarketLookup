using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Parsers
{
    public abstract class ModdableItemParser<T> : QualitableItemParser<T> 
        where T : ModdableItem, new()
    {
        private static readonly Regex RE_RESISTANCE = new Regex(@"\+#% to (?:(?: and )?(Fire|Cold|Lightning|Chaos|all Elemental))+ Resistance");

        public ModdableItemParser(string rawItemText) : base(rawItemText)
        {
        }

        public ModdableItemParser(string rawItemText, PoEItemType type) : base(rawItemText, type)
        {
        }

        protected override void ParseItem()
        {
            base.ParseItem();
            
            ParseItemRequirements();
            ParseItemSockets();
            ParseItemLevel();
            ParseItemMods();
        }

        protected override void ParseInfoSection()
        {
            if (_itemFields.ContainsKey("Rarity"))
            {
                _item.Rarity = RarityUtils.StringToRarity(_itemFields["Rarity"]);
            }

            string[] itemInfoFields = Utils.SplitItemSection(_itemSections[0]);

            if((int)_item.Rarity > 1)
            {
                _item.Name = itemInfoFields[1];
                _item.Base = itemInfoFields[2];
            }
            else
            {
                _item.Base = itemInfoFields[1];
            }
        }

        private void ParseItemRequirements()
        {
            if (!_itemFields.ContainsKey("Requirements"))
            {
                return;
            }
            if (_itemFields.ContainsKey("Level"))
            {
                _item.LevelRequirement = int.Parse(_itemFields["Level"]);
            }
            if (_itemFields.ContainsKey("Str"))
            {
                _item.StrengthRequirement = int.Parse(_itemFields["Str"]);
            }
            if (_itemFields.ContainsKey("Dex"))
            {
                _item.DexterityRequirement = int.Parse(_itemFields["Dex"]);
            }
            if (_itemFields.ContainsKey("Int"))
            {
                _item.IntelligenceRequirement = int.Parse(_itemFields["Int"]);
            }
        }

        private void ParseItemSockets()
        {
            if (_itemFields.ContainsKey("Sockets"))
            {
                _item.Sockets = SocketGroup.Parse(_itemFields["Sockets"]);
            }
        }

        private void ParseItemLevel()
        {
            if (_itemFields.ContainsKey("Item Level"))
            {
                _item.ItemLevel = int.Parse(_itemFields["Item Level"]);
            }
        }

        private void ParseItemMods()
        {
            string rarity = _itemFields["Rarity"];
            int modsStartIndex = GetModsStartIndex();
            int remainingSections = GetPossibleModsSectionsCount(modsStartIndex);

            bool hasImplicit =
                (rarity.Equals("Normal") && remainingSections == 1)
                || remainingSections == 2;

            if (hasImplicit)
            {
                var mods = GetModsFromModSection(_itemSections[modsStartIndex]);
                _item.ImplicitMods = mods;
            }

            if (!rarity.Equals("Normal"))
            {
                int explicitModsIndex = hasImplicit ? modsStartIndex + 1 : modsStartIndex;
                var mods = GetModsFromModSection(_itemSections[explicitModsIndex]);
                _item.ExplicitMods = mods;
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
                        case "Fire": _item.FireResistance += val; break;
                        case "Cold": _item.ColdResistance += val; break;
                        case "Lightning": _item.LightningResistance += val; break;
                        case "Chaos": _item.ChaosResistance += val; break;
                        case "all Elemental":
                            _item.FireResistance += val;
                            _item.ColdResistance += val;
                            _item.LightningResistance += val;
                            break;
                    }
                }

                return;
            }

            if(mod.Affix == "+# to maximum Life")
            {
                _item.TotalLife += (int)mod.AffixValues[0];
            }
            else if (mod.Affix == "+# to all Attributes"
                || mod.Affix.StartsWith("+# to Strength"))
            {
                _item.TotalLife += (int)mod.AffixValues[0] / 2;
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

            for (i = 0; i < _itemSections.Length; i++)
            {
                if(_itemSections[i].StartsWith("Item Level:"))
                {
                    break;
                }
            }

            return i + 1;
        }

        protected virtual int GetPossibleModsSectionsCount(int index)
        {
            int remainingSections = _itemSections.Length - index;

            for(int i = index; i < _itemSections.Length; i++)
            {
                if (!CheckPossibleModSection(_itemSections[i]))
                {
                    remainingSections--;
                }
            }

            if (_itemFields.ContainsKey("Note"))
            {
                remainingSections--;
            }

            if (_itemFields["Rarity"].Equals("Unique"))
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
                _item.Corrupted = true;
            }
            else if (itemSection.Equals("Mirrored"))
            {
                _item.Mirrored = true;
            }
            else if (itemSection.Equals("Shaper Item"))
            {
                _item.Shaper = true;
            }
            else if (itemSection.Equals("Elder Item"))
            {
                _item.Elder = true;
            }
            else if (itemSection.Equals("Synthesised Item"))
            {
                _item.Synthesised = true;
            }
            else if(itemSection.Equals("Fractured Item"))
            {
                _item.Fractured = true;
            }
            else
            {
                isPossibleMod = true;
            }

            return isPossibleMod;
        }
    }
}
