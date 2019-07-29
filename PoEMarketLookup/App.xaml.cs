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

            Task loadStats = LoadStats();
            Task<IList<string>> getLeagues = GetLeagues();

            await Task.WhenAll(loadStats, getLeagues);

            var mainWindowVM = new MainWindowViewModel()
            {
                Leagues = getLeagues.Result
            };

            MainWindow = new MainWindow()
            {
                DataContext = mainWindowVM
            };

            MainWindow.Show();
        }

        private async Task LoadStats()
        {
            string stats = null;

            if (!File.Exists(PATH_STATS))
            {
                var client = new OfficialTradeWebClient();
                stats = await client.FetchStatsAsync();

                if (stats == null)
                {
                    return;
                }

                await CreateStatsFileAsync(stats);
            }

            StatRepository.LoadStats(stats);
        }

        private async Task CreateStatsFileAsync(string stats)
        {
            using (var fr = File.CreateText(PATH_STATS))
            {
                await fr.WriteAsync(stats);
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
                    SaveLeaguesToFile(leagues);
                }
            }
            else
            {
                leagues = LoadLeaguesFromFile();
            }

            return leagues;
        }

        private void SaveLeaguesToFile(IList<string> leagues)
        {
            using (var fr = File.CreateText(PATH_LEAGUES))
            {
                foreach (var league in leagues)
                {
                    fr.WriteLine(league);
                }
            }
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
