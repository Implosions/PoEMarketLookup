namespace PoEMarketLookupTests.Parsing
{
    public static class PoEItemData
    {
        public static class Currency
        {
            public const string EXALTED_ORB = 
@"Rarity: Currency
Exalted Orb
--------
Stack Size: 9/10
--------
Enchants a rare item with a new random property
--------
Right click this item then left click a rare item to apply it. Rare items can have up to six random properties.
Shift click to unstack.";

            public const string ORB_MISSING_INFO_FIELD =
@"Exalted Orb
--------
Stack Size: 9/10
--------
Enchants a rare item with a new random property
--------
Right click this item then left click a rare item to apply it. Rare items can have up to six random properties.
Shift click to unstack.";

            public const string ORB_MISSING_STACKSIZE_FIELD =
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
            public const string GLOVES_AR =
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

            public const string BODY_EV =
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

            public const string BODY_ES =
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

            public const string BODY_AR_EV_ES =
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

            public const string BODY_QUAL_EV =
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

            public const string SHIELD_ES_WITH_IMPLICIT =
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

            public const string SHIELD_ES_RARE =
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

            public const string GLOVES_CORRUPTED =
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

            public const string BODY_SHAPER =
@"Rarity: Rare
Mind Shell
Majestic Plate
--------
Armour: 678 (augmented)
--------
Requirements:
Level: 57
Str: 144
--------
Sockets: R-R 
--------
Item Level: 72
--------
+40 to Strength
28% increased Armour
+88 to maximum Life
+43% to Fire Resistance
24% increased Stun and Block Recovery
8% of Physical Damage from Hits taken as Lightning Damage
--------
Shaper Item
";

            public const string BODY_ELDER =
@"Rarity: Rare
Mind Shell
Majestic Plate
--------
Armour: 678 (augmented)
--------
Requirements:
Level: 57
Str: 144
--------
Sockets: R-R 
--------
Item Level: 72
--------
+40 to Strength
28% increased Armour
+88 to maximum Life
+43% to Fire Resistance
24% increased Stun and Block Recovery
8% of Physical Damage from Hits taken as Lightning Damage
--------
Elder Item
";

            public const string BODY_SYNTHESISED =
@"Rarity: Rare
Mind Salvation
Synthesised Scale Vest
--------
Armour: 33 (augmented)
Evasion Rating: 29 (augmented)
--------
Requirements:
Level: 8
--------
Sockets: B G R 
--------
Item Level: 13
--------
17% increased Armour
--------
+10 to Strength
7% increased Armour and Evasion
1.6 Life Regenerated per second
6% increased Stun and Block Recovery
Reflects 8 Physical Damage to Melee Attackers
--------
Synthesised Item
";

            public const string BOOTS_MIRRORED =
@"Rarity: Rare
Gale Tread
Antique Greaves
--------
Armour: 142 (augmented)
--------
Requirements:
Level: 37
Str: 67
--------
Sockets: B-R-R-G 
--------
Item Level: 38
--------
+20 to Armour
+22 to maximum Life
1.8 Life Regenerated per second
+10% to Lightning Resistance
17% increased Stun and Block Recovery
--------
Mirrored
";

            public const string SHIELD_WITH_NOTE =
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
+20% to Lightning Resistance
--------
Note: This is a note
";

            public const string BOOTS_ENCHANTED =
@"Rarity: Rare
Corpse League
Synthesised Leatherscale Boots
--------
Armour: 24 (augmented)
Evasion Rating: 29 (augmented)
--------
Requirements:
Level: 42
--------
Sockets: R-R-B R 
--------
Item Level: 34
--------
Adds 1 to 56 Lightning Damage if you haven't Killed Recently
--------
+8 to Dexterity
--------
+24 to Strength
+12 to Armour
+17 to Evasion Rating
2.3 Life Regenerated per second
20% increased Movement Speed
--------
Synthesised Item
";

            public const string GLOVES_STORMS_GIFT =
@"Rarity: Unique
Storm's Gift
Synthesised Assassin's Mitts
--------
Quality: +3% (augmented)
Evasion Rating: 414 (augmented)
Energy Shield: 80 (augmented)
--------
Requirements:
Level: 58
Dex: 45
Int: 45
--------
Sockets: G-G B 
--------
Item Level: 84
--------
+8% to Fire Resistance
--------
28% increased Damage over Time
295% increased Evasion and Energy Shield
+22% to Lightning Resistance
Enemies you kill are Shocked
Shocks you inflict spread to other Enemies within a Radius of 15
--------
The power of lightning is a power best shared.
--------
Synthesised Item
";

            public const string BOOTS_MAGIC_UNID =
@"Rarity: Magic
Hydrascale Boots
--------
Armour: 106
Evasion Rating: 106
--------
Requirements:
Str: 56
Dex: 56
--------
Sockets: R-R-R-R 
--------
Item Level: 63
--------
Unidentified
";

            public const string ARMOR_TEMPLATE =
@"Rarity: Unique
Test Item
$
";

            public const string HELMET_ENCHANTED =
@"Rarity: Rare
Tempest Halo
Callous Mask
--------
Quality: +20% (augmented)
Evasion Rating: 146 (augmented)
Energy Shield: 29 (augmented)
--------
Requirements:
Level: 52
Dex: 51
Int: 51
--------
Sockets: B-B-G-G 
--------
Item Level: 47
--------
25% increased Cleave Damage
--------
+16 to Dexterity
+12 to Accuracy Rating
+55 to maximum Life
Reflects 4 Physical Damage to Melee Attackers
5% increased Light Radius
+17% to Fire Resistance (crafted)
";

            public const string GLOVES_ENCHANTED =
@"Rarity: Rare
Death Paw
Ringmail Gloves
--------
Armour: 53 (augmented)
Energy Shield: 19 (augmented)
--------
Requirements:
Level: 25
Str: 16
Int: 16
--------
Sockets: B 
--------
Item Level: 26
--------
Trigger Word of Ire when Hit
--------
+18 to Armour
+12 to maximum Energy Shield
+27 to maximum Mana
+12% to Cold Resistance
+3 Mana gained on Kill
--------
Note: ~price 1 alt
";
        }

        public class Accessories
        {
            public const string AMULET_RARE =
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

            public const string RING_RARE = 
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

            public const string BELT_RARE =
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

            public const string AMULET_TALISMAN =
@"Rarity: Rare
Ghoul Collar
Mandible Talisman
--------
Requirements:
Level: 41
--------
Item Level: 55
--------
Talisman Tier: 1
--------
6% increased Attack and Cast Speed
--------
+15 to Dexterity
Adds 7 to 18 Physical Damage to Attacks
Adds 7 to 11 Cold Damage to Attacks
+37 to maximum Mana
+3% to all Elemental Resistances
+10% to Cold Resistance
--------
The First Ones hold us
between two sharpened blades.
That should we stray too far from the path,
we find ourselves severed.
- The Wolven King
--------
Corrupted
";

            public const string BELT_FAMINE_BIND =
@"Rarity: Unique
Faminebind
Rustic Sash
--------
Requirements:
Level: 18
--------
Item Level: 80
--------
24% increased Global Physical Damage
--------
+27% to Cold Resistance
20% increased Projectile Damage
30% reduced Flask Charges gained
60% increased Flask Effect Duration
Deals 50 Chaos Damage per second to nearby Enemies
--------
After the Great Fire, the land lay barren
and our forefathers grew weak.
Mother Gull took pity on them
and gave them grain and water.
--------
Note: ~price 10 alt
";

            public const string BELT_STYGIAN_VISE_RARE =
@"Rarity: Rare
Hate Thread
Stygian Vise
--------
Requirements:
Level: 29
--------
Sockets: A 
--------
Item Level: 74
--------
Has 1 Abyssal Socket
--------
+10 to Armour
+12% to Fire Resistance
+25% to Lightning Resistance
13% increased Stun and Block Recovery
";

            public const string QUIVER_RARE =
@"Rarity: Rare
Gloom Impaler
Penetrating Arrow Quiver
--------
Requirements:
Level: 59
--------
Item Level: 78
--------
Arrows Pierce an additional Target
--------
+38 to Dexterity
Adds 6 to 12 Cold Damage to Attacks
+35% to Global Critical Strike Multiplier
+95 to maximum Life
+39% to Cold Resistance
";

            public const string JEWEL_RARE = 
@"Rarity: Rare
Carrion Glimmer
Viridian Jewel
--------
Item Level: 1
--------
14% increased Totem Damage
4% increased Attack Speed while holding a Shield
+8% to Chaos Resistance
+12% to Fire and Cold Resistances
--------
Place into an allocated Jewel Socket on the Passive Skill Tree. Right click to remove from the Socket.
--------
Corrupted
";

            public const string AMULET_LIFE =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+100 to maximum Life
";

            public const string AMULET_STRENGTH =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+50 to Strength
";

            public const string AMULET_ALL_ATT =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+50 to all Attributes
";

            public const string AMULET_STR_INT =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+50 to Strength and Intelligence
";

            public const string AMULET_STR_DEX =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+50 to Strength and Dexterity
";

            public const string AMULET_COLD_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to Cold Resistance
";

            public const string AMULET_FIRE_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to Fire Resistance
";

            public const string AMULET_LIGHTNING_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to Lightning Resistance
";

            public const string AMULET_CHAOS_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to Chaos Resistance
";

            public const string AMULET_ALL_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to all Elemental Resistances
";

            public const string AMULET_FIRE_COLD_RES =
@"Rarity: Rare
Name
Base
--------
Requirements:
Level: 100
--------
Item Level: 100
--------
+10% to Fire and Cold Resistances
";

            public const string BELT_FRACTURED =
@"Rarity: Rare
Plague Bind
Rustic Sash
--------
Requirements:
Level: 30
--------
Item Level: 50
--------
19% increased Global Physical Damage
--------
+12% to Chaos Resistance (fractured)
+22 to Strength
+47 to Armour
+28% to Cold Resistance
--------
Fractured Item
";
        }

        public class Gem
        {
            public const string DIVINE_IRE =
@"Rarity: Gem
Divine Ire
--------
Lightning, Spell, AoE, Channelling
Level: 1
Mana Cost: 3
Cast Time: 0.22 sec
Critical Strike Chance: 6.00%
Effectiveness of Added Damage: 30%
Quality: +11% (augmented)
Experience: 1/199345
--------
Requirements:
Level: 28
Str: 29
Int: 42
--------
Channelling draws in energy around you to repeatedly build up stages, damaging a number of nearby enemies when you do so. Release to unleash a powerful burst of energy in a beam in front of you.
--------
Deals 17 to 25 Physical Damage
Damages 5 nearby Enemies when you gain Stages
50% of Physical Damage Converted to Lightning Damage
5% increased Area of Effect
Beam deals 70% more Damage with Ailments per Stage after the first
50% less Damage while Channelling
Beam deals 100% more Damage with Hits per Stage after the first
40% chance to gain an additional Stage when Hitting a Normal or Magic Enemy
Gains an additional Stage when Hitting a Rare or Unique Enemy
--------
Place into an item socket of the right colour to gain this skill. Right click to remove from a socket.
";

            public const string EMPOWER_CORRUPTED = 
@"Rarity: Gem
Empower Support
--------
Support
Level: 3 (Max)
Mana Multiplier: 125%
--------
Requirements:
Level: 45
Str: 73
--------
Supports any skill gem. Once this gem reaches level 2 or above, will raise the level of supported gems. Cannot support skills that don't come from gems.
--------
+2 to Level of Supported Active Skill Gems
--------
This is a Support Gem. It does not grant a bonus to your character, but to skills in sockets connected to it. Place into an item socket connected to a socket containing the Active Skill Gem you wish to augment. Right click to remove from a socket.
--------
Corrupted
";
        }

        public class Weapon
        {
            public const string SWORD_REBUKE_OF_THE_VAAL =
@"Rarity: Unique
Rebuke of the Vaal
Vaal Blade
--------
One Handed Sword
Physical Damage: 66-122 (augmented)
Elemental Damage: 25-36 (augmented), 26-35 (augmented), 1-67 (augmented)
Chaos Damage: 25-37 (augmented)
Critical Strike Chance: 5.55%
Attacks per Second: 1.55 (augmented)
Weapon Range: 9
--------
Requirements:
Level: 64
Str: 113
Dex: 113
--------
Sockets: R-G R 
--------
Item Level: 75
--------
+460 to Accuracy Rating
--------
Adds 20 to 36 Physical Damage
Adds 25 to 36 Fire Damage
Adds 26 to 35 Cold Damage
Adds 1 to 67 Lightning Damage
Adds 25 to 37 Chaos Damage
19% increased Attack Speed
--------
Though the Vaal revered peace, it would have 
been suicide for any culture to rouse them to war.
- Icius Perandus, Scholar to the Empire.
--------
Note: ~price 1 alt
";

            public const string WEAPON_TEMPLATE =
@"Rarity: Unique
Test Weapon
Weapon Base
--------
$
Attacks per Second: 1.00
";

            public const string BOW_SYNTHESISED_RARE =
@"Rarity: Rare
Victory Breeze
Synthesised Decurve Bow
--------
Bow
Physical Damage: 17-70
Elemental Damage: 14-26 (augmented), 2-27 (augmented)
Critical Strike Chance: 6.00%
Attacks per Second: 1.25
--------
Requirements:
Level: 38
Dex: 125
--------
Sockets: G-G B 
--------
Item Level: 41
--------
Adds 1 to 21 Lightning Damage
--------
Adds 14 to 26 Fire Damage
Adds 1 to 6 Lightning Damage
11% increased Projectile Speed
+117 to Accuracy Rating
--------
Synthesised Item
--------
Note: ~price 1 alt
";

            public const string DEBEONS_DIRGE =
@"Rarity: Unique
Debeon's Dirge
Despot Axe
--------
Two Handed Axe
Physical Damage: 76-103
Elemental Damage: 328-495 (augmented)
Critical Strike Chance: 5.00%
Attacks per Second: 1.30
Weapon Range: 11
--------
Requirements:
Level: 66
Str: 140
Dex: 86
--------
Sockets: R-G-G 
--------
Item Level: 74
--------
Adds 328 to 495 Cold Damage
15% increased Movement Speed if you've used a Warcry Recently
150% increased Elemental Damage if you've used a Warcry Recently
Warcries Knock Enemies Back in an Area
--------
A sharp and heavy beat,
discorded, out of tune,
when you hear it on the wind,
you know death will follow soon.
--------
Note: ~price 1 alt
";

            public const string PILEDRIVER_NORMAL =
@"Rarity: Normal
Piledriver
--------
Two Handed Mace
Physical Damage: 67-100
Elemental Damage:
Critical Strike Chance: 5.00%
Attacks per Second: 1.30
Weapon Range: 11
--------
Requirements:
Level: 61
Str: 212
--------
Sockets: B-R-R-R-R 
--------
Item Level: 73
--------
45% increased Stun Duration on Enemies
";
        }

        public static class Flask
        {
            public const string MANA =
@"Rarity: Normal
Colossal Mana Flask
--------
Recovers 200 Mana over 5.60 Seconds
Consumes 5 of 25 Charges on use
Currently has 0 Charges
--------
Requirements:
Level: 30
--------
Item Level: 30
--------
Right click to drink. Can only hold charges while in belt. Refills as you kill monsters.
";

            public const string GRANITE_MAGIC =
@"Rarity: Magic
Granite Flask of Fending
--------
Lasts 4.00 Seconds
Consumes 30 of 60 Charges on use
Currently has 0 Charges
+3000 to Armour
--------
Requirements:
Level: 27
--------
Item Level: 80
--------
Adds Knockback to Melee Attacks during Flask effect
--------
Right click to drink. Can only hold charges while in belt. Refills as you kill monsters.
";

            public const string MANA_MAGIC_QUALITY =
@"Rarity: Magic
Caustic Greater Mana Flask of Grounding
--------
Quality: +9% (augmented)
Recovers 244 (augmented) Mana over 5.60 Seconds
Consumes 12 of 32 Charges on use
Currently has 0 Charges
--------
Requirements:
Level: 12
--------
Item Level: 20
--------
60% increased Mana Recovered
Removes 15% of Mana Recovered from Life when used
Immunity to Shock during Flask effect
Removes Shock on use
--------
Right click to drink. Can only hold charges while in belt. Refills as you kill monsters.
";

            public const string TOPAZ_MAGIC =
@"Rarity: Magic
Chemist's Topaz Flask
--------
Lasts 4.00 Seconds
Consumes 22 (augmented) of 60 Charges on use
Currently has 0 Charges
+50% to Lightning Resistance
20% less Lightning Damage taken
--------
Requirements:
Level: 18
--------
Item Level: 73
--------
25% reduced Charges used
--------
Right click to drink. Can only hold charges while in belt. Refills as you kill monsters.
";
        }

        public static class Map
        {
            public const string MAP_NORMAL =
@"Rarity: Normal
Fungal Hollow Map
--------
Map Tier: 1
--------
Item Level: 61
--------
Travel to this Map by using it in the Templar Laboratory or a personal Map Device. Maps can only be used once.
";

            public const string MAP_MAGIC =
@"Rarity: Magic
Splitting Iceberg Map of Endurance
--------
Map Tier: 1
Item Quantity: +32% (augmented)
Item Rarity: +19% (augmented)
Monster Pack Size: +12% (augmented)
--------
Item Level: 63
--------
Monsters fire 2 additional Projectiles
Monsters gain an Endurance Charge on Hit
--------
Travel to this Map by using it in the Templar Laboratory or a personal Map Device.Maps can only be used once.
";
        }
    }
}