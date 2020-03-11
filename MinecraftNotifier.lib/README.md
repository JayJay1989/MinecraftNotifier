For a general overview and example, refer to https://github.com/JayJay1989/MinecraftNotifier/blob/master/README.md

```csharp
using System;
using MinecraftNotifier.Lib;
using MinecraftNotifier.Lib.Events;

namespace MinecraftNotifier.Cons
{
    class Program
    {

        public static void Main(string[] args)
        {
            MinecraftVersion mcversion = new MinecraftVersion();

            mcversion.onNewMinecraftVersionHandler += OnNewMinecraftVersionHandler;
            mcversion.onNewSnapshotReleaseHandler += OnNewSnapshotReleaseHandler;
            mcversion.onNewStableReleaseHandler += OnNewStableReleaseHandler;
            mcversion.SetInterval(5000); //Standard: 60000ms => 60seconds
            mcversion.SetTimespan(14); //Standard: 1 => 1 day
            mcversion.Start();
            Console.ReadLine();
        }

        private static void OnNewMinecraftVersionHandler(object sender, OnNewMinecraftVersionArgs e)
        {
            Console.WriteLine($"New release: {e.MCVersion.Id} ({e.MCVersion.ReleaseTime.ToLocalTime():F})");
        }

        private static void OnNewStableReleaseHandler(object sender, OnNewStableReleaseArgs e)
        {
            Console.WriteLine($"New stable release: {e.MCVersion.Id} ({e.MCVersion.ReleaseTime.ToLocalTime():F})");
        }

        private static void OnNewSnapshotReleaseHandler(object sender, OnNewSnapshotReleaseArgs e)
        {
            Console.WriteLine($"New snapshot release: {e.MCVersion.Id} ({e.MCVersion.ReleaseTime.ToLocalTime():F})");
        }
    }
}
```
