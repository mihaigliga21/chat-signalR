using Chat.DataAccess.Database;
using Chat.DataAccess.Implementation;
using Chat.DataAccess.Implementation.Repository;
using Chat.Domain.Contracts;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.DataAccess.Helper
{
    public static class DIContainer
    {
        public static IServiceCollection AddChatDataAccess(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<ChatContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionStrings.ChatDatabase);
            });

            services.AddChatRepository();

            return services;
        }

        public static IServiceCollection AddChatRepository(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatRoomUserRepository, ChatRoomUserRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();

            return services;
        }
    }
}
