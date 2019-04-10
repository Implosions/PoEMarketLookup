﻿using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Builders;

namespace PoEMarketLookup.PoE.Parsers
{
    public class AccessoryParser : ModdableItemParser<AccessoryBuilder>
    {
        public AccessoryParser(string rawItemText) : base(rawItemText)
        {
            itemBuilder = new AccessoryBuilder();
        }

        public override PoEItem Parse()
        {
            ParseInfoSection();
            ParseModdableItemSections();
            ParseTalismanTier();

            return itemBuilder.Build();
        }

        private void ParseTalismanTier()
        {
            if (itemFieldsDict.ContainsKey("Talisman Tier"))
            {
                var tier = itemFieldsDict["Talisman Tier"];
                itemBuilder.SetTalismanTier(int.Parse(tier));
            }
        }
    }
}
