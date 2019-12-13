using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MinecraftNotifier.Lib.Models
{
    public partial class MinecraftJson
    {
        [JsonProperty("latest")]
        public Latest Latest { get; set; }

        [JsonProperty("versions")]
        public List<Version> Versions { get; set; }
    }

    public class Latest
    {
        [JsonProperty("release")]
        public string Release { get; set; }

        [JsonProperty("snapshot")]
        public string Snapshot { get; set; }
    }

    public partial class Version
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("releaseTime")]
        public DateTimeOffset ReleaseTime { get; set; }
    }

    public partial class MinecraftJson
    {
        public static MinecraftJson FromJson(string json) => JsonConvert.DeserializeObject<MinecraftJson>(json, Converter.Settings);

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

    public static class Serialize
    {
        public static string ToJson(this MinecraftJson self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            },
        };
    }
}
