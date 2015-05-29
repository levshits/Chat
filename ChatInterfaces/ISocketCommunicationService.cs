using System.Net;
using ChatEntities;

namespace ChatInterfaces
{
    public interface ISocketCommunicationService
    {
        IDispatchService DispatchService { get; set; }
        IPEndPoint EndPointConfiguration { get; }
        bool IsRunned { get; }
        void Send(IPEndPoint address, CommunicationPacket packet);
        void Run();
        void Run(bool isServerMode);
        void Run(IPEndPoint endPoint);
        void Run(IPEndPoint endPoint, bool isServerMode);
        void Stop();
    }
}
