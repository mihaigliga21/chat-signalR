using Chat.DataAccess.Database;
using Chat.Domain.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess.Implementation.Repository
{
    public class UserConnectionRepository : BaseRepository<Domain.Model.UserConnection>, IUserConnectionRepository
    {
        public UserConnectionRepository(ChatContext context) : base(context)
        {
        }
    }
}
