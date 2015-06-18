using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trame
{
    /// <summary>
    /// I skeleton.
    /// </summary>
    public interface ISkeleton : IEquatable<ISkeleton>
    {
        /// <summary>
        /// Updates the skeleton.
        /// </summary>
        /// <param name="jt">Jt.</param>
        /// <param name="j">J.</param>
        void UpdateSkeleton(JointType jt, IJoint j);
        /// <summary>
        /// Gets the joint.
        /// </summary>
        /// <returns>The joint.</returns>
        /// <param name="jt">Jt.</param>
        IJoint GetJoint(JointType jt);
        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        /// <value>The root.</value>
        IJoint Root { get; set; }
        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        UInt32 Timestamp { get; }
        /// <summary>
        /// Gets the I.
        /// </summary>
        /// <value>The I.</value>
        UInt32 ID { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Trame.ISkeleton"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        bool Valid { get; set; }
        /// <summary>
        /// Gets the arms.
        /// </summary>
        /// <returns>The arms.</returns>
        ISkeleton GetArms();
        /// <summary>
        /// Clone this instance.
        /// </summary>
        ISkeleton Clone();
        /// <summary>
        /// Gets the head.
        /// </summary>
        /// <returns>The head.</returns>
        IJoint GetHead();
        IList<IJoint> Joints { get; } 
    }
}
