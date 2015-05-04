using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatEntities;
using ChatSocketService.Models;

namespace ChatInterfaces
{
    public interface ISocketCommunicationService
    {
        IPEndPoint EndPointConfiguration { get; }
        void Send(IPEndPoint address, CommunicationPacket packet);
        void Run();
    }
}
