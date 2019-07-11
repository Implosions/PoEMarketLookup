namespace PoEMarketLookup.ViewModels
{
    public class ItemStat<T> : ItemField
    {
        public T Value { get; }
        public string Name { get; }

        public ItemStat(string name, T value)
        {
            Value = value;
            Name = name;

            if(typeof(T) == typeof(bool))
            {
                Title = name;
            }
            else
            {
                Title = name + ": " + value;
            }
        }
    }
}
