using Newtonsoft.Json.Linq;

namespace PoEMarketLookup.Web
{
    public static class PoEJsonConverter
    {
        public static string SerializeSearchParameters()
        {
            var root = new JObject
            {
                new JProperty("query")
            };
            return root.ToString();
        }
    }
}
