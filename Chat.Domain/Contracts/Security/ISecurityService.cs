using Chat.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Contracts.Security
{
    public interface ISecurityService
    {
        UserTokens GenerateUserTokens(User user);
    }
}
