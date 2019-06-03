using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoEMarketLookup.ViewModels
{
    public class ItemStat : ItemField
    {
        int Value { get; }

        public ItemStat(string name, int value)
        {
            Value = value;
            Title = name + ": " + Value;
        }
    }
}
