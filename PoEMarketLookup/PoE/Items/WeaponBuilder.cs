namespace PoEMarketLookup.PoE.Items
{
    public class WeaponBuilder : PoEItemBuilder
    {
        public override PoEItem Build()
        {
            return new Weapon(this);
        }
    }
}
