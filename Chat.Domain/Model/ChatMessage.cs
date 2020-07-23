using System;

namespace Chat.Domain.Model
{
    public class ChatMessage : BaseEntity
    {
        public string Text { get; set; }

        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
