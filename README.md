<p align="center"> 
	<img src="https://lateur.pro/dev/MinecraftNotifier.png" style="max-height: 300px;">
</p>

<p align="center">
	<a href="https://www.microsoft.com/net"><img src="https://img.shields.io/badge/.NET%20standard-2.0-orange.svg" style="max-height: 300px;"></a>
	<a href="https://www.microsoft.com/net"><img src="https://img.shields.io/badge/Platform-.NET-lightgrey.svg" style="max-height: 300px;" alt="Platform: .net"></a>
	<a href="https://www.nuget.org/packages/MinecraftNotifier/"><img src="https://buildstats.info/nuget/MinecraftNotifier"></a>
</p>

## About
MinecraftNotifier is a C# library that allow interaction with Minecraft (Mojang) version API. Currenly supporting: New version, new stable release and new snapshot release. Below you can find the instruction on how to use it.

## Features
* **MinecraftAPI**
	* Get formatted information about the newest version

## Documentation
#### Doxygen
Coming soon

## Implementing
#### MinecraftNotifier.Lib.MinecraftVersion - CSharp
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

or in the [Console project](https://github.com/JayJay1989/MinecraftNotifier/tree/master/MinecraftNotifier.Cons)
