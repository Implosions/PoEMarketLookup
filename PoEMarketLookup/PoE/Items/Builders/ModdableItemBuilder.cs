using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items.Builders
{
    public abstract class ModdableItemBuilder : PoEItemBuilder
    {
        public int Quality;
        public int LevelRequirement;
        public int StrengthRequirement;
        public int DexterityRequirement;
        public int IntelligenceRequirement;
        public SocketGroup Sockets;
        public int ItemLevel;
        public Mod[] ImplicitMods;
        public Mod[] ExplicitMods;
        public bool Corrupted;
        public bool Shaper;
        public bool Elder;
        public bool Synthesised;
        public bool Mirrored;
        public Mod Enchantment;

        public ModdableItemBuilder SetQuality(int quality)
        {
            Quality = quality;

            return this;
        }

        public ModdableItemBuilder SetLevelRequirement(int level)
        {
            LevelRequirement = level;

            return this;
        }

        public ModdableItemBuilder SetStrengthRequirement(int str)
        {
            StrengthRequirement = str;

            return this;
        }

        public ModdableItemBuilder SetDexterityRequirement(int dex)
        {
            DexterityRequirement = dex;

            return this;
        }

        public ModdableItemBuilder SetIntelligenceRequirement(int intel)
        {
            IntelligenceRequirement = intel;

            return this;
        }

        public ModdableItemBuilder SetSocketGroup(SocketGroup sockets)
        {
            Sockets = sockets;

            return this;
        }

        public ModdableItemBuilder SetItemLevel(int ilvl)
        {
            ItemLevel = ilvl;

            return this;
        }

        public ModdableItemBuilder SetImplicitMods(Mod[] mods)
        {
            ImplicitMods = mods;

            return this;
        }

        public ModdableItemBuilder SetExplicitMods(Mod[] mods)
        {
            ExplicitMods = mods;

            return this;
        }

        public ModdableItemBuilder SetCorrupted()
        {
            Corrupted = true;

            return this;
        }

        public ModdableItemBuilder SetShaper()
        {
            Shaper = true;

            return this;
        }

        public ModdableItemBuilder SetElder()
        {
            Elder = true;

            return this;
        }

        public ModdableItemBuilder SetSynthesised()
        {
            Synthesised = true;

            return this;
        }

        public ModdableItemBuilder SetMirrored()
        {
            Mirrored = true;

            return this;
        }

        public ModdableItemBuilder SetEnchantment(Mod enchant)
        {
            Enchantment = enchant;

            return this;
        }
    }
}
