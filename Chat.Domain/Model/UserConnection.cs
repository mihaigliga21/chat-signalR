using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Model
{
    public class UserConnection : BaseEntity
    {
        public string ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
