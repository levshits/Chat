using System.Collections.Generic;
using ChatEntities;

namespace ChatInterfaces
{
    public interface IChatService
    {
        ChatUser ConnectionSetting { get; }
        void SendMessage(ChatUser user, string msg);
        void AddUser(ChatUser user);
        void RemoveUser(ChatUser user);
        List<ChatUser> Users { get; }
    }
}
