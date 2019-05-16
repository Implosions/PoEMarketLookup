using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PoEMarketLookup.PoE.Parsers
{
    class Enchantments
    {
        private const string XML_FILEPATH = @"Resources\Enchantments.xml";

        private ISet<string> enchantments;

        private Enchantments(ISet<string> enchants)
        {
            enchantments = enchants;
        }

        public bool IsEnchantment(string affix)
        {
            return enchantments.Contains(affix);
        }

        public static Enchantments LoadEnchantments()
        {
            var enchants = new HashSet<string>();
            try
            {
                var xml = XDocument.Load(XML_FILEPATH);
                foreach(var type in xml.Elements())
                {
                    foreach (var enchant in type.Elements())
                    {
                        enchants.Add(enchant.Value);
                    }
                }
                
            }
            catch
            {
                // Error reading file or in file format
                // Not necessary to handle as the resulting set will be empty
            }

            return new Enchantments(enchants);
        }
    }
}
