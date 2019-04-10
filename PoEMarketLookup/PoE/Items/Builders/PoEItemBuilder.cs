using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookup.PoE.Items.Builders
{
    public abstract class PoEItemBuilder
    {
        public Rarity Rarity;
        public string Base;
        public string Name;

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

        public PoEItemBuilder SetItemName(string name)
        {
            Name = name;

            return this;
        }
    }
}
