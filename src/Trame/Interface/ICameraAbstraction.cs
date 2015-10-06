using System;

namespace Trame
{
    public interface ICameraAbstraction
    {
        /// <summary>
        /// The method uses the configured device and don't serialize the fetched result.
        /// </summary>
        /// <returns>the current skeleton</returns>
        ISkeleton GetSkeleton();

        /// <summary>
        /// Event is fired if a new skeleton was created.
        /// </summary>
        event Action<ISkeleton> NewSkeleton;

		/// <summary>
		/// Sets the device.
		/// </summary>
		/// <param name="t">T.</param>
        void SetDevice(DeviceType t);
		/// <summary>
		/// Stop this instance.
		/// </summary>
        void Stop();

        void Start();
    }
}
