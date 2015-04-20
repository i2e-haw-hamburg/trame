using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using BeardLogger.Interface;
using BeardWire.Interface;
using BeardWire.Interface.Exceptions;

namespace TrameService
{
    abstract class Service
    {
        public const int DefaultRemotePort = 11115;
        public const string DefaultRemoteIp = "127.0.0.1";
        public const string DefaulLocalIp = "127.0.0.1";
        public const int DefaultLocalPort = 11111;
        public const string MessageTypesFileName = "MessageTypes.txt";
        protected string localIp;
        protected string remoteIp;
        protected IList<int> localPorts;
        protected int remotePort;
        protected INetworkAdapter networkAdapter;
        private Thread runner;

        private bool run = true;

        protected Service()
            : this(DefaulLocalIp, DefaultLocalPort, DefaultRemoteIp, 0)
        {

        }

        protected Service(int localPort, int remotePort)
            : this(DefaulLocalIp, localPort, DefaultRemoteIp, remotePort)
        {

        }

        protected Service(string localIp, int localPort, string remoteIp, int remotePort)
        {
            this.localIp = localIp;
            this.localPorts = new List<int> { localPort };
            this.remoteIp = remoteIp;
            this.remotePort = remotePort;
            networkAdapter = NetworkAdapterFactory.GetNewNetworkAdapter(MessageTypesFileName, LoggerFactory.GetNewDummyLogger());
            runner = new Thread(Run);
            runner.Start();
        }

        private Boolean Connect()
        {
            while (remotePort > 0)
            {
                try
                {
                    networkAdapter.ConnectToTCPRemote(IPAddress.Parse(remoteIp), remotePort);
                    return true;
                }
                catch (ConnectionFailedException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Check the status of the remote service and then press any key for another try!");
                }
                Console.ReadKey();
                Console.WriteLine("Next try!");
            }
            return false;
        }

        private void Run()
        {
            SubscribeToMessages();
            if (Connect())
            {
                Console.WriteLine("Connection established!");
            }
            foreach (var port in localPorts)
            {
                networkAdapter.StartListeningForMessagesOnTCPPort(port);
            }

            RegisterForService();

            while (run)
            {
                Thread.Sleep(200);
                Update();
            }

            UnregisterFromService();
            ShutdownAdapter();
            AfterShutdown();
        }

        protected abstract void Update();

        /// <summary>
        /// 
        /// </summary>
        protected void AfterShutdown()
        { }

        private void ShutdownAdapter()
        {
            networkAdapter.Shutdown();
        }


        public string NiceString()
        {
            var services = localPorts.Select(port => String.Format("{0}:{1}", localIp, port));
            return "Service running on " + String.Join(", ", services);
        }

        public void Close()
        {
            BeforeShutdown();
            run = false;
        }
        /// <summary>
        /// 
        /// </summary>
        protected void BeforeShutdown()
        { }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void SubscribeToMessages();
        /// <summary>
        /// 
        /// </summary>
        protected abstract void UnregisterFromService();
        /// <summary>
        /// 
        /// </summary>
        protected abstract void RegisterForService();
    }
}