using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Enums;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public interface IAuthService
    {
        Session CreateSession(User user, long expirationTimeSeconds, string sessionTypeCode);
        string GenerateJWTToken(User user, long expirationTimeSeconds);
        string GenerateJWTToken(User user, long expirationTimeSeconds, out SecurityToken securityToken);
        void Logout(string token);
    }
}