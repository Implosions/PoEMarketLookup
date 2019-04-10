namespace PoEMarketLookup.PoE.Items.Builders
{
    public class ArmorBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Armor(this);
        }
    }
}
