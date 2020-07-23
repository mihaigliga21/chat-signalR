using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.DataAccess.Implementation.Repository
{
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
        private readonly ChatContext _context;

        public ChatMessageRepository(ChatContext context) : base(context)
        {
            _context = context;
        }

        public ICollection<ChatMessage> GetChatMessages(Guid roomId)
        {
            return _context.ChatMessages.Include(x=> x.User).Where(x => x.ChatRoomId == roomId).ToList();
        }
    }
}
