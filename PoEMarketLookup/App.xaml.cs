using PoEMarketLookup.PoE;
using PoEMarketLookup.ViewModels;
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

            LoadStats();

            var mainWindowVM = new MainWindowViewModel()
            {
                Leagues = await GetLeagues()
            };

            MainWindow = new MainWindow()
            {
                DataContext = mainWindowVM
            };

            MainWindow.Show();
        }

        private async void LoadStats()
        {
            if (!File.Exists(PATH_STATS))
            {
                var client = new OfficialTradeWebClient();
                string stats = await client.FetchStatsAsync();

                if (stats == null)
                {
                    return;
                }

                StatRepository.LoadStats(stats);

                using (var fr = File.CreateText(PATH_STATS))
                {
                    fr.Write(stats);
                }
            }
            else
            {
                StatRepository.LoadStats();
            }
        }

        private async Task<IList<string>> GetLeagues()
        {
            IList<string> leagues = null;

            if (!File.Exists(PATH_LEAGUES))
            {
                var client = new OfficialTradeWebClient();
                leagues = await client.FetchLeaguesAsync();

                if (leagues != null)
                {
                    using (var fr = File.CreateText(PATH_LEAGUES))
                    {
                        foreach (var league in leagues)
                        {
                            fr.WriteLine(league);
                        }
                    }
                }
            }
            else
            {
                leagues = LoadLeaguesFromFile();
            }

            return leagues;
        }

        private IList<string> LoadLeaguesFromFile()
        {
            var leagues = new List<string>();

            using (var fs = File.OpenRead(PATH_LEAGUES))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        leagues.Add(sr.ReadLine());
                    }
                }
            }

            return leagues;
        }
    }
}
