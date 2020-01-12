using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace STeM.Infrastructure.Heatmaps
{
    /// <summary>
    /// Logs eye positions to a file.
    /// </summary>
    public class EyePositionLogger
    {
        private readonly string _directoryName;
        private readonly int _maxReadingsPerFile;
        private readonly int _flushSize;

        private DateTime _currentBatchStartTime;
        private int _readingNumber = 0;
        private int _totalReadings = 0;
        private (int X, int Y)[] _readings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EyePositionLogger"/> class.
        /// </summary>
        /// <param name="directoryFilename">Where to place the logs.</param>
        /// <param name="maxReadingsPerFile">The maximum number of stored positions per file.</param>
        /// <param name="flushSize">How often position data should be saved..</param>
        public EyePositionLogger(string directoryFilename = "gaze_data", int maxReadingsPerFile = 10000, int flushSize = 1000)
        {
            Directory.CreateDirectory(directoryFilename);

            _currentBatchStartTime = DateTime.Now;
            _flushSize = flushSize;
            _directoryName = directoryFilename;
            _maxReadingsPerFile = maxReadingsPerFile;

            _readings = new (int, int)[_flushSize];
        }

        /// <summary>
        /// Logs an eye position.
        /// </summary>
        public void LogPosition(Vector2 position)
        {
            if (_readingNumber == _flushSize)
            {
                Flush();
                _readingNumber = 0;
            }
            _readings[_readingNumber++] = ((int)position.X, (int)position.Y);
            ;
        }

        /// <summary>
        /// Flushes the logged positions.
        /// </summary>
        public void Flush()
        {
            File.AppendAllText(Path.Join(_directoryName, _currentBatchStartTime.Ticks + ".csv"), string.Join(";", _readings.Select(x => x.X + "," + x.Y)) + ";");
            _totalReadings += _readingNumber;
            if (_totalReadings % _maxReadingsPerFile == 0)
            {
                _currentBatchStartTime = DateTime.Now;
            }
        }
    }
}
