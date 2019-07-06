using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace PoEMarketLookup.PoE
{
    public class StatRepository
    {
        private static StatRepository _instance;

        private Dictionary<string, string> _statIdRepo = new Dictionary<string, string>();

        private StatRepository() { }

        public string GetStatId(string stat)
        {
            if (!_statIdRepo.ContainsKey(stat))
            {
                return null;
            }

            return _statIdRepo[stat];
        }

        public static StatRepository GetRepository()
        {
            if(_instance == null)
            {
                LoadStats();
            }

            return _instance;
        }

        private static void LoadStats()
        {
            var newInstance = new StatRepository();
            var dict = newInstance._statIdRepo;
            var file = File.ReadAllText(@"Resources\stats.json");
            var json = JToken.Parse(file);

            foreach(var results in json["result"])
            {
                foreach(var entry in results["entries"])
                {
                    string id = entry["id"].ToString();
                    string stat = entry["text"].ToString();

                    // Remove id prefix
                    id = id.Substring(id.IndexOf('.') + 1);

                    if (!dict.ContainsKey(stat))
                    {
                        dict.Add(stat, id);
                    }
                }
            }
            
            _instance = newInstance;
        }
    }
}
