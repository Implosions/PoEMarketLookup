using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        private const string NUM_PLACEHOLDER = "#";
        private static readonly Regex _reAffixValue = new Regex(@"\d+(?:.\d+)?");

        public string Affix { get; }
        public float[] AffixValues { get; }

        private Mod(string affix, float[] values)
        {
            Affix = affix;
            AffixValues = values;
        }

        public override string ToString()
        {
            string affix = Affix;

            foreach(int val in AffixValues)
            {
                int placeholderPos = affix.IndexOf(NUM_PLACEHOLDER);

                affix = affix.Substring(0, placeholderPos) + val.ToString() + affix.Substring(placeholderPos + 1);
            }

            return affix;
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

            return new Mod(text, values);
        }
    }
}
