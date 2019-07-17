using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        public enum ModType
        {
            Normal,
            Crafted
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

        public static Mod Parse(string mod)
        {
            var matches = _reAffixValue.Matches(mod);
            var values = new float[matches.Count];

            for(int i = 0; i < values.Length; i++)
            {
                float matchVal = float.Parse(matches[i].Value);

                values[i] = matchVal;
            }

            string affix = _reAffixValue.Replace(mod, NUM_PLACEHOLDER);
            ModType type = ModType.Normal;

            if (mod.EndsWith("(crafted)"))
            {
                type = ModType.Crafted;
                affix = affix.Substring(0, affix.Length - 10);
            }

            return new Mod(mod, affix, values, type);
        }
    }
}
