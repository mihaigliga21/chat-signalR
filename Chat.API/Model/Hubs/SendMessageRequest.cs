using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Model.Hubs
{
    public class SendMessageRequest
    {
        public Guid SenderUserId { get; set; }
        public Guid RoomId { get; set; }
        public string Text { get; set; }
    }
}
