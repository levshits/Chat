using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;

namespace ServerSpecificServices.Services
{
    public class ServerDispatchService:IDispatchService 
    {
        private readonly ISocketCommunicationService _socketCommunicationService;
        public ServerDispatchService(ISocketCommunicationService socketCommunicationService)
        {
            _socketCommunicationService = socketCommunicationService;
        }

        public void Dispatch(CommunicationPacket packet)
        {
            switch (packet.Type)
            {
                    case PacketType.ENTER:
                    break;
                    case PacketType.LEAVE:
                    break;
                    case PacketType.GET_LIST:
                    break;
                default:
                    throw new IncorrectPacketTypeException();
            }
        }
    }

    
}
