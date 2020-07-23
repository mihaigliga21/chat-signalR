using AutoMapper;
using Chat.API.Model;
using Chat.API.Model.Request;
using Chat.Domain.Model;
using System;

namespace Chat.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // user
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, UserWithTokenModel>();
            CreateMap<UserTokens, UserWithTokenModel>();

            // chat
            CreateMap<ChatRoom, ChatRoomModel>();
            CreateMap<ChatMessage, ChatMessageModel>();
            CreateMap<ChatRoomUser, ChatRoomUserModel>();
        }
    }
}
