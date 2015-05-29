using System.Net;

namespace ChatInterfaces
{
    public interface IServerChatService:IChatService
    {
        void Start();
        void Start(IPEndPoint address, string login);
        void RegisterPingResponse(string ipAddress, int port);
        void Stop();
    }
}