namespace ChatSocketService.Models
{
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
