using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PoEMarketLookup.Web
{
    public static class JsonExtensions
    {
        public static JProperty CreateProperty(this JObject obj, string propName)
        {
            var prop = new JProperty(propName);
            obj.Add(prop);

            return prop;
        }

        public static JObject CreateObject(this JProperty prop)
        {
            var obj = new JObject();
            prop.Value = obj;

            return obj;
        }

        public static JObject CreateObject(this JArray a)
        {
            var obj = new JObject();
            a.Add(obj);

            return obj;
        }

        public static JArray CreateArray(this JProperty prop)
        {
            var a = new JArray();
            prop.Value = a;

            return a;
        }

        public static JProperty SetValue(this JProperty prop, JObject obj)
        {
            prop.Value = obj;

            return prop;
        }

        public static JArray SetValue(this JProperty prop, JArray a)
        {
            prop.Value = a;

            return a;
        }

        public static JArray AddAll(this JArray a, ICollection<JObject> collection)
        {
            foreach(var obj in collection)
            {
                a.Add(obj);
            }

            return a;
        }
    }
}
