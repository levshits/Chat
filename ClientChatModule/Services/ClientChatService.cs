using System.Collections.Generic;
using ChatEntities;
using ChatInterfaces;

namespace ClientSpecificServices.Services
{
    public class ClientChatService : IChatService
    {
        private readonly ISocketCommunicationService _service;
        public ClientChatService(ISocketCommunicationService service)
        {
            _service = service;
        }

        public ChatUser ConnectionSetting { get; private set; }
        public void RemoveUser(ChatUser user)
        {
            throw new System.NotImplementedException();
        }

        public List<ChatUser> Users { get; private set; }

        public void SendMessage(ChatUser user, string msg)
        {
            throw new System.NotImplementedException();
        }

        public void AddUser(ChatUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
