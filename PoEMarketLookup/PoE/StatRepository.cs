using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace PoEMarketLookup.PoE
{
    public class StatRepository
    {
        private static StatRepository _instance;

        private Dictionary<string, Stat> _statIdRepo = new Dictionary<string, Stat>();

        private StatRepository() { }

        public string GetStatId(string stat)
        {
            if (!_statIdRepo.ContainsKey(stat))
            {
                stat = stat.Remove(0, 1);

                if (!_statIdRepo.ContainsKey(stat))
                {
                    return null;
                }
            }

            return _statIdRepo[stat].Id;
        }

        public bool IsEnchantment(string stat)
        {
            if (!_statIdRepo.ContainsKey(stat))
            {
                stat = stat.Remove(0, 1);
            }

            if (!_statIdRepo.ContainsKey(stat))
            {
                return false;
            }

            return _statIdRepo[stat].IsEnchant;
        }

        public static StatRepository GetRepository()
        {
            if(_instance == null)
            {
                LoadStats();
            }

            return _instance;
        }

        public static void LoadStats(string stats = null)
        {
            var newInstance = new StatRepository();
            var dict = newInstance._statIdRepo;

            if(stats == null)
            {
                stats = File.ReadAllText(@"Resources\stats.json");
            }

            var json = JToken.Parse(stats);

            foreach(var results in json["result"])
            {
                string label = results["label"].ToString();

                if(label == "Pseudo")
                {
                    continue;
                }

                bool enchant = label == "Enchant";

                foreach(var entry in results["entries"])
                {
                    string id = entry["id"].ToString();
                    string stat = entry["text"].ToString();

                    // Remove id prefix
                    id = id.Substring(id.IndexOf('.') + 1);

                    // Fix discrepencies with affix names on glove enchants
                    if(enchant && stat.StartsWith("#% chance to Trigger"))
                    {
                        stat = stat.Remove(0, 13);
                    }

                    if (!dict.ContainsKey(stat))
                    {
                        dict.Add(stat, new Stat(id, enchant));
                    }
                }
            }
            
            _instance = newInstance;
        }

        private class Stat
        {
            public string Id { get; }
            public bool IsEnchant { get; }

            public Stat(string id, bool enchant)
            {
                Id = id;
                IsEnchant = enchant;
            }
        }
    }
}
