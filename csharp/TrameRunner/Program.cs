using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trame;

namespace TrameRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            ICameraAbstraction trame = new Trame.Trame();
            
            trame.NewSkeleton += (skel) => {
                Console.WriteLine(skel);
            };

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
        }
    }
}
