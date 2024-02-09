using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tribe_OAuth2_BE_Demo.DataAdapters;
using Tribe_OAuth2_BE_Demo.Enums;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public class AuthService : IAuthService
    {
        private ISessionDataAdapter _sessionDataAdapter { get; }
        private IConfiguration _configuration { get; }

        public AuthService(ISessionDataAdapter sessionDataAdapter,
            IConfiguration configuration)
        {
            _sessionDataAdapter = sessionDataAdapter;
            _configuration = configuration;
        }

        public Session CreateSession(User user, long expirationTimeSeconds, string sessionTypeCode) {
            SecurityToken securityToken;

            var jwtToken = GenerateJWTToken(user, expirationTimeSeconds, out securityToken);

            var session = new Session {
                UserId = user.UserId,
                Token = jwtToken,
                SessionStart = securityToken.ValidFrom,
                SessionStop = securityToken.ValidTo,
                SessionTypeCode = sessionTypeCode
            };

            return _sessionDataAdapter.Insert(session);
        }

        public string GenerateJWTToken(User user, long expirationTimeSeconds)
        {
            return GenerateJWTToken(user, expirationTimeSeconds, out var securityToken);
        }

        public string GenerateJWTToken(User user, long expirationTimeSeconds, out SecurityToken securityToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("name", user.Name),
                    new Claim("email", user.Email),
                    new Claim("picture", user.Picture),
                }),
                Expires = GetExpirationDateTime(expirationTimeSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            securityToken = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            return encryptedToken;
        }

        private DateTime GetExpirationDateTime(long? expirationTimeSeconds)
        {
            return expirationTimeSeconds != null ?
                DateTime.UtcNow.AddSeconds((long)expirationTimeSeconds) :
                DateTime.UtcNow.AddHours(1);
        }

        public void Logout(string token)
        {
            _sessionDataAdapter.ArchiveByToken(token);
        }
    }
}
