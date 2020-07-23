using Chat.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chat.DataAccess.Database
{
    public class ChatContext : DbContext
    {
        private readonly string _connectionString;
        
        public ChatContext()
        {
            _connectionString = "Data Source=.;Initial Catalog=Chat;User ID=m;Password=qaz;Connect Timeout=100;MultipleActiveResultSets=true";
        }

        public ChatContext(DbContextOptions options) : base(options)
        {
        }

        public ChatContext(string connection)
        {
            if (string.IsNullOrEmpty(connection))
                throw new ArgumentException("Connection string should not be empty.");

            _connectionString = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if(!builder.IsConfigured)
                builder.UseSqlServer(_connectionString, x => x.MigrationsAssembly("Chat.DataAccess"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
    }
}
