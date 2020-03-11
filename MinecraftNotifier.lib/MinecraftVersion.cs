using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MinecraftNotifier.Lib.Core;
using MinecraftNotifier.Lib.Events;
using MinecraftNotifier.Lib.Models;
using Timer = System.Timers.Timer;

namespace MinecraftNotifier.Lib
{
    /// <summary>
    /// Class representing MinecraftVersion
    /// </summary>
    public class MinecraftVersion
    {
        private Timer _timer = new Timer();
        private int _interval = 60000;
        private WebRequest _request;
        private string _latestStable;
        private string _latestSnapshot;
        private string _latestVersion;
        private int _timespan = 1;

        #region Events

        /// <summary>
        /// Fires when MinecraftNotifier receives an update of any version type from Mojang
        /// </summary>
        public EventHandler<OnNewMinecraftVersionArgs> OnNewMinecraftVersionHandler;
        /// <summary>
        /// Fires when MinecraftNotifier receives an update on the stable branch from Mojang 
        /// </summary>
        public EventHandler<OnNewStableReleaseArgs> OnNewStableReleaseHandler;
        /// <summary>
        /// Fires when MinecraftNotifier receives an update on the snapshot branch from Mojang  
        /// </summary>
        public EventHandler<OnNewSnapshotReleaseArgs> OnNewSnapshotReleaseHandler;

        #endregion

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            _timer.Elapsed += TimerTickMethod;
            _timer.Start();
            _timer.Interval = _interval;
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
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
        /// Set the timespan to search for new updates
        /// </summary>
        /// <param name="timespan">timespan in days</param>
        public void Timespan(int timespan)
        {
            _timespan = timespan;
        }

        /// <summary>
        /// Get the latest request
        /// </summary>
        /// <returns>The latest data</returns>
        public async Task<MinecraftJson> LastResult()
        {

            MinecraftJson result = await _request.DeserializeStreamAsync(CancellationToken.None);
            return result;
        }
        
        /// <summary>
        /// Timer tick handler
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">event arguments</param>
        private async void TimerTickMethod(object sender, ElapsedEventArgs e)
        {
            DateTime now = DateTime.Now;
            _request = new WebRequest();
            MinecraftJson requestData = await _request.DeserializeStreamAsync(CancellationToken.None);

            if (requestData.GetLatest().ReleaseTime.AddDays(_timespan) > now.ToLocalTime() && _latestVersion != requestData.GetLatest().Id)
            {
                OnNewMinecraftVersionHandler?.Invoke(this, new OnNewMinecraftVersionArgs()
                {
                    MCVersion = requestData.GetLatest()
                });
                _latestVersion = requestData.GetLatest().Id;
            }

            if (requestData.GetLatestSnapshot().ReleaseTime.AddDays(_timespan) > now.ToLocalTime() && _latestSnapshot != requestData.GetLatestSnapshot().Id)
            {
                OnNewSnapshotReleaseHandler?.Invoke(this, new OnNewSnapshotReleaseArgs()
                {
                    MCVersion = requestData.GetLatestSnapshot()
                });
                _latestSnapshot = requestData.GetLatestSnapshot().Id;
            }

            if (requestData.GetLatestStable().ReleaseTime.AddDays(_timespan) > now.ToLocalTime() && _latestStable != requestData.GetLatestStable().Id)
            {
                OnNewStableReleaseHandler?.Invoke(this, new OnNewStableReleaseArgs()
                {
                    MCVersion = requestData.GetLatestStable()
                });
                _latestStable = requestData.GetLatestStable().Id;
            }
        }
    }
}
