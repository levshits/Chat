using ChatEntities;

namespace ChatInterfaces
{
    public interface IDispatchService
    {
        ISocketCommunicationService SocketCommunicationService { get; set; }
        IChatService ChatService { get; set; }
        void Dispatch(CommunicationPacket packet);
    }
}
