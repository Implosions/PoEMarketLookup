using System;
using System.ComponentModel;

namespace PoEMarketLookup.PoE.Items.Components
{
    public enum PoEItemType
    {
        Unknown = 0,

        // Misc
        Currency = 100,
        Gem = 101,

        // Weapons
        // 1H
        [Description("One Handed Sword")]
        Sword1H = 200,
        [Description("One Handed Axe")]
        Axe1H = 201,
        [Description("One Handed Mace")]
        Mace1H = 202,
        [Description("Dagger")]
        Dagger = 203,
        [Description("Claw")]
        Claw = 204,
        [Description("Sceptre")]
        Sceptre = 205,
        [Description("Wand")]
        Wand = 206,

        // 2H
        [Description("Two Handed Sword")]
        Sword2H = 250,
        [Description("Two Handed Axe")]
        Axe2H = 251,
        [Description("Two Handed Mace")]
        Mace2H = 252,
        [Description("Staff")]
        Staff = 253,
        [Description("Bow")]
        Bow = 254,
        [Description("Fishing Rod")]
        FishingRod = 255,

        // Accessories
        Amulet = 300,
        Ring = 301
    }

    public static class PoEItemTypeExtensions
    {
        public static string GetDescription(this PoEItemType value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static PoEItemType GetValueFromDescription(string description)
        {
            Type enumType = typeof(PoEItemType);

            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                {
                    continue;
                }

                if (attribute.Description == description)
                {
                    return (PoEItemType)field.GetValue(null);
                }
            }

            return PoEItemType.Unknown;
        }
    }
}
