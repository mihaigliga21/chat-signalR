using System.Collections.Generic;

namespace Chat.Domain.Model
{
    public class ChatRoom : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ChatRoomUser> ChatRoomUsers { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
