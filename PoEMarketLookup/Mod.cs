using System.Text.RegularExpressions;

namespace PoEMarketLookup
{
    public class Mod
    {
        private const string NUM_PLACEHOLDER = "#";

        public string Affix { get; }

        private Mod(string affix)
        {
            Affix = affix;
        }

        public static Mod Parse(string mod)
        {
            var re = new Regex(@"\d+");
            string text = re.Replace(mod, NUM_PLACEHOLDER);

            return new Mod(text);
        }
    }
}
