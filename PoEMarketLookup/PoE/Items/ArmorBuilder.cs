namespace PoEMarketLookup.PoE.Items
{
    public class ArmorBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Armor(this);
        }
    }
}
