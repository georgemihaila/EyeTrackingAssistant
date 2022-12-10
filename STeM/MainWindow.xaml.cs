using STeM.Infrastructure.EyeTracking;
using STeM.Infrastructure.Heatmaps;
using STeM.Infrastructure.Hooks;
using STeM.Infrastructure.Logging;
using STeM.Infrastructure.Overlays;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STeM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IEyeTracker _eyeTracker;
        private readonly IEnumerable<OverlayBase> _overlays;

        public MainWindow(ApplicationConfiguration configuration, IEyeTracker eyeTracker, ILogger logger, IEnumerable<OverlayBase> overlays)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));
            if (eyeTracker is null)
                throw new ArgumentNullException(nameof(eyeTracker));
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));
            if (overlays is null)
                throw new ArgumentNullException(nameof(overlays));

            _configuration = configuration;
            _eyeTracker = eyeTracker;
            _logger = logger;
            _overlays = overlays;

            _eyeTracker.EyePositionChanged += _eyeTracker_EyePositionChanged;

            InitializeComponent();
        }

        private void _eyeTracker_EyePositionChanged(object sender, Vector2 e)
        {

            foreach(var overlay in _overlays)
            {
                overlay.OnEyePositionChanged(e);
                overlay.Update(LayoutRoot);
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}
