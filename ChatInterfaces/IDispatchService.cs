using ChatEntities;

namespace ChatInterfaces
{
    public interface IDispatchService
    {
        void Dispatch(CommunicationPacket packet);
    }
}
