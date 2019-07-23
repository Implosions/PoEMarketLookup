using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PoEMarketLookup.PoE.Items.Components
{
    public enum Rarity
    {
        Normal,
        Magic,
        Rare,
        Unique
    }

    public static class RarityUtils
    {
        private static readonly IDictionary<string, Rarity> _rarityDefinitions = new ReadOnlyDictionary<string, Rarity>(
            new Dictionary<string, Rarity>()
            {
                { "Normal", Rarity.Normal },
                { "Magic", Rarity.Magic },
                { "Rare", Rarity.Rare },
                { "Unique", Rarity.Unique }
            });

        public static Rarity StringToRarity(string rarity)
        {
            if (!_rarityDefinitions.ContainsKey(rarity))
            {
                throw new System.NotImplementedException("Rarity: '" + rarity + "' not found");
            }

            return _rarityDefinitions[rarity];
        }
    }
}
