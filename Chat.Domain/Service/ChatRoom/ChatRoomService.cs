using Chat.Domain.Contracts;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using Chat.Domain.Service.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chat.Domain.Service.ChatRoom
{
    #region interface

    public interface IChatRoomService
    {
        Model.ChatRoom CreateChatRoom(Guid creatorUserId, Guid invitedUserId, string chatName = "");
        ICollection<Model.ChatRoom> GetChatRoomsForUser(Guid userId);

        ICollection<Model.ChatRoom> GetChatRooms();
    }

    #endregion

    #region implementation

    public class ChatRoomService : IChatRoomService
    {
        #region privates

        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IChatRoomUserRepository _chatRoomUserRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public ChatRoomService(
            IChatRoomRepository chatRoomRepository,
            IChatRoomUserRepository chatRoomUserRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _chatRoomRepository = chatRoomRepository;
            _chatRoomUserRepository = chatRoomUserRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public Model.ChatRoom CreateChatRoom(Guid creatorUserId, Guid invitedUserId, string chatName = "")
        {

            if (!_userService.IsUserValid(creatorUserId) && !_userService.IsUserValid(invitedUserId))
                throw new ValidationException("Invalid user Id's");

            //1. create room
            var chatRoom = new Model.ChatRoom() { Name = !string.IsNullOrEmpty(chatName) ? chatName : "roomName" };
            _chatRoomRepository.Add(chatRoom);

            //2. create room users
            var chatRoomUsers = new List<Model.ChatRoomUser>()
            {
                new Model.ChatRoomUser()
                {
                    ChatRoom = chatRoom,
                    UserId = creatorUserId
                },
                new Model.ChatRoomUser()
                {
                    ChatRoom = chatRoom,
                    UserId = invitedUserId
                }
            };
            _chatRoomUserRepository.AddRange(chatRoomUsers);

            _unitOfWork.Commit();

            return chatRoom;
        }

        public ICollection<Model.ChatRoom> GetChatRooms()
        {
            return _chatRoomRepository.GetAll().ToList();
        }

        public ICollection<Model.ChatRoom> GetChatRoomsForUser(Guid userId)
        {
            var chatRoomUsers = _chatRoomUserRepository.Where(x => x.UserId == userId);
            if (!chatRoomUsers.Any())
                return new List<Model.ChatRoom>();

            var chatRoomUserIds = chatRoomUsers.Select(x => x.ChatRoomId).Distinct().ToList();

            var rooms = _chatRoomRepository.Where(x => chatRoomUserIds.Contains(x.Id)).ToList();

            return rooms;
        }

        #region private methods

        #endregion
    }

    #endregion

}
