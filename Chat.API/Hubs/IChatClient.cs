using Chat.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Hubs
{
    public interface IChatClient
    {
        Task NewChatMessage(ChatMessageModel model);
        Task ChatRoomCreated(ChatRoomModel model);
        Task UpdatedChatRoomList(ICollection<ChatRoomModel> chatRooms);
        Task RoomMessagesList(ICollection<ChatMessageModel> chatMessages);
    }
}
