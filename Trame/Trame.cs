using System;
using System.Threading;
using Trame.Implementation;
using Trame.Interface;
using Timer = System.Timers.Timer;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread _t = null;
        ISkeleton _last = null;
        private readonly IDevice _currentDevice;
        private DateTime _lastUpdate;
        private readonly Timer _resetTimer;
        private bool _resetInProgress = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame"/> class.
		/// </summary>
        public Trame(IDevice device)
        {
            _last = Creator.GetNewInvalidSkeleton();
            _currentDevice = device;

		    _resetTimer = new Timer(1000);
            _resetTimer.AutoReset = true;
            _lastUpdate = DateTime.Now;

            _resetTimer.Elapsed += (sender, args) =>
            {
                if (!_resetInProgress && (DateTime.Now - _lastUpdate).TotalMilliseconds > 2000)
                {
                    _resetInProgress = true;
                    // reset trame
                    _currentDevice.Start();
                    _resetInProgress = false;
                    _lastUpdate = DateTime.Now;
                }
            };
        }

        public ISkeleton GetSkeleton()
        {
            // return copy of last element
            return _last;
        }

        public event Action<ISkeleton> NewSkeleton;
        
		/// <summary>
		/// Fires the new skeleton.
		/// </summary>
		/// <param name="skeleton">Skeleton.</param>
        private void FireNewSkeleton(ISkeleton skeleton)
        {
            _last = skeleton;
		    _lastUpdate = DateTime.Now;

		    NewSkeleton?.Invoke(skeleton);
        }

        public void Stop()
        {
            Console.WriteLine("Close all resources");
            _currentDevice.NewSkeleton -= FireNewSkeleton;
            _currentDevice.Stop();
        }

        public void Start()
        {
            _currentDevice.Start();
        }

        /// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame"/>.</returns>
        public override string ToString()
        {
            return "Trame - Device: " + _currentDevice.ToString();
        }
    }
}
