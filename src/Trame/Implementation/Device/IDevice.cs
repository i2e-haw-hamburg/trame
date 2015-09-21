using System;

namespace Trame.Implementation.Device
{
	/// <summary>
	/// I device.
	/// </summary>
    interface IDevice
    {
		/// <summary>
		/// Gets the skeleton.
		/// </summary>
		/// <returns>The skeleton.</returns>
        ISkeleton GetSkeleton();
		/// <summary>
		/// Gets the skeleton.
		/// </summary>
		/// <returns>The skeleton.</returns>
		/// <param name="baseSkeleton">Base skeleton.</param>
        ISkeleton GetSkeleton(ISkeleton baseSkeleton);
		/// <summary>
		/// Stop this instance.
		/// </summary>
        void Stop();
        void Start();
        /// <summary>
        /// Occurs when new skeleton.
        /// </summary>
        event Action<ISkeleton> NewSkeleton;
    }
}
