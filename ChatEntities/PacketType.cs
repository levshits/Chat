namespace ChatSocketService.Models
{
    public enum PacketType
    {
        ENTER,
        LEAVE,
        GET_LIST,
        SEND_LIST,
        MESSAGE,
        PING
    }
}
