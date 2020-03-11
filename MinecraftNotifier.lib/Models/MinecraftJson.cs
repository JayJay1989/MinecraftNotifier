using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MinecraftNotifier.Lib.Models
{
    /// <summary>
    /// MinecraftJson Model
    /// </summary>
    public partial class MinecraftJson
    {
        /// <summary>
        /// The latest versions
        /// </summary>
        [JsonProperty("latest")]
        public Latest Latest { get; set; }

        /// <summary>
        /// All the versions
        /// </summary>
        [JsonProperty("versions")]
        public List<Version> Versions { get; set; }
    }

    /// <summary>
    /// Latest Model
    /// </summary>
    public class Latest
    {
        /// <summary>
        /// Latest release version
        /// </summary>
        [JsonProperty("release")]
        public string Release { get; set; }

        /// <summary>
        /// Latest snapshot version
        /// </summary>
        [JsonProperty("snapshot")]
        public string Snapshot { get; set; }
    }

    /// <summary>
    /// Version Model
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Release id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// release version
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Url with game settings
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Time published
        /// </summary>
        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Official release time
        /// </summary>
        [JsonProperty("releaseTime")]
        public DateTimeOffset ReleaseTime { get; set; }
    }

    /// <summary>
    /// Class representing the MinecraftJson
    /// </summary>
    public partial class MinecraftJson
    {
        /// <summary>
        /// Get Version information  by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Version GetById(string id)
        {
            return Versions.Find(item => item.Id == id);
        }

        /// <summary>
        /// Get latest version
        /// </summary>
        /// <returns><see cref="Versions"/>the version</returns>
        public Version GetLatest()
        {
            return Versions
                .Find(version => version.ReleaseTime == Versions.Max(item => item.ReleaseTime));
        }

        /// <summary>
        /// Get latest Snapshot Version
        /// </summary>
        /// <returns><see cref="Versions"/>the version</returns>
        public Version GetLatestSnapshot()
        {
            return Versions.Find(version => version.ReleaseTime == Versions.Where(item => item.Type == "snapshot").Max(item => item.ReleaseTime));
        }

        /// <summary>
        /// Get latest stable
        /// </summary>
        /// <returns><see cref="Versions"/>the version</returns>
        public Version GetLatestStable()
        {
            return Versions
                .Find(version => version.ReleaseTime == Versions.Where(item => item.Type == "release").Max(item => item.ReleaseTime));
        }
    }
}
