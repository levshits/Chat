using System;

namespace ChatEntities
{
    [Serializable]
    public class ChatUser
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
    }
}
