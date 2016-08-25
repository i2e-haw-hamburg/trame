using System;

namespace Trame.Interface
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
		/// Stops this instance.
		/// </summary>
        void Stop();

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();
    }
}
