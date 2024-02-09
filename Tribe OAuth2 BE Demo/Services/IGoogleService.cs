using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public interface IGoogleService
    {
        Task<dynamic> Login(string creditenal);
        Task<dynamic> Signup(string creditenal);

        public User NHibernate(string email);
        Task<dynamic> TestToken(string creditenals);
    }
}
