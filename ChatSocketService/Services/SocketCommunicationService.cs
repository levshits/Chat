using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ChatEntities;
using ChatInterfaces;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace ChatSocketCommunicationService.Services
{
    public class SocketCommunicationService: ISocketCommunicationService 
    {
        private volatile bool isRunned = false;
        private Thread listenerThread;
        private Socket socket;
        [Dependency]
        public ILoggerFacade Logger { get; set; }
        public IDispatchService DispatchService { get; set; }
        public IPEndPoint EndPointConfiguration { get; private set; }

        public void Send(IPEndPoint address, CommunicationPacket packet)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            Logger.Log("Starting server", Category.Debug, Priority.Low);

            lock (DispatchService)
            {
                if(isRunned == false)
                isRunned = true;
                listenerThread = new Thread(ListenerLoop);
                socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                if (EndPointConfiguration == null)
                {
                    EndPointConfiguration = new IPEndPoint(IPAddress.Any, 11000);
                }
                socket.Bind(EndPointConfiguration);
                listenerThread.Start();
            }
            
        }

        public void Run(IPEndPoint endPoint)
        {
            EndPointConfiguration = endPoint;
            this.Run();
        }

        private void ListenerLoop()
        {
            socket.Listen(10);
            Logger.Log("Listening was started", Category.Debug, Priority.Low);
            while (isRunned)
            {
                var acceptedConnection = socket.Accept();
                ThreadPool.QueueUserWorkItem(ProcessAcceptedConnection, acceptedConnection);
            }
        }

        private void ProcessAcceptedConnection(object connection)
        {
            Socket acceptedConnection = connection as Socket;
            ///TODO
            /// send serialized object over socket
            CommunicationPacket packet = null;
            var _dispatchService = DispatchService;
            if (_dispatchService != null)
                _dispatchService.Dispatch(packet);
        }
    }
}
