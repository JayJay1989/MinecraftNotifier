using System;
using Version = MinecraftNotifier.Lib.Models.Version;

namespace MinecraftNotifier.Lib.Events
{
    /// <summary>
    /// Object representing the arguments for a new Minecraft version event
    /// </summary>
    public class OnNewMinecraftVersionArgs : EventArgs
    {
        /// <summary>
        /// Property representing the information of the latest Minecraft version.
        /// </summary>
        public Version MCVersion { get; set; }
    }
}
