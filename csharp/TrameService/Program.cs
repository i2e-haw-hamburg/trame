using System;
using System.Collections.Generic;
using System.Net;
using AForge.Math;
using BeardWire.Interface.Exceptions;
using NetworkMessages.Trame;
using Trame;
using TrameSerialization.Serializer;

namespace TrameService
{
    class Program : Service
    {
        private Trame.ICameraAbstraction trame;
        private ProtobufSerializer serializer = new ProtobufSerializer();
        private int _sendedSkeletons = 0;

        private List<IPEndPoint> receivers = new List<IPEndPoint>();
        private bool run = true;

        Program(int localPort, int remotePort, DeviceType type)
            : base(localPort, remotePort)
        {
            this.trame = new Trame.Trame();
            this.trame.SetDevice(type);
            Console.WriteLine(trame.ToString());
        }
        
        protected override void SubscribeToMessages()
        {
            networkAdapter.SubscribeToMessagesOfType<RegisterForTrameMessage>(PerformNewRegistration);
            networkAdapter.SubscribeToMessagesOfType<UnregisterFromTrameMessage>(PerformUnregistration);
        }

        protected override void UnregisterFromService()
        {
            trame.NewSkeleton -= FireNewSkeleton;
        }

        protected override void RegisterForService()
        {
            trame.NewSkeleton += FireNewSkeleton;
        }

        private void PerformNewRegistration(RegisterForTrameMessage message, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, Guid transactionId)
        {
            // Sorry, but I play Black Flag at the moment, Aye!
            var subscriber = new IPEndPoint(IPAddress.Parse(message.listenerip), message.listenerport);
            networkAdapter.ConnectToTCPRemote(subscriber.Address, subscriber.Port);
            Console.WriteLine("Someone would like to register for some new skeletons.");
            Console.WriteLine("He sits on: " + subscriber.Address + ":" + subscriber.Port);
            Console.WriteLine("Let us send him some bones!!");

            receivers.Add(subscriber);
        }

        private void PerformUnregistration(UnregisterFromTrameMessage message, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, Guid transactionId)
        {
            var subscriber = new IPEndPoint(IPAddress.Parse(message.listenerip), message.listenerport);
            Console.WriteLine("have a nice day old friend: " + subscriber.Address + ":" + subscriber.Port);

            Bye(subscriber);
        }

        private void Bye(IPEndPoint subscriber)
        {
            receivers.Remove(subscriber);
            try
            {
                networkAdapter.DisconnectFromTCPRemote(subscriber.Address, subscriber.Port);
            }
            catch (DisconnectFailedException)
            {
                Console.WriteLine("Remote already disconnected");
            }
            Console.WriteLine("BYE");
        }


        protected override void Update()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write("Messages send: " + _sendedSkeletons);
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("Receivers: " + receivers.Count);
        }

        public void FireNewSkeleton(ISkeleton<Vector4, Vector3> skeleton)
        {
            if (receivers.Count > 0)
            {
                var m = serializer.ToMessage(skeleton);
                try
                {
                    receivers.ForEach(receiver =>
                    {
                        try
                        {
                            networkAdapter.SendMessageOverTCP(m, receiver.Address, receiver.Port);
                        }
                        catch (FailedToSendMessageException)
                        {
                            Bye(receiver);
                        }
                    });
                    _sendedSkeletons++;
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
        
        static void Main(string[] args)
        {
            int port = DefaultLocalPort;
            var dt = DeviceType.KINECT;

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
                    case "both":
                        dt = DeviceType.LEAP_MOTION_AND_KINECT;
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

            var program = new Program(port, 0, dt);
            try
            {
                Console.WriteLine(program.NiceString());
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured.");
            }
            while (Console.ReadLine() != "exit")
            { }
            program.Close();
        }
    }
}
