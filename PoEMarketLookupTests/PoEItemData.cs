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

            public static readonly string GLOVES_CORRUPTED =
@"Rarity: Rare
Cataclysm Talons
Trapper Mitts
--------
Evasion Rating: 88 (augmented)
Energy Shield: 18 (augmented)
--------
Requirements:
Level: 36
Dex: 29
Int: 29
--------
Sockets: R B 
--------
Item Level: 36
--------
35% increased Evasion and Energy Shield
+24 to maximum Life
6.4 Life Regenerated per second
+11% to Fire Resistance
+6% to Lightning Resistance
9% increased Stun and Block Recovery
--------
Corrupted";
        }

        public class Accessories
        {
            public static readonly string AMULET_RARE =
@"Rarity: Rare
Mind Locket
Amber Amulet
--------
Requirements:
Level: 65
--------
Item Level: 83
--------
+30 to Strength
--------
21% increased Spell Damage
+54 to Dexterity
+37 to Intelligence
+23 to maximum Energy Shield
14% increased maximum Energy Shield
+43% to Fire Resistance";

            public static readonly string RING_RARE = 
@"Rarity: Rare
Demon Band
Ruby Ring
--------
Requirements:
Level: 36
--------
Item Level: 69
--------
+20% to Fire Resistance
--------
Adds 9 to 16 Fire Damage to Attacks
Adds 10 to 24 Cold Damage to Attacks
+10 to Accuracy Rating
7% increased Cast Speed
+40 to maximum Mana
5% increased Light Radius";

            public static readonly string BELT_RARE =
@"Rarity: Rare
Victory Girdle
Chain Belt
--------
Requirements:
Level: 40
--------
Item Level: 63
--------
+19 to maximum Energy Shield
--------
+7 to maximum Energy Shield
16% increased Flask Mana Recovery rate
Reflects 3 Physical Damage to Melee Attackers
Your Critical Strike Chance is Lucky while Focussed";
        }
    }
}