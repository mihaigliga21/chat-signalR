using System.Collections.Generic;

namespace Chat.Domain.Model
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<ChatRoomUser> ChatRoomUsers { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public UserConnection UserConnection { get; set; }
    }

    public class UserTokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
