using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;

namespace ClientSpecificServices.Services
{
    public class ClientDispatchService:IDispatchService 
    {
        private readonly ISocketCommunicationService _socketCommunicationService;
        public ClientDispatchService(ISocketCommunicationService socketCommunicationService)
        {
            _socketCommunicationService = socketCommunicationService;
        }

        public void Dispatch(CommunicationPacket packet)
        {
            switch (packet.Type)
            {
                    case PacketType.MESSAGE:
                    break;
                    case PacketType.PING:
                    break;
                default:
                    throw new IncorrectPacketTypeException();
            }
        }
    }
}
