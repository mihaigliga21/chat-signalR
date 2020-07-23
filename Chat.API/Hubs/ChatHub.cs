using AutoMapper;
using Chat.API.Model;
using Chat.API.Model.Hubs;
using Chat.Domain.Service.ChatRoom;
using Chat.Domain.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMapper _mapper;
        private readonly IUserConnectionService _userConnectionService;
        private readonly IUserService _userService;
        private readonly IChatMessageService _chatMessageService;
        private readonly IChatRoomUserService _chatRoomUserService;

        public ChatHub(IChatRoomService chatRoomService, 
            IMapper mapper,
            IUserConnectionService userConnectionService,
            IUserService userService,
            IChatMessageService chatMessageService,
            IChatRoomUserService chatRoomUserService)
        {
            _chatRoomService = chatRoomService;
            _mapper = mapper;
            _userConnectionService = userConnectionService;
            _userService = userService;
            _chatMessageService = chatMessageService;
            _chatRoomUserService = chatRoomUserService;
        }

        public override async Task OnConnectedAsync()
        {
            if (Context?.User?.Identity?.Name != null)
            {
                var user = _userService.SearchUserByEmail(Context.User.Identity.Name).FirstOrDefault();
                if(user != null)
                {
                    _userConnectionService.SaveUserConnection(user.Id, Context.ConnectionId);
                    await this.GetChatRooms(user.Id);
                }
            }

            await this.CreateGroupsForCurrentRooms();


            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context?.User?.Identity?.Name != null)
            {
                var user = _userService.SearchUserByEmail(Context.User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    _userConnectionService.RemoveConnectionForUser(user.Id);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            var newMessage = _chatMessageService.CreateChatMessage(request.SenderUserId, request.RoomId, request.Text);

            if (newMessage == null)
                return;

            var result = _mapper.Map<ChatMessageModel>(newMessage);

            await this.Clients.Group(newMessage.ChatRoomId.ToString()).NewChatMessage(result);
        }

        public async Task CreateChatRoom(CreateChatRoomRequest roomRequest)
        {
            try
            {
                var room = _chatRoomService.CreateChatRoom(roomRequest.UserCreatorId, roomRequest.UserInvitedId, roomRequest.Name);

                var result = _mapper.Map<ChatRoomModel>(room);

                // create group
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, room.Id.ToString());

                //3. return room id to those tow users
                await this.Clients.Caller.ChatRoomCreated(result);

                var invitedUserConnected = _userConnectionService.GetConnectionForUser(roomRequest.UserInvitedId);
                if(invitedUserConnected != null)
                {
                    await this.Groups.AddToGroupAsync(invitedUserConnected.ConnectionId, room.Id.ToString());
                    await this.Clients.Client(invitedUserConnected.ConnectionId).ChatRoomCreated(result);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task GetChatRooms(Guid userId)
        {
            var rooms = _chatRoomService.GetChatRoomsForUser(userId);

            var result = _mapper.Map<ICollection<ChatRoomModel>>(rooms);

            await this.Clients.Caller.UpdatedChatRoomList(result);
        }

        public async Task GetRoomMessages(Guid roomId)
        {
            try
            {
                var messages = _chatMessageService.GetChatMessages(roomId);

                var result = _mapper.Map<ICollection<ChatMessageModel>>(messages);

                await this.Clients.Caller.RoomMessagesList(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    
        private async Task CreateGroupsForCurrentRooms()
        {
            var rooms = _chatRoomService.GetChatRooms();

            foreach (var room in rooms)
            {
                var roomUsers = _chatRoomUserService.GetChatRoomUsers(room.Id);
                foreach (var roomUser in roomUsers)
                {
                    var userConnection = _userConnectionService.GetConnectionForUser(roomUser.UserId);
                    if(userConnection != null)
                        await this.Groups.AddToGroupAsync(userConnection.ConnectionId, room.Id.ToString());
                }
            }

        }
    
    }
}
