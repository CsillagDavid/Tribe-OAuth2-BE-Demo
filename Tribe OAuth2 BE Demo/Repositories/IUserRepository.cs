using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.Repositories.Interfaces
{
    public interface IUserRepository
    {
        UserDto FindUserByEmail(string email);
        void Insert(UserDto dto);
    }
}
