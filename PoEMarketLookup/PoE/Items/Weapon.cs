namespace PoEMarketLookup.PoE.Items
{
    public class Weapon : ModdableItem
    {
        public string Type { get; }

        public Weapon(WeaponBuilder builder) : base(builder)
        {
            Type = builder.Type;
        }
    }
}
