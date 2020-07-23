using System;
using System.Collections.Generic;

namespace Chat.API.Model
{
    public class ChatRoomModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public ICollection<ChatMessageModel> ChatMessages { get; set; }
        public ICollection<ChatRoomUserModel> ChatRoomUsers { get; set; }
    }
}
