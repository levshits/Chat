using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
