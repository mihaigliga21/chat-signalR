using Chat.API.Middleware;
using Chat.Domain.Contracts.Security;
using Chat.Domain.Helper;
using Chat.Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Chat.API.Service
{
    public class SecurityService : ISecurityService
    {
        private readonly AppSettings _appSettings;

        public SecurityService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserTokens GenerateUserTokens(User user)
        {
            var token = new JwtSecurityToken(
                issuer: _appSettings.Settings.Jwt.Issuer,
                audience: _appSettings.Settings.Jwt.Audience,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: GetCredentials(_appSettings.Settings.Jwt.Secret),
                notBefore: DateTime.UtcNow,
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email)
                });

            var refreshToken = GenerateRefreshToken();
            
            var userTokens = new UserTokens()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };

            return userTokens;
        }

        private static SigningCredentials GetCredentials(string asymetricKey)
        {
            var bytes = Encoding.ASCII.GetBytes(asymetricKey);
            var key = new SymmetricSecurityKey(bytes);
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        }

        private string GenerateRefreshToken()
        {
            var refreshToken = string.Empty;
            var randomNumber = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }

            return refreshToken;
        }
    }
}
