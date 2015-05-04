using System.Net;

namespace ChatEntities
{
    public class ChatUser
    {
        public IPEndPoint IpEndPoint { get; set; }
        public string Login { get; set; }
    }
}
