using System;
using ChatSocketService.Models;

namespace ChatEntities
{
    [Serializable]
    public class CommunicationPacket
    {
        public PacketType Type { get; set; }
        public object Content { get; set; }
    }
}
