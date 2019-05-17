namespace PoEMarketLookup.PoE.Items
{
    public class Flask : ModdableItem
    {
        public int MaxCharges { get; set; }
        public int ChargesConsumedOnUse { get; set; }

        public Flask()
        {
            Category = Components.PoEItemType.Flask;
        }
    }
}
