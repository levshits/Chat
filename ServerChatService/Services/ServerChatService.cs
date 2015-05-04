using System;
using System.Collections.Generic;
using ChatEntities;
using ChatInterfaces;

namespace ServerSpecificServices.Services
{
    public class ServerChatService:IChatService
    {
        public ChatUser ConnectionSetting { get; private set; }
        public void RemoveUser(ChatUser user)
        {
            throw new NotImplementedException();
        }

        public List<ChatUser> Users { get; private set; }

        public void AddUser(ChatUser user)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ChatUser user, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
