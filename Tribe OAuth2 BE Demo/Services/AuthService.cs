using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public class AuthService : IAuthService
    {
        public string GenerateJWTToken(User user, long? expirationTimeSeconds)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("name", user.Name),
                    new Claim("email", user.Email),
                    new Claim("picture", user.Picture),
                }),
                Expires = GetExpirationDateTime(expirationTimeSeconds)
            };

            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = jwtSecurityTokenHandler.WriteToken(token);
            
            return encryptedToken;
        }

        private DateTime GetExpirationDateTime(long? expirationTimeSeconds)
        {
            return expirationTimeSeconds != null ?
                DateTime.UtcNow.AddSeconds((long)expirationTimeSeconds) :
                DateTime.UtcNow.AddHours(1);
        }
    }
}
