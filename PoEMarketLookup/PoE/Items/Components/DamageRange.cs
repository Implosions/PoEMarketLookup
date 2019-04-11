namespace PoEMarketLookup.PoE.Items.Components
{
    public class DamageRange
    {
        public int BottomEnd { get; set; }
        public int TopEnd { get; set; }
        public int Combined => BottomEnd + TopEnd;
    }
}
