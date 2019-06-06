using Newtonsoft.Json.Linq;

namespace PoEMarketLookup.Web
{
    public static class PoEJsonConverter
    {
        public static string SerializeSearchParameters()
        {
            var query = new JObject()
            {
                new JProperty("status", "any")
            };

            var root = new JObject
            {
                new JProperty("query", query)
            };

            return root.ToString();
        }
    }
}
