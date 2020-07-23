using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Model
{
    public class ChatMessageModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Created { get ; set; }
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
    }
}
