using STeM.Infrastructure.Exceptions;
using STeM.Infrastructure.EyeTracking;
using STeM.Infrastructure.Logging;
using STeM.Infrastructure.Overlays;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace STeM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string _configFilename = "config.json";

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var config = ApplicationConfiguration.FromJsonFile(_configFilename);
                IEyeTracker eyeTracker = null;
                switch (config.EyeTrackerName.ToUpper())
                {
                    case "TOBII4C":
                        eyeTracker = new TobiiEyeTracker4C();
                        break;
                    default:
                        throw new NotImplementedException($"Eye tracker {config.EyeTrackerName} not supported");
                }
                var logger = new TextFileLogger();
                App.Current.DispatcherUnhandledException += (sender, ev) => 
                {
#if DEBUG
                    ev.Handled = true;
#endif
                    logger.Log(ev.Exception);
                };
                var overlays = new List<OverlayBase>();
                overlays.Add(new PositionOverlay());
                overlays.Add(new TraceOverlay(500));
                var window = new MainWindow(config, eyeTracker, logger, overlays);
                window.Show();
            }
            catch (Exception exception)
            {
                throw new StartupException("Initialization error", exception);
            }
        }
    }
}
