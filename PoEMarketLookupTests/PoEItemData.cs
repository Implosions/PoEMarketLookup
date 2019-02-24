using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoEMarketLookupTests
{
    public static class PoEItemData
    {
        public static class Currency
        {
            public static readonly string EXALTED_ORB = 
@"Rarity: Currency
Exalted Orb
--------
Stack Size: 9/10
--------
Enchants a rare item with a new random property
--------
Right click this item then left click a rare item to apply it. Rare items can have up to six random properties.
Shift click to unstack.";

            public static readonly string ORB_MISSING_INFO_FIELD =
@"Exalted Orb
--------
Stack Size: 9/10
--------
Enchants a rare item with a new random property
--------
Right click this item then left click a rare item to apply it. Rare items can have up to six random properties.
Shift click to unstack.";

            public static readonly string ORB_MISSING_STACKSIZE_FIELD =
@"Rarity: Currency
Exalted Orb
--------
Enchants a rare item with a new random property
--------
Right click this item then left click a rare item to apply it. Rare items can have up to six random properties.
Shift click to unstack.";
        }

        public static class Armor
        {
            public static readonly string GLOVES_AR =
@"Rarity: Normal
Plated Gauntlets
--------
Armour: 39
--------
Requirements:
Level: 11
Str: 20
--------
Sockets: R-R R 
--------
Item Level: 33";

            public static readonly string BODY_EV =
@"Rarity: Normal
Sun Leather
--------
Evasion Rating: 324
--------
Requirements:
Level: 32
Dex: 91
--------
Sockets: G-G 
--------
Item Level: 33";
        }
    }
}
