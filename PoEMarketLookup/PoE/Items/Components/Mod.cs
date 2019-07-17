using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        private const string NUM_PLACEHOLDER = "#";
        private static readonly Regex _reAffixValue = new Regex(@"(?:-)?\d+(?:.\d+)?");

        private readonly string _original;

        public string Affix { get; }
        public float[] AffixValues { get; }

        private Mod(string original, string affix, float[] values)
        {
            Affix = affix;
            AffixValues = values;
            _original = original;
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

            string text = _reAffixValue.Replace(mod, NUM_PLACEHOLDER);

            return new Mod(mod, text, values);
        }
    }
}
