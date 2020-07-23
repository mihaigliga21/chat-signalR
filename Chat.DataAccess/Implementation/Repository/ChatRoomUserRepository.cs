using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess.Implementation.Repository
{
    public class ChatRoomUserRepository : BaseRepository<ChatRoomUser>, IChatRoomUserRepository
    {
        public ChatRoomUserRepository(ChatContext context) : base(context)
        {
        }
    }
}
