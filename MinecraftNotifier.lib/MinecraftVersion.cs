using System;
using System.Timers;
using MinecraftNotifier.Lib.Core;
using MinecraftNotifier.Lib.Events;
using MinecraftNotifier.Lib.Models;

namespace MinecraftNotifier.Lib
{
    public class MinecraftVersion
    {
        private Timer timer = new Timer();
        private int _interval = 60000;
        private WebRequest request;
        private string latestStable;
        private string latestSnapshot;
        private string latestVersion;

        #region Events

        /// <summary>
        /// Fires when MinecraftNotifier receives an update of any version type from Mojang
        /// </summary>
        public EventHandler<OnNewMinecraftVersionArgs> onNewMinecraftVersionHandler;
        /// <summary>
        /// Fires when MinecraftNotifier receives an update on the stable branch from Mojang 
        /// </summary>
        public EventHandler<OnNewStableReleaseArgs> onNewStableReleaseHandler;
        /// <summary>
        /// Fires when MinecraftNotifier receives an update on the snapshot branch from Mojang  
        /// </summary>
        public EventHandler<OnNewSnapshotReleaseArgs> onNewSnapshotReleaseHandler;

        #endregion

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            timer.Interval = _interval;
            timer.Elapsed += TimerTickMethod;
            timer.Start();
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        /// <summary>
        /// Set the interval between 2 requests
        /// </summary>
        /// <param name="interval">interval in ms</param>
        public void SetInterval(int interval)
        {
            this._interval = interval;
        }

        /// <summary>
        /// Get the latest request
        /// </summary>
        /// <returns>The latest data</returns>
        public MinecraftJson LastResult()
        {
            return request.GetResult();
        }
        
        /// <summary>
        /// Timer tick handler
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">event arguments</param>
        private void TimerTickMethod(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            request = new WebRequest();
            MinecraftJson requestData = request.GetResult();

            if (requestData.GetLatest().ReleaseTime.AddDays(1) > now.ToLocalTime() && latestVersion != requestData.GetLatest().Id)
            {
                onNewMinecraftVersionHandler?.Invoke(this, new OnNewMinecraftVersionArgs()
                {
                    MCVersion = requestData.GetLatest()
                });
                latestVersion = requestData.GetLatest().Id;
            }

            if (requestData.GetLatestSnapshot().ReleaseTime.AddDays(1) > now.ToLocalTime() && latestSnapshot != requestData.GetLatestSnapshot().Id)
            {
                onNewSnapshotReleaseHandler?.Invoke(this, new OnNewSnapshotReleaseArgs()
                {
                    MCVersion = requestData.GetLatestSnapshot()
                });
                latestSnapshot = requestData.GetLatestSnapshot().Id;
            }

            if (requestData.GetLatestStable().ReleaseTime.AddDays(1) > now.ToLocalTime() && latestStable != requestData.GetLatestStable().Id)
            {
                onNewStableReleaseHandler?.Invoke(this, new OnNewStableReleaseArgs()
                {
                    MCVersion = requestData.GetLatestStable()
                });
                latestStable = requestData.GetLatestStable().Id;
            }
        }
    }
}
