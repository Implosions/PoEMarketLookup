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

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(PATH_RESOURCES))
            {
                Directory.CreateDirectory(PATH_RESOURCES);
            }
        }
    }
}
