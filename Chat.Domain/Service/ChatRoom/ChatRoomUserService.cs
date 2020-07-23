using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Domain.Service.ChatRoom
{
    #region interface

    public interface IChatRoomUserService
    {
        ICollection<Model.ChatRoomUser> GetChatRoomUsers(Guid roomId);
    }

    #endregion

    #region implementation

    public class ChatRoomUserService : IChatRoomUserService
    {
        #region privates

        private readonly IChatRoomUserRepository _chatRoomUserRepository;

        #endregion

        public ChatRoomUserService(IChatRoomUserRepository chatRoomUserRepository)
        {
            _chatRoomUserRepository = chatRoomUserRepository;
        }

        public ICollection<ChatRoomUser> GetChatRoomUsers(Guid roomId)
        {
            return _chatRoomUserRepository.Where(x => x.ChatRoomId == roomId).ToList();
        }

        #region private methods

        #endregion
    }

    #endregion

}
