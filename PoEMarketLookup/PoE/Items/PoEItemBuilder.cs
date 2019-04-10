using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class PoEItemBuilder
    {
        public Rarity Rarity;
        public string Base;
        public string Name;
        public int StackSize;
        public int Armour;
        public int EvasionRating;
        public int EnergyShield;
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
        public int TalismanTier;
        public bool Shaper;
        public bool Elder;
        public bool Synthesised;
        public bool Mirrored;
        public Mod Enchantment;
        public string Type;
        public DamageRange PhysicalDamage;
        public DamageRange ChaosDamage;
        public DamageRange FireDamage;
        public DamageRange ColdDamage;
        public DamageRange LightningDamage;
        public double CriticalStrikeChance;

        public abstract PoEItem Build();

        public PoEItemBuilder SetRarity(Rarity rarity)
        {
            Rarity = rarity;

            return this;
        }

        public PoEItemBuilder SetBase(string baseItem)
        {
            Base = baseItem;

            return this;
        }

        public PoEItemBuilder SetArmour(int ar)
        {
            Armour = ar;

            return this;
        }

        public PoEItemBuilder SetEvasion(int ev)
        {
            EvasionRating = ev;

            return this;
        }

        public PoEItemBuilder SetEnergyShield(int es)
        {
            EnergyShield = es;

            return this;
        }

        public PoEItemBuilder SetQuality(int quality)
        {
            Quality = quality;

            return this;
        }

        public PoEItemBuilder SetLevelRequirement(int level)
        {
            LevelRequirement = level;

            return this;
        }

        public PoEItemBuilder SetStrengthRequirement(int str)
        {
            StrengthRequirement = str;

            return this;
        }

        public PoEItemBuilder SetDexterityRequirement(int dex)
        {
            DexterityRequirement = dex;

            return this;
        }

        public PoEItemBuilder SetIntelligenceRequirement(int intel)
        {
            IntelligenceRequirement = intel;

            return this;
        }

        public PoEItemBuilder SetSocketGroup(SocketGroup sockets)
        {
            Sockets = sockets;

            return this;
        }

        public PoEItemBuilder SetStackSize(int size)
        {
            StackSize = size;

            return this;
        }

        public PoEItemBuilder SetItemLevel(int ilvl)
        {
            ItemLevel = ilvl;

            return this;
        }

        public PoEItemBuilder SetImplicitMods(Mod[] mods)
        {
            ImplicitMods = mods;

            return this;
        }

        public PoEItemBuilder SetItemName(string name)
        {
            Name = name;

            return this;
        }

        public PoEItemBuilder SetExplicitMods(Mod[] mods)
        {
            ExplicitMods = mods;

            return this;
        }

        public PoEItemBuilder SetCorrupted()
        {
            Corrupted = true;

            return this;
        }

        public PoEItemBuilder SetTalismanTier(int tier)
        {
            TalismanTier = tier;

            return this;
        }

        public PoEItemBuilder SetShaper()
        {
            Shaper = true;

            return this;
        }

        public PoEItemBuilder SetElder()
        {
            Elder = true;

            return this;
        }

        public PoEItemBuilder SetSynthesised()
        {
            Synthesised = true;

            return this;
        }

        public PoEItemBuilder SetMirrored()
        {
            Mirrored = true;

            return this;
        }

        public PoEItemBuilder SetEnchantment(Mod enchant)
        {
            Enchantment = enchant;

            return this;
        }

        public PoEItemBuilder SetType(string type)
        {
            Type = type;

            return this;
        }

        public PoEItemBuilder SetPhysicalDamage(int bottom, int top)
        {
            PhysicalDamage.BottomEnd = bottom;
            PhysicalDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetChaosDamage(int bottom, int top)
        {
            ChaosDamage.BottomEnd = bottom;
            ChaosDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetFireDamage(int bottom, int top)
        {
            FireDamage.BottomEnd = bottom;
            FireDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetColdDamage(int bottom, int top)
        {
            ColdDamage.BottomEnd = bottom;
            ColdDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetLightningDamage(int bottom, int top)
        {
            LightningDamage.BottomEnd = bottom;
            LightningDamage.TopEnd = top;

            return this;
        }

        public PoEItemBuilder SetCritChance(double crit)
        {
            CriticalStrikeChance = crit;

            return this;
        }
    }
}
