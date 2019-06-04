namespace PoEMarketLookup.ViewModels
{
    public class ItemStat : ItemField
    {
        public int Value { get; }
        public string Name { get; }

        public ItemStat(string name, int value)
        {
            Value = value;
            Name = name;
            Title = name + ": " + value;
        }
    }
}
