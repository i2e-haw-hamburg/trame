using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame
{
    /// <summary>
    /// Device type.
    /// </summary>
    public enum DeviceType
    {
		/// <summary>
		/// The KINECT
		/// </summary>
        KINECT,

        /// <summary>
        /// Kinect for Windows (v2)
        /// </summary>
        KINECT2,

		/// <summary>
		/// The LEAP motion.
		/// </summary>
        LEAP_MOTION,

		/// <summary>
		/// The LEAP motion and KINECT.
		/// </summary>
        LEAP_MOTION_AND_KINECT,

		/// <summary>
		/// The EMPTY device.
		/// </summary>
        EMPTY
    }
}
