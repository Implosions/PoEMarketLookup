namespace PoEMarketLookup.PoE.Items
{
    public class ArmorBuilder : PoEItemBuilder
    {
        public override IPoEItem Build()
        {
            return new Armor(this);
        }
    }
}
