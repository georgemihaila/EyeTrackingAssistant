using STeM.Infrastructure.EyeTracking;
using STeM.Infrastructure.Heatmaps;
using STeM.Infrastructure.Hooks;
using STeM.Infrastructure.Logging;
using STeM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private readonly GazeArea _gazeArea;

        private readonly GlobalKeyboardHook _globalKeyboardHook;

        private readonly EyePositionLogger _eyePositionLogger;

        /// <summary>
        /// Whether to lock the gaze area.
        /// </summary>
        private bool _lockGaze = false;

        public MainWindow(ApplicationConfiguration configuration, IEyeTracker eyeTracker, ILogger logger)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));
            if (eyeTracker is null)
                throw new ArgumentNullException(nameof(eyeTracker));
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));

            _configuration = configuration;
            _eyeTracker = eyeTracker;
            _logger = logger;
            if (_configuration.LogEyePosition)
            {
                _eyePositionLogger = new EyePositionLogger();
            }

            InitializeComponent();

            _gazeArea = new GazeArea(hiddenUntilLocked: true)
            {
                Width = 200,
                Height = 200
            };
            LayoutRoot.Children.Add(_gazeArea);
            _eyeTracker.EyePositionChanged += _eyeTracker_EyePositionChanged;
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += _globalKeyboardHook_KeyboardPressed;
        }

        private void _globalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            switch (e.KeyboardData.VirtualCode)
            {
                case 162: //L_CTRL
                    switch (e.KeyboardState)
                    {
                        case GlobalKeyboardHook.KeyboardState.KeyDown:
                            _gazeArea.PositionLocked = true;
                            break;
                        case GlobalKeyboardHook.KeyboardState.KeyUp:
                            _gazeArea.PositionLocked = false;
                            break;
                    }
                    break;
            }
        }

        private void _eyeTracker_EyePositionChanged(object sender, System.Numerics.Vector2 e)
        {
            var x = e.X - _gazeArea.ActualWidth / 2;
            var y = e.Y - _gazeArea.ActualHeight / 2;
            if (!_gazeArea.PositionLocked)
            {
                _eyePositionLogger?.LogPosition(e);

                Canvas.SetLeft(_gazeArea, x);
                Canvas.SetTop(_gazeArea, y);
            }
            EyePosition_TextBlock.Text = string.Format("{0} {1}", x, y);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _eyePositionLogger?.Flush();
            base.OnClosing(e);
        }
    }
}
