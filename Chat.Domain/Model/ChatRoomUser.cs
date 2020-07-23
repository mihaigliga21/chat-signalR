using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Model
{
    public class ChatRoomUser : BaseEntity
    {
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
