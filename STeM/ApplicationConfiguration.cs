using Newtonsoft.Json;
using STeM.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STeM
{
    /// <summary>
    /// Represents an application configuration.
    /// </summary>
    public class ApplicationConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupConfig" /> class, based on a json file.
        /// </summary>
        /// <param name="jsonfilename">The jsonfilename.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Configuration file {jsonfilename} not found.</exception>
        /// <exception cref="StartupException">Configuration file {jsonfilename} not found.
        /// Malformed configuration file</exception>
        public static ApplicationConfiguration FromJsonFile(string jsonfilename)
        {
            if (!File.Exists(jsonfilename))
            {
                throw new FileNotFoundException($"Configuration file {jsonfilename} not found.");
            }
            try
            {
                return JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(jsonfilename));
            }
            catch (Exception e)
            {
                throw new StartupException("Malformed configuration file", e);
            }
        }

        /// <summary>
        /// Gets or sets the eye tracking provider.
        /// </summary>
        [JsonProperty("EyeTracker")]
        public string EyeTrackerName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the application should log eye positions. These positions can be used for building heatmaps.
        /// </summary>
        public bool LogEyePosition { get; set; }
    }
}
