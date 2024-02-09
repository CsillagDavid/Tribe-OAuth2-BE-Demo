using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public interface ISessionDataAdapter
    {
        Session FindByUserId(int userId);
        Session Insert(Session session);
        void ArchiveByToken(string token);
    }
}
