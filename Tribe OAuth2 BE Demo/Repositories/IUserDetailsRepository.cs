using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Repositories
{
    public interface IUserDetailsRepository
    {
        UserDetailsDto FindByUserId(long userId);
        void Insert(UserDetailsDto dto);
    }
}
