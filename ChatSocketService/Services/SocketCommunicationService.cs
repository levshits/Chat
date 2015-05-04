using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ChatEntities;
using ChatInterfaces;

namespace ChatSocketCommunicationService.Services
{
    public class SocketCommunicationService: ISocketCommunicationService 
    {
        private volatile bool _isRunned = false;
        private Thread _listenerThread;
        private Socket _socket;
        public IDispatchService DispatchService { get; set; }
        public IPEndPoint EndPointConfiguration { get; private set; }

        public void Send(IPEndPoint address, CommunicationPacket packet)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            bool isNeedToBeInitialised = false;

            ///TODO  Check code for multithreadthing using
            if (!_isRunned)
            {
                _isRunned = true;
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
            while (_isRunned)
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
            var _dispatchService = DispatchService;
            if (_dispatchService != null)
                _dispatchService.Dispatch(packet);
        }
    }
}
