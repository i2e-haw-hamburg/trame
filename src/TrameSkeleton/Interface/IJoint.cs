using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrameSkeleton.Math;

namespace Trame
{
    /// <summary>
    /// I joint.
    /// </summary>
    public interface IJoint : IEquatable<IJoint>
    {
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <returns>The children.</returns>
        IList<IJoint> GetChildren();

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        Vector4 Orientation { get; set; }

        /// <summary>
        /// Gets or sets the point.
        /// </summary>
        /// <value>The point.</value>
        Vector3 Point { get; set; }
        /// <summary>
        /// Gets or sets the type of the joint.
        /// </summary>
        /// <value>The type of the joint.</value>
        JointType JointType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Trame.IJoint"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        bool Valid { get; set; }
        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <returns><c>true</c>, if child was added, <c>false</c> otherwise.</returns>
        /// <param name="j">J.</param>
        bool AddChild(IJoint j);
        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <returns><c>true</c>, if child was removed, <c>false</c> otherwise.</returns>
        /// <param name="jt">Jt.</param>
        bool RemoveChild(JointType jt);
        /// <summary>
        /// Finds the child.
        /// </summary>
        /// <returns>The child.</returns>
        /// <param name="jt">Jt.</param>
        IJoint FindChild(JointType jt);
        /// <summary>
        /// Deeps the find.
        /// </summary>
        /// <returns>The find.</returns>
        /// <param name="jt">Jt.</param>
        IJoint DeepFind(JointType jt);
        /// <summary>
        /// Update the specified jt and j.
        /// </summary>
        /// <param name="jt">Jt.</param>
        /// <param name="j">J.</param>
        void Update(JointType jt, IJoint j);
        /// <summary>
        /// Append the specified j.
        /// </summary>
        /// <param name="j">J.</param>
        IJoint Append(IJoint j);
        /// <summary>
        /// Clone this instance.
        /// </summary>
        IJoint Clone();
        /// <summary>
        /// Adds the children.
        /// </summary>
        /// <param name="joints">Joints.</param>
        void AddChildren(IEnumerable<IJoint> joints);
    }
}
