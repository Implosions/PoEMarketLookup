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

            public static readonly string BODY_ES =
@"Rarity: Normal
Destroyer Regalia
--------
Energy Shield: 104
--------
Requirements:
Level: 53
Int: 144
--------
Sockets: B-B-B-B-B-B 
--------
Item Level: 100";

            public static readonly string BODY_AR_EV_ES =
@"Rarity: Normal
Fake Chest
--------
Armour: 500
Evasion Rating: 450
Energy Shield: 400
--------
Requirements:
Level: 100
Str: 1
Dex: 2
Int: 3
--------
Sockets: B-B-B-B-B-B 
--------
Item Level: 100";

            public static readonly string BODY_QUAL_EV =
@"Rarity: Normal
Sun Leather
--------
Quality: +20% (augmented)
Evasion Rating: 646 (augmented)
--------
Requirements:
Level: 32
Dex: 91
--------
Sockets: G-G 
--------
Item Level: 33";

            public static readonly string SHIELD_ES_WITH_IMPLICIT =
@"Rarity: Normal
Jingling Spirit Shield
--------
Chance to Block: 23%
Energy Shield: 24
--------
Requirements:
Level: 28
Int: 71
--------
Sockets: G-B 
--------
Item Level: 33
--------
13% increased Spell Damage";

            public static readonly string SHIELD_ES_RARE =
@"Rarity: Rare
Carrion Duty
Fossilised Spirit Shield
--------
Chance to Block: 22%
Energy Shield: 72 (augmented)
--------
Requirements:
Level: 59
Int: 141
--------
Sockets: G-B 
--------
Item Level: 74
--------
15% increased Spell Damage
--------
+25 to maximum Energy Shield
+104 to maximum Life
+7% to all Elemental Resistances
+26% to Cold Resistance
+20% to Lightning Resistance";
        }
    }
}