using System.Net;
using ChatEntities;

namespace ChatInterfaces
{
    public interface ISocketCommunicationService
    {
        IDispatchService DispatchService { get; set; }
        IPEndPoint EndPointConfiguration { get; }
        void Send(IPEndPoint address, CommunicationPacket packet);
        void Run();
        void Run(IPEndPoint endPoint);
    }
}
