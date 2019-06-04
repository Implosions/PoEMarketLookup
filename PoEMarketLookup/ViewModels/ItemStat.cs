namespace PoEMarketLookup.ViewModels
{
    public class ItemStat : ItemField
    {
        public double Value { get; }
        public string Name { get; }

        public ItemStat(string name, double value)
        {
            Value = value;
            Name = name;
            Title = name + ": " + value;
        }

        public ItemStat(string name, float value) : this(name, (double)value)
        {
        }
    }
}
