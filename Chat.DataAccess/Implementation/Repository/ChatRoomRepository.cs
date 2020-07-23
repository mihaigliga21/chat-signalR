using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess.Implementation.Repository
{
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatContext context) : base(context)
        {
        }
    }
}
