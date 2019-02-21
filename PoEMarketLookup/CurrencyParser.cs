namespace PoEMarketLookup
{
    public class CurrencyParser
    {
        private static readonly string SECTION_SEPARATOR = new string('-', 8);

        private string[] itemSections;

        public CurrencyParser(string rawItemText)
        {
            itemSections = rawItemText.Split(SECTION_SEPARATOR.ToCharArray());
        }

        public Currency Parse()
        {
            string[] itemInfo = itemSections[0].Split('\n');
            string itemBase = itemInfo[1].Trim();

            return new Currency(itemBase, 0);
        }
    }
}
