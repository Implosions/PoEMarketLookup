using System.Text.RegularExpressions;

namespace PoEMarketLookup.PoE.Items.Components
{
    public class Mod
    {
        private const string NUM_PLACEHOLDER = "#";

        public string Affix { get; }
        public int[] AffixValues { get; }

        private Mod(string affix, int[] values)
        {
            Affix = affix;
            AffixValues = values;
        }

        public static Mod Parse(string mod)
        {
            var re = new Regex(@"\d+");
            var matches = re.Matches(mod);
            var values = new int[matches.Count];

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
