using System;

namespace Chat.API.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class UserWithTokenModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
