using System;
using System.Collections.Generic;
using System.Net;
using ChatEntities;
using ChatInterfaces;
using ChatSocketService.Models;

namespace ServerSpecificServices.Services
{
    public class ServerChatService:IChatService
    {
        private readonly ISocketCommunicationService _socketCommunicationService;
        private readonly IDispatchService _dispatchService;

        public ServerChatService(ISocketCommunicationService socketCommunicationService, IDispatchService dispatchService)
        {
            Users = new List<ChatUser>();
            _socketCommunicationService = socketCommunicationService;
            _dispatchService = dispatchService;
            _socketCommunicationService.DispatchService = _dispatchService;
            _dispatchService.SocketCommunicationService = _socketCommunicationService;
            _dispatchService.ChatService = this;
        }

        public void Connect()
        {
            Connect(null, "Server");
        }

        public void Connect(IPEndPoint address, string login)
        {
            _socketCommunicationService.Run(address);
            ConnectionSetting = new ChatUser();
            ConnectionSetting.IpEndPoint = _socketCommunicationService.EndPointConfiguration;
            ConnectionSetting.Login = login;
        }

        public ChatUser ConnectionSetting { get; private set; }
        public void RemoveUser(ChatUser user)
        {
            Users.Remove(user);
        }

        public List<ChatUser> Users { get; private set; }

        public void AddUser(ChatUser user)
        {
            Users.Add(user);
        }

        public void SendMessage(ChatUser user, string msg)
        {
            _socketCommunicationService.Send(user.IpEndPoint, new CommunicationPacket
            {
                Type = PacketType.MESSAGE, 
                Content = msg, 
                From = ConnectionSetting.IpEndPoint
            });
        }
    }
}
