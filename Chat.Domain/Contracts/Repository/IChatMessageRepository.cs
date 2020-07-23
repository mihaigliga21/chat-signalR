using Chat.Domain.Model;
using System;
using System.Collections.Generic;

namespace Chat.Domain.Contracts.Repository
{
    public interface IChatMessageRepository : IRepository<ChatMessage>
    {
        ICollection<ChatMessage> GetChatMessages(Guid roomId);
    }
}
