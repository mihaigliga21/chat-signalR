using Chat.Domain.Service.ChatRoom;
using Chat.Domain.Service.User;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Domain.Helper
{
    public static class DIContainer
    {
        public static IServiceCollection AddChatDomain(this IServiceCollection services)
        {
            services.AddChatServices();
            
            return services;
        }

        private static IServiceCollection AddChatServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IChatRoomUserService, ChatRoomUserService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IUserConnectionService, UserConnectionService>();

            return services;
        }
    }
}
