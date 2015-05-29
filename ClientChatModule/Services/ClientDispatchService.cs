using System.Collections.Generic;
using System.Linq;
using System.Net;
using ChatEntities;
using ChatInterfaces;

namespace ClientSpecificServices.Services
{
    public class ClientDispatchService:IDispatchService 
    {
        public ISocketCommunicationService SocketCommunicationService { get; set; }
        public IChatService ChatService { get; set; }

        public void Dispatch(CommunicationPacket packet)
        {
            switch (packet.Type)
            {
                    case PacketType.Message:
                {
                    ProcessMessageRequest(packet);
                }break;
                    case PacketType.SendList:
                {
                    ProcessListRequest(packet.Content);
                }
                    break;
                    case PacketType.Ping:
                {
                    ProcessPingRequest(packet.IpAddressFrom, packet.PortFrom);
                }
                    break;
                default:
                    throw new IncorrectPacketTypeException();
            }
        }

        private void ProcessListRequest(object content)
        {
            var message = content as List<ChatUser>;
            if (message != null)
            {
                var newUser = message.Except(ChatService.Users).ToList();
                foreach (var user in newUser)
                {
                    ChatService.AddUser(user);
                }
                var oldUser = ChatService.Users.Except(message).ToList();
                foreach (var user in oldUser)
                {
                    ChatService.AddUser(user);
                }
            }
        }

        private void ProcessMessageRequest(CommunicationPacket packet)
        {
            var message = packet.Content as ChatMessage;
            if (message != null)
            {
                ((IClientChatService) (ChatService)).ArriveMessage(message);
            }
        }

        private void ProcessPingRequest(string ipAddressFrom, int portFrom)
        {
            SocketCommunicationService.Send(new IPEndPoint(IPAddress.Parse(ipAddressFrom), portFrom ),new CommunicationPacket()
            {
                Type = PacketType.Ping, 
                IpAddressFrom = SocketCommunicationService.EndPointConfiguration.Address.ToString(),
                PortFrom = SocketCommunicationService.EndPointConfiguration.Port
            } );
        }
    }
}
