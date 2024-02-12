using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Services
{
    public interface IUserService
    {
        UserDetails GetUserDetails(string email);
    }
}
