using System;
using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;

namespace ServerSpecificServices.Services
{
    public class ServerDispatchService:IDispatchService 
    {
        public ISocketCommunicationService SocketCommunicationService { get; set; }
        public IChatService ChatService { get; set; }

        public void Dispatch(CommunicationPacket packet)
        {
            if (SocketCommunicationService == null || ChatService == null)
            {
                throw new Exception("SocketCommunication service and chat service must be setted");
            }
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
                Content = ChatService.Users,
                From = ChatService.ConnectionSetting.IpEndPoint,
                Type = PacketType.SEND_LIST
            };
            SocketCommunicationService.Send(packet.From, returnPacket);
        }

        private void processLeavePacket(CommunicationPacket packet)
        {
            ChatService.RemoveUser(packet.Content as ChatUser);
        }

        private void processEnterPacket(CommunicationPacket packet)
        {
            ChatService.AddUser(packet.Content as ChatUser);
        }
    }

    
}
