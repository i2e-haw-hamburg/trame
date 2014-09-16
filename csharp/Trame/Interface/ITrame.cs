using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trame;

namespace Trame
{
    public interface ITrame
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISkeleton GetSkeleton();

        /// <summary>
        /// 
        /// </summary>
        event Action<ISkeleton> NewSkeleton;
    }
}
