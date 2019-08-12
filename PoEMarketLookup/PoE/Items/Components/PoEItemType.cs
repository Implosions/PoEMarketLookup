using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PoEMarketLookup.PoE.Items.Components
{
    public enum PoEItemType
    {
        Unknown = 0,

        // Misc
        Currency = 100,
        Gem = 101,
        Flask = 102,
        Map = 103,

        // Weapons
        // 1H
        Sword1H = 200,
        Axe1H = 201,
        Mace1H = 202,
        Dagger = 203,
        Claw = 204,
        Sceptre = 205,
        Wand = 206,

        // 2H
        Sword2H = 250,
        Axe2H = 251,
        Mace2H = 252,
        Staff = 253,
        Bow = 254,
        FishingRod = 255,

        // Accessories
        Amulet = 300,
        Ring = 301,
        Belt = 302,
        Quiver = 303,
        Jewel = 304,

        // Armor
        Helmet = 400,
        Gloves = 401,
        Boots = 402,
        BodyArmor = 403,
        Shield = 404
    }

    public static class ItemTypeUtils
    {
        private static readonly IDictionary<string, PoEItemType> _typeDefinitions = new ReadOnlyDictionary<string, PoEItemType>(
            new Dictionary<string, PoEItemType>()
            {
                { "One Handed Sword", PoEItemType.Sword1H },
                { "One Handed Axe", PoEItemType.Axe1H },
                { "One Handed Mace", PoEItemType.Mace1H },
                { "Dagger", PoEItemType.Dagger },
                { "Claw", PoEItemType.Claw },
                { "Sceptre", PoEItemType.Sceptre },
                { "Wand", PoEItemType.Wand },
                { "Two Handed Sword", PoEItemType.Sword2H },
                { "Two Handed Axe", PoEItemType.Axe2H },
                { "Two Handed Mace", PoEItemType.Mace2H },
                { "Staff", PoEItemType.Staff },
                { "Bow", PoEItemType.Bow },
                { "Fishing Rod", PoEItemType.FishingRod }
            });

        public static PoEItemType StringToItemType(string type)
        {
            if (!_typeDefinitions.ContainsKey(type))
            {
                return PoEItemType.Unknown;
            }

            return _typeDefinitions[type];
        }

        public static bool IsWeapon(this PoEItemType type)
        {
            int val = (int)type;

            return val >= 200 && val < 300;
        }

        public static bool IsAccessory(this PoEItemType type)
        {
            int val = (int)type;

            return val >= 300 && val < 400;
        }

        public static bool IsArmor(this PoEItemType type)
        {
            int val = (int)type;

            return val >= 400 && val < 500;
        }
    }
}
