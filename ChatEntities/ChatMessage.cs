using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatEntities
{
    public class ChatMessage
    {
        public ChatUser From { get; set; }
        public ChatUser To { get; set; }
        public DateTime Time { get; set; }
        public String Text { get; set; }
    }
}
