using System.Collections.Generic;
using System.Net;
using ChatEntities;

namespace ChatInterfaces
{
    public interface IChatService
    {
        void Connect();
        void Connect(IPEndPoint address, string login);
        ChatUser ConnectionSetting { get; }
        void SendMessage(ChatUser user, string msg);
        void AddUser(ChatUser user);
        void RemoveUser(ChatUser user);
        List<ChatUser> Users { get; }
    }
}
