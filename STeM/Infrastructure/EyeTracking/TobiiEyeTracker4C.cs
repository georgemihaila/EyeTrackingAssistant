using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Eyetracking.NET;

namespace STeM.Infrastructure.EyeTracking
{
    /// <summary>
    /// Represents an API for a Tobii Eye Tracker 4C
    /// </summary>
    public class TobiiEyeTracker4C : IEyeTracker
    {
        private readonly IEyetracker _eyetracker;
        private readonly int _updateInterval;
        private readonly Dispatcher _uiDispatcher;
        private readonly double _screenWidth;
        private readonly double _screenHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="TobiiEyeTracker4C"/> class.
        /// </summary>
        /// <param name="updateIntervalms">The update interval, in ms.</param>
        /// <exception cref="ArgumentOutOfRangeException">The update interval must be >= 10ms</exception>
        /// <exception cref="Exception">Device not found.</exception>
        public TobiiEyeTracker4C(int updateIntervalms = 100)
        {
            if (updateIntervalms < 10)
                throw new ArgumentOutOfRangeException("The update interval must be >= 10ms");
            _updateInterval = updateIntervalms;
            _eyetracker = new Eyetracker();

            if (_eyetracker is null)
                throw new Exception("Device not found.");

            _uiDispatcher = Dispatcher.CurrentDispatcher;
            _screenWidth = SystemParameters.PrimaryScreenWidth;
            _screenHeight = SystemParameters.PrimaryScreenHeight;

            UpdateAsync();
        }

        private async void UpdateAsync()
        {
            await Task.Run(async () => 
            {
                while (true)
                {
                    _uiDispatcher.Invoke(() =>
                    {
                        //Eyetracking.NET returns the eye position as a unit vector, so we need to correct it
                        EyePositionChanged?.Invoke(this, new Vector2((float)_screenWidth * _eyetracker.X, (float)_screenHeight * _eyetracker.Y));
                    });
                    await Task.Delay(_updateInterval);
                }
            });
        }

        /// <summary>
        /// Occurs when the eye position changes.
        /// </summary>
        public event EventHandler<Vector2> EyePositionChanged;
    }
}
