using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;

namespace ServerSpecificServices.Services
{
    public class ServerDispatchService:IDispatchService 
    {
        private readonly ISocketCommunicationService _socketCommunicationService;
        private readonly IChatService _serverChatService;

        public ServerDispatchService(ISocketCommunicationService socketCommunicationService,
            IChatService serverChatService)
        {
            _socketCommunicationService = socketCommunicationService;
            _serverChatService = serverChatService;
        }

        public void Dispatch(CommunicationPacket packet)
        {
            switch (packet.Type)
            {
                    case PacketType.ENTER:
                    processEnterPacket(packet);
                    break;
                    case PacketType.LEAVE:
                    processLeavePacket(packet);
                    break;
                    case PacketType.GET_LIST:
                    processGetListPacket(packet);
                    break;
                default:
                    throw new IncorrectPacketTypeException();
            }
        }

        private void processGetListPacket(CommunicationPacket packet)
        {
            var returnPacket = new CommunicationPacket()
            {
                Content = _serverChatService.Users,
                From = _serverChatService.ConnectionSetting.IpEndPoint,
                Type = PacketType.SEND_LIST
            };
            _socketCommunicationService.Send(packet.From, returnPacket);
        }

        private void processLeavePacket(CommunicationPacket packet)
        {
            _serverChatService.RemoveUser(packet.Content as ChatUser);
        }

        private void processEnterPacket(CommunicationPacket packet)
        {
            _serverChatService.AddUser(packet.Content as ChatUser);
        }
    }

    
}
