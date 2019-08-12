using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoEMarketLookup.PoE.Items
{
    public abstract class QualitableItem : PoEItem
    {
        public int Quality { get; set; }
    }
}
