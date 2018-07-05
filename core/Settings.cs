using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace couchbase.Core
{
    /// <summary>
    /// Class for accessing the applications settings.
    /// </summary>
    public class Settings {

        /// <summary>
        /// The applications settings.
        /// </summary>
        /// <returns>The settings</returns>
        public IConfiguration Configuration
        {
            get;
        }

        /// <summary>
        /// The username to connect with to the couchbase cluster.
        /// </summary>
        /// <returns>The username</returns>
        public string Username
        {
            get => Configuration["username"];
            set => Configuration["username"] = value;
        }

        /// <summary>
        /// The passworf to connect with to the couchbase cluster.
        /// </summary>
        /// <returns>The password</returns>
        public string Password
        {
            get => Configuration["password"];
            set => Configuration["password"] = value;
        }

        /// <summary>
        /// The uri to connect to the couchbase cluster.
        /// </summary>
        /// <returns>The uri</returns>
        public string Uri
        {
            get => Configuration["uri"];
            set => Configuration["uri"] = value;
        }

        /// <summary>
        /// Create a new settings object.
        /// </summary>
        internal Settings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json");

            Configuration = builder.Build();
        }
    }
}