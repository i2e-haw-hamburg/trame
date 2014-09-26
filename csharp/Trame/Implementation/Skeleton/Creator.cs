using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trame.Implementation.Skeleton
{
    class Creator
    {
        public static ISkeleton DefaultSkeleton
        {
            get
            {
                return new Skeleton();
            }
        }

        public static ISkeleton InvalidSkeleton { 
            get 
            {
                var s = new Skeleton();
                s.Valid = false;
                return s;
            } 
        }


    }
}
