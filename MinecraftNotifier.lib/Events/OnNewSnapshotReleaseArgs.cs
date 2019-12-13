using System;
using Version = MinecraftNotifier.Lib.Models.Version;

namespace MinecraftNotifier.Lib.Events
{
    /// <summary>
    /// Object representing the arguments for a new snapshot Minecraft version event
    /// </summary>
    public class OnNewSnapshotReleaseArgs : EventArgs
    {
        /// <summary>
        /// Property representing the information of the latest Minecraft snapshot version
        /// </summary>
        public Version MCVersion { get; set; }
    }
}
