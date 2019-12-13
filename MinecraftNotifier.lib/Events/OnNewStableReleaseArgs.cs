using System;
using Version = MinecraftNotifier.Lib.Models.Version;

namespace MinecraftNotifier.Lib.Events
{
    /// <summary>
    /// Object representing the arguments for a new Stable Minecraft event
    /// </summary>
    public class OnNewStableReleaseArgs : EventArgs
    {
        /// <summary>
        /// Property representing the information of the new stable Minecraft version
        /// </summary>
        public Version MCVersion { get; set; }
    }
}
