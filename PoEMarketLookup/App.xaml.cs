using PoEMarketLookup.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PoEMarketLookup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string PATH_RESOURCES = @"Resources";
        private const string PATH_STATS = PATH_RESOURCES + @"\stats.json";
        private const string PATH_LEAGUES = PATH_RESOURCES + @"\leagues.txt";

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(PATH_RESOURCES))
            {
                Directory.CreateDirectory(PATH_RESOURCES);
            }

            if (!File.Exists(PATH_STATS))
            {
                var client = new OfficialTradeWebClient();
                string stats = await client.FetchStatsAsync();

                if (stats != null)
                {
                    using(var fr = File.CreateText(PATH_STATS))
                    {
                        fr.Write(stats);
                    }
                }
            }

            if (!File.Exists(PATH_LEAGUES))
            {
                var client = new OfficialTradeWebClient();
                IList<string> leagues = await client.FetchLeaguesAsync();

                if (leagues != null)
                {
                    using(var fr = File.CreateText(PATH_LEAGUES))
                    {
                        foreach(var league in leagues)
                        {
                            fr.WriteLine(league);
                        }
                    }
                }
            }
        }
    }
}
