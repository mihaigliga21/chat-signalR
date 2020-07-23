using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Model
{
    public class ChatRoomUserModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
