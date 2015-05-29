using System;

namespace ChatEntities
{
    [Serializable]
    public enum PacketType
    {
        Enter,
        Leave,
        GetList,
        SendList,
        Message,
        Ping
    }
}
