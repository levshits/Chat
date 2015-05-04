using System;
using System.Net;
using ChatSocketService.Models;

namespace ChatEntities
{
    [Serializable]
    public class CommunicationPacket
    {
        public PacketType Type { get; set; }
        public object Content { get; set; }
        public IPEndPoint From { get; set; }
    }
}
