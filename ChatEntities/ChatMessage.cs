using System;

namespace ChatEntities
{
    [Serializable]
    public class ChatMessage
    {
        public ChatUser From { get; set; }
        public ChatUser To { get; set; }
        public DateTime Time { get; set; }
        public String Text { get; set; }
    }
}
