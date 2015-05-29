using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatEntities
{
    public class UpdateUserListPacket
    {
        public ChatUser User { get; set; }
        public OperationType Type { get; set; }
    }

    public enum OperationType
    {
        Add,
        Remove
    }
}
