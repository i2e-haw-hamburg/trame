using System;
using System.Collections.Generic;

namespace Trame.Implementation.Skeleton
{
	/// <summary>
	/// Creator.
	/// </summary>
    public class Creator
    {
		/// <summary>
		/// Gets the new default skeleton.
		/// </summary>
		/// <returns>The new default skeleton.</returns>
        public static ISkeleton GetNewDefaultSkeleton()
		{
		    return GetNewDefaultSkeleton<Skeleton>();
		}
		/// <summary>
		/// Gets the default length of the bone.
		/// </summary>
		/// <returns>The default bone length.</returns>
		/// <param name="jt">Jt.</param>
        public static float GetDefaultBoneLength(JointType jt)
        {
            return Default.Lengths[jt];
        }
		/// <summary>
		/// Gets the new invalid skeleton.
		/// </summary>
		/// <returns>The new invalid skeleton.</returns>
        public static ISkeleton GetNewInvalidSkeleton()
        {
            var s = new Skeleton();
            s.Valid = false;
            return s;
        }
		/// <summary>
		/// Creates the parent.
		/// </summary>
		/// <returns>The parent.</returns>
		/// <param name="list">List.</param>
        public static IJoint CreateParent(IEnumerable<IJoint> list)
        {
            var parent = new OrientedJoint();
            foreach (var child in list)
            {
                parent.AddChild(child);
            }
            return parent;
        }
		/// <summary>
		/// Creates the head.
		/// </summary>
		/// <returns>The head.</returns>
        public static IJoint CreateHead()
        {
            return Default.CreateHead();
        }

	    public static ISkeleton GetNewDefaultSkeleton<T>() where T : ISkeleton
	    {
	        if (typeof (T) == typeof (Skeleton))
	        {
                return Default.CreateSkeleton();
            }
	        if (typeof (T) == typeof (InMapSkeleton))
	        {
	            return Default.CreateInMapSkeleton();
	        }
	        throw new Exception("No implementation found");
	    }
    }
}
