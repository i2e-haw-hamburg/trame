using System;
using System.Collections.Generic;
using System.Net;
using BeardLogger.Interface;
using BeardWire.Interface;
using NetworkMessages.Trame;
using Trame;

namespace TrameService
{
    class Program
    {
        private const int DefaultPort = 11111;
        private const string MessageTypesFileName = "C:\\Users\\squad\\git\\trame\\csharp\\TrameNetworkMessages\\MessageTypes.txt";
        private int port;
        private INetworkAdapter networkAdapter;
        private Trame.ICameraAbstraction trame;

        private List<IPEndPoint> receivers = new List<IPEndPoint>();
        private bool run;

        public Program()
            : this(DefaultPort, DeviceType.EMPTY)
        {
            
        }

        public Program(int port, DeviceType type)
        {
            this.port = port;
            this.trame = new Trame.Trame();
            this.trame.SetDevice(type);
            this.networkAdapter = NetworkAdapterFactory.GetNewNetworkAdapter(MessageTypesFileName, LoggerFactory.GetNewDummyLogger());
            SubscribeToMessages();
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private void SubscribeToMessages()
        {
            networkAdapter.SubscribeToMessagesOfType<RegisterForTrameMessage>(Register);
            networkAdapter.SubscribeToMessagesOfType<UnregisterFromTrameMessage>(Unregister);
        }

        private void Register(RegisterForTrameMessage message, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, Guid transactionId)
        {
            // Sorry, but I play Black Flag at the moment, Aye!
            Console.WriteLine("Someone would like to register for some new skeletons.");
            Console.WriteLine("He sits on: " + remoteEndPoint.Address + ":" + remoteEndPoint.Port);
            Console.WriteLine("Let us send him some bones!!");

            receivers.Add(remoteEndPoint);
        }

        private void Unregister(UnregisterFromTrameMessage message, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, Guid transactionId)
        {
            Console.WriteLine("have a nice day old friend: " + remoteEndPoint.Address + ":" + remoteEndPoint.Port);
            Console.WriteLine("BYE");

            receivers.Remove(remoteEndPoint);
        }

        public void Run()
        {
            trame.NewSkeleton += FireNewSkeleton;

            while (run)
            {
            }

        }

        public void FireNewSkeleton(ISkeleton skeleton)
        {
            if (receivers.Count > 0)
            {
                var m = skeleton.ToMessage();
                receivers.ForEach(receiver =>
                {
                    networkAdapter.SendMessageOverTCP(m, receiver.Address, receiver.Port);
                });
            }
        }

        public string NiceString()
        {
            return "Trame Service running on " + networkAdapter.LocalAddress.ToString() + ":" + port;
        }

        public void Close()
        {
            run = false;
            trame.Stop();
        }
        
        static void Main(string[] args)
        {
            int port = DefaultPort;
            DeviceType dt = DeviceType.EMPTY;
            
            if (args.Length >= 1)
            {
                string type = args[0];
                switch (type)
                {
                    case "kinect":
                        dt = DeviceType.KINECT;
                        break;
                    case "leap":
                        dt = DeviceType.LEAP_MOTION;
                        break;
                    default:
                        dt = DeviceType.EMPTY;
                        break;
                }
            }
            if (args.Length >= 2)
            {
                int.TryParse(args[1], out port);
            }
            var program = new Program(port, dt);
            Console.WriteLine(program.NiceString());
            program.Run();

            Console.ReadKey();
            program.Close();
        }

        
    }
}
