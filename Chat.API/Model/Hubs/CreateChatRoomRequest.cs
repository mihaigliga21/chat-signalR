using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Model.Hubs
{
    public class CreateChatRoomRequest
    {
        public string Name { get; set; }
        public Guid UserCreatorId { get; set; }
        public Guid UserInvitedId { get; set; }
    }
}
