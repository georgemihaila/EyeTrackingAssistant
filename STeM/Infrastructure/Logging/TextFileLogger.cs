using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STeM.Infrastructure.Logging
{
    /// <summary>
    /// Represents a logger that logs to a file.
    /// </summary>
    public class TextFileLogger : ILogger
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFileLogger"/> class.
        /// </summary>
        public TextFileLogger()
        {
            _path = "application_log.log";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFileLogger"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public TextFileLogger(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Occurs when an exception is thrown.
        /// </summary>
        public event EventHandler<Exception> Exception;

        /// <summary>
        /// Occurs when information is logged.
        /// </summary>
        public event EventHandler<string> Info;

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        public void Log(string message) 
        {
            var result = string.Format("{0} - {1}\r\n", DateTime.Now, message);
            File.AppendAllText(_path, result);
            Info?.Invoke(this, result);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        public void Log(Exception e)
        {
            var result = string.Format("{0} - {1}\r\n", DateTime.Now, e.ToString());
            File.AppendAllText(_path, result);
            Exception?.Invoke(this, e);
        }
    }
}
