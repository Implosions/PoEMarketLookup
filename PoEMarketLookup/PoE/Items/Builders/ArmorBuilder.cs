namespace PoEMarketLookup.PoE.Items.Builders
{
    public class ArmorBuilder : ModdableItemBuilder
    {
        public int Armour;
        public int EvasionRating;
        public int EnergyShield;

        public ArmorBuilder SetArmour(int ar)
        {
            Armour = ar;

            return this;
        }

        public ArmorBuilder SetEvasion(int ev)
        {
            EvasionRating = ev;

            return this;
        }

        public ArmorBuilder SetEnergyShield(int es)
        {
            EnergyShield = es;

            return this;
        }

        public override PoEItem Build()
        {
            return new Armor(this);
        }
    }
}
