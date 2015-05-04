using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;
using Microsoft.Practices.Prism.Logging;

namespace ChatSocketService.Services
{
    public class SocketCommunicationService: ISocketCommunicationService 
    {
        private readonly IDispatchService _dispatchService;
        private volatile bool IsRunned = false;
        private Thread _listenerThread;
        private Socket _socket;

        public SocketCommunicationService(IDispatchService dispatchService)
        {
            _dispatchService = dispatchService;
        }

        public IPEndPoint EndPointConfiguration { get; private set; }

        public void Send(IPEndPoint address, CommunicationPacket packet)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            bool isNeedToBeInitialised = false;

            ///TODO  Check code for multithreadthing using
            if (!IsRunned)
            {
                IsRunned = true;
                isNeedToBeInitialised = true;
            }
            if (isNeedToBeInitialised)
            {
                _listenerThread = new Thread(ListenerLoop);
                _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                if (EndPointConfiguration == null)
                {
                    EndPointConfiguration = new IPEndPoint(IPAddress.Any, 11000);
                }
                _socket.Bind(EndPointConfiguration);
                _listenerThread.Start();
            }
            
        }

        private void ListenerLoop()
        {
            _socket.Listen(10);
            while (IsRunned)
            {
                var acceptedConnection = _socket.Accept();
                ThreadPool.QueueUserWorkItem(ProcessAcceptedConnection, acceptedConnection);
            }
        }

        private void ProcessAcceptedConnection(object connection)
        {
            Socket acceptedConnection = connection as Socket;
            ///TODO
            /// send serialized object over socket
            CommunicationPacket packet = null;
            _dispatchService.Dispatch(packet);
        }
    }
}
