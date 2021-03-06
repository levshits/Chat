﻿using System;

namespace ChatEntities
{
    [Serializable]
    public class CommunicationPacket
    {
        public PacketType Type { get; set; }
        public object Content { get; set; }
        public string IpAddressFrom { get; set; }
        public int PortFrom { get; set; }
    }
}
