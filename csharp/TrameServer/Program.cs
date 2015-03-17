using System;
using System.Collections.Generic;
using System.IO;
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
        Serialization jsonSerialization = new Serialization();
        private Serialization basicSerialization = new Serialization(OutputType.BASIC);
        ICameraAbstraction trame = new Trame.Trame();
        private FileStream file = new FileStream("skeletons.txt", FileMode.Create, FileAccess.Write);

        public Program()
        {
            trame.SetDevice(DeviceType.LEAP_MOTION_AND_KINECT);
        }

        static void Main(string[] args)
        {
            var p = new Program();
    

            Server ws = new Server(p.SendResponse, "http://localhost:12345/");
            ws.Run();
            Console.WriteLine("A simple webserver. Press a key to quit.");
            Console.ReadKey();
            ws.Stop();
            p.Stop();
        }

        private void Stop()
        {
            file.Close();
        }

        public string SendResponse(HttpListenerRequest request)
        {
            
            var skeleton = trame.GetSkeleton();

            SaveToFile(basicSerialization.Serialize(skeleton));
            var sr = new StreamReader(jsonSerialization.Serialize(skeleton));
            return sr.ReadToEnd();
        }

        public void SaveToFile(Stream ms)
        {
            ms.CopyTo(file);
            
            file.Flush();
        }
    }
  
}
