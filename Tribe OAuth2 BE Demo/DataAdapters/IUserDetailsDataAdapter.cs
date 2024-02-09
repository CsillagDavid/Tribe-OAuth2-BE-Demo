using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public interface IUserDetailsDataAdapter
    {
        UserDetails FindByUserId(long userId);
        UserDetails Insert(UserDetails userDetails);
    }
}
