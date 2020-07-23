using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess.Implementation.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ChatContext context) : base(context)
        {
        }
    }
}
