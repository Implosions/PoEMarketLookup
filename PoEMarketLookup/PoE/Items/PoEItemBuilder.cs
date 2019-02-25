using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class PoEItemBuilder
    {
        public string Rarity;
        public string Base;
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

        public abstract IPoEItem Build();

        public PoEItemBuilder SetRarity(string rarity)
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
    }
}
