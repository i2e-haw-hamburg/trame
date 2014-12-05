using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Trame;
using TrameSerialization;

namespace TrameServer
{
    class Program
    {
        Serialization serial = new Serialization();
        ICameraAbstraction trame = new Trame.Trame();
        

        static void Main(string[] args)
        {
            var p = new Program();
            

            Server ws = new Server(p.SendResponse, "http://localhost:12345/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
        }
 
        public string SendResponse(HttpListenerRequest request)
        {
            return serial.Serialize(trame.GetSkeleton());    
        }
    }
  
}
