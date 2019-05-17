using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        private const string NUM_PLACEHOLDER = "#";

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
            var re = new Regex(@"\d+");
            var matches = re.Matches(mod);
            var values = new float[matches.Count];

            for(int i = 0; i < values.Length; i++)
            {
                int matchVal = int.Parse(matches[i].Value);

                values[i] = matchVal;
            }

            string text = re.Replace(mod, NUM_PLACEHOLDER);

            return new Mod(text, values);
        }
    }
}
