using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        public enum ModType
        {
            Normal,
            Crafted,
            Fractured
        }

        private const string NUM_PLACEHOLDER = "#";
        private static readonly Regex _reAffixValue = new Regex(@"(?:-)?\d+(?:.\d+)?");

        private readonly string _original;

        public string Affix { get; }
        public float[] AffixValues { get; }
        public ModType Type { get; }

        private Mod(string original, string affix, float[] values, ModType type)
        {
            _original = original;
            Affix = affix;
            AffixValues = values;
            Type = type;
        }

        public override string ToString()
        {
            return _original;
        }

        public float GetAverageValue()
        {
            float total = 0;

            foreach(float val in AffixValues)
            {
                total += val;
            }

            return total / AffixValues.Length;
        }

        public static Mod Parse(string mod)
        {
            var matches = _reAffixValue.Matches(mod);
            var values = new float[matches.Count];

            for(int i = 0; i < values.Length; i++)
            {
                float matchVal = float.Parse(matches[i].Value);

                values[i] = matchVal;
            }
            
            ModType type = ModType.Normal;

            if (mod.EndsWith("(crafted)"))
            {
                type = ModType.Crafted;
            }
            else if (mod.EndsWith("(fractured)"))
            {
                type = ModType.Fractured;
            }

            string affix = mod;

            if (type != ModType.Normal)
            {
                affix = affix.Substring(0, affix.LastIndexOf(' '));
            }

            affix = _reAffixValue.Replace(affix, NUM_PLACEHOLDER);

            return new Mod(mod, affix, values, type);
        }
    }
}
