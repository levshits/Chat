using System;
using System.Net;
using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace ServerSpecificServices.Services
{
    public class ServerDispatchService:IDispatchService 
    {
        public ISocketCommunicationService SocketCommunicationService { get; set; }
        public IChatService ChatService { get; set; }
        [Dependency]
        public ILoggerFacade Logger { get; set; }

        public void Dispatch(CommunicationPacket packet)
        {
            if (SocketCommunicationService == null || ChatService == null)
            {
                throw new Exception("SocketCommunication service and chat service must be setted");
            }
            switch (packet.Type)
            {
                case PacketType.Enter:
                    ProcessEnterPacket(packet);
                    break;
                case PacketType.Leave:
                    ProcessLeavePacket(packet);
                    break;
                case PacketType.GetList:
                    ProcessGetListPacket(packet);
                    break;
                case PacketType.Ping:
                    ProcessPingResponse(packet);
                    break;
                default:
                    Logger.Log(packet.Content.ToString(), Category.Debug, Priority.Low);
                    break;
            }
        }

        private void ProcessPingResponse(CommunicationPacket packet)
        {
            ((IServerChatService)ChatService).RegisterPingResponse(packet.IpAddressFrom, packet.PortFrom);
        }

        private void ProcessGetListPacket(CommunicationPacket packet)
        {
            var returnPacket = new CommunicationPacket()
            {
                Content = ChatService.Users,
                IpAddressFrom = ChatService.ConnectionSetting.IpAddress,
                Type = PacketType.SendList
            };
            SocketCommunicationService.Send(new IPEndPoint(IPAddress.Parse(packet.IpAddressFrom), packet.PortFrom), returnPacket);
        }

        private void ProcessLeavePacket(CommunicationPacket packet)
        {
            ChatService.RemoveUser(packet.Content as ChatUser);
        }

        private void ProcessEnterPacket(CommunicationPacket packet)
        {
            ChatService.AddUser(packet.Content as ChatUser);
        }
    }

    
}
