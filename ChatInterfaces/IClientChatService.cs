using System;
using System.Net;
using ChatEntities;

namespace ChatInterfaces
{
    public interface IClientChatService:IChatService
    {
        void Connect(IPEndPoint address, string login);
        void ArriveMessage(ChatMessage message);
        event EventHandler UserListUpdated;
        event EventHandler<ChatMessage> MessageArrived;
    }
}