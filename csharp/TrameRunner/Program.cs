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
                IJoint j = skel.Root;
                Console.WriteLine(j.Point);
            };

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
        }


    }
}
