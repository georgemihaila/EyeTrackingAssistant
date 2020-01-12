using System;
using System.Collections.Generic;
using System.Text;

namespace STeM.Infrastructure.Logging
{
    /// <summary>
    /// Represents a logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        void Log(Exception e);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        public void Log(string message);

        /// <summary>
        /// Occurs when an exception is thrown.
        /// </summary>
        public event EventHandler<Exception> Exception;

        /// <summary>
        /// Occurs when information is logged.
        /// </summary>
        public event EventHandler<string> Info;
    }
}
