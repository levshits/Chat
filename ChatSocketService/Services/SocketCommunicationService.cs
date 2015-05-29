using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;
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

        public bool IsRunned
        {
            get { return isRunned; }
        }

        public void Send(IPEndPoint address, CommunicationPacket packet)
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(address);
                BinaryFormatter serializer = new BinaryFormatter();
                using (var ms = new MemoryStream())
                {
                    serializer.Serialize(ms, packet);
                    socket.Send(BitConverter.GetBytes(ms.Length));
                    socket.Send(ms.GetBuffer());
                    Thread.Sleep(100);
                }
        }

        public void Run(bool isServerMode)
        {
            Logger.Log("Starting server", Category.Debug, Priority.Low);

            lock (DispatchService)
            {
                if (!isRunned)
                {
                    isRunned = true;
                    listenerThread = new Thread(ListenerLoop);
                    if(!isServerMode)
                        listenerThread.IsBackground = true;
                    socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    if (EndPointConfiguration == null)
                    {
                        EndPointConfiguration = new IPEndPoint(IPAddress.Any, 0);
                    }
                    socket.Bind(EndPointConfiguration);
                    EndPointConfiguration = socket.LocalEndPoint as IPEndPoint;
                    listenerThread.Start();
                }
            }
        }
        public void Run()
        {
            Run(isServerMode: false);
        }

        public void Run(IPEndPoint endPoint)
        {
            EndPointConfiguration = endPoint;
            Run(isServerMode: false);
        }

        public void Run(IPEndPoint endPoint, bool isServerMode)
        {
            EndPointConfiguration = endPoint;
            Run(isServerMode: isServerMode);
        }

        public void Stop()
        {
            isRunned = false;
            socket.Close();
            listenerThread.Join();
            Logger.Log("Server stopped", Category.Debug, Priority.Low);
        }

        private void ListenerLoop()
        {
            socket.Listen(20);
            Logger.Log("Listening was started", Category.Debug, Priority.Low);
            while (isRunned)
            {
                try
                {
                    var acceptedConnection = socket.Accept();
                    ThreadPool.QueueUserWorkItem(ProcessAcceptedConnection, acceptedConnection);
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                catch (Exception ex)
                {
                    Logger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), Category.Exception, Priority.Low);
                }
                
            }
        }

        private void ProcessAcceptedConnection(object connection)
        {
            Socket acceptedConnection = connection as Socket;
            try
            {
                if (acceptedConnection != null)
                {
                    byte[] buffer = BitConverter.GetBytes((long) 0);
                    acceptedConnection.Receive(buffer);
                    buffer = new byte[BitConverter.ToInt64(buffer, 0)];
                    int size = acceptedConnection.Receive(buffer);
                    var serializer = new BinaryFormatter();
                    var packet = (CommunicationPacket) serializer.Deserialize(new MemoryStream(buffer, 0, size));
                    IPEndPoint remoteEndPoint = acceptedConnection.RemoteEndPoint as IPEndPoint;
                    packet.IpAddressFrom = remoteEndPoint.Address.ToString();
                    var dispatchService = DispatchService;
                    if (dispatchService != null && packet != null)
                        dispatchService.Dispatch(packet);
                }
                if (acceptedConnection != null && acceptedConnection.Connected)
                    acceptedConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), Category.Exception, Priority.Low);
            }
        }
    }
}
