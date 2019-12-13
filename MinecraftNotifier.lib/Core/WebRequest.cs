using System;
using System.Net;
using MinecraftNotifier.Lib.Models;

namespace MinecraftNotifier.Lib.Core
{
    /// <summary>
    /// Class representing WebRequest
    /// </summary>
    public class WebRequest
    {
        /// <summary>
        /// Data
        /// </summary>
        private MinecraftJson _manifest;

        /// <summary>
        /// Constructor for creating a web request
        /// </summary>
        public WebRequest()
        {
            using (WebClient client = new WebClient() { Encoding = System.Text.Encoding.UTF8 })
            {
                string s = null;
                try
                {
                    s = client.DownloadString("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                if (s == null) return;

                this._manifest = MinecraftJson.FromJson(s);
            }
        }

        /// <summary>
        /// Get the result
        /// </summary>
        /// <returns>The result</returns>
        public MinecraftJson GetResult()
        {
            return this._manifest;
        }

    }
}
