using System;
using System.Collections.Generic;
using ChatEntities;
using ChatInterfaces;

namespace ServerSpecificServices.Services
{
    public class ServerChatService:IChatService
    {
        public ChatUser ConnectionSetting { get; private set; }
        public List<ChatUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(ChatUser user)
        {
            throw new NotImplementedException();
        }

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
