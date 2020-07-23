using Chat.Domain.Contracts;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using Chat.Domain.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Domain.Service.ChatRoom
{
    #region interface

    public interface IChatMessageService
    {
        ICollection<Model.ChatMessage> GetChatMessages(Guid roomId);
        ChatMessage CreateChatMessage(Guid userId, Guid roomId, string text);
    }

    #endregion

    #region implementation

    public class ChatMessageService : IChatMessageService
    {
        #region privates

        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        #endregion

        public ChatMessageService(
            IChatMessageRepository chatMessageRepository,
            IUnitOfWork unitOfWork,
            IUserService userService)
        {
            _chatMessageRepository = chatMessageRepository;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public ChatMessage CreateChatMessage(Guid userId, Guid roomId, string text)
        {
            var user = _userService.GetUser(userId);

            var message = new ChatMessage()
            {
                User = user,
                ChatRoomId = roomId,
                Text = text
            };

            _chatMessageRepository.Add(message);
            _unitOfWork.Commit();

            return message;
        }

        public ICollection<ChatMessage> GetChatMessages(Guid roomId)
        {
            var messages = _chatMessageRepository.GetChatMessages(roomId);

            return messages.ToList();
        }

        #region private methods

        #endregion
    }

    #endregion

}
