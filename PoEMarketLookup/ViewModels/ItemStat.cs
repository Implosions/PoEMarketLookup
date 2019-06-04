namespace PoEMarketLookup.ViewModels
{
    public class ItemStat : ItemField
    {
        public float Value { get; }
        public string Name { get; }

        public ItemStat(string name, float value)
        {
            Value = value;
            Name = name;
            Title = name + ": " + value;
        }
    }
}
