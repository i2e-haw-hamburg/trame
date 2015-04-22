using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AForge.Math;
using BeardWire.Interface.Exceptions;
using CoolKidsConsole;
using CoolKidsConsole.Layout;
using NetworkMessages.Trame;
using Trame;
using TrameSerialization.Serializer;

namespace TrameService
{
    class Program : Service
    {
        private ICameraAbstraction trame;
        private ProtobufSerializer serializer = new ProtobufSerializer();
        private int _sendedSkeletons = 0;
        private ILayout layout;

        private List<IPEndPoint> receivers = new List<IPEndPoint>();
        private bool run = true;

        Program(int localPort, int remotePort, DeviceType type, ILayout layout)
            : base(localPort, remotePort)
        {
            this.layout = layout;
            this.trame = new Trame.Trame();
            this.trame.SetDevice(type);
            layout.SetTitle(trame.ToString());
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
            var subscriber = new IPEndPoint(IPAddress.Parse(message.listenerip), message.listenerport);
            networkAdapter.ConnectToTCPRemote(subscriber.Address, subscriber.Port);

            receivers.Add(subscriber);
            UpdateSubscriberList();
        }

        private void UpdateSubscriberList()
        {
            var listOfReceivers = receivers.Select(s => String.Format("{0}:{1}", s.Address, s.Port)).ToList();
            layout.Update(listOfReceivers);
        }

        private void PerformUnregistration(UnregisterFromTrameMessage message, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, Guid transactionId)
        {
            var subscriber = new IPEndPoint(IPAddress.Parse(message.listenerip), message.listenerport);
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
            {}
            UpdateSubscriberList();
        }


        protected override void Update()
        {
            var service = NiceString();
            layout.Update(String.Format("{1}  Skeletons sent: {0}", _sendedSkeletons, service));
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

            try
            {
                var l = LayoutFactory.Create2ColumnLayout("Trame Service");
                var program = new Program(port, 0, dt, l);
                
                while (Console.ReadLine() != "exit")
                { }
                program.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured.");
            }
        }
    }
}
