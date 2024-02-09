using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;

namespace Tribe_OAuth2_BE_Demo.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private ISession _session { get; }
        public UserDetailsRepository(ISession session)
        {
            _session = session;
        }

        public UserDetailsDto FindByUserId
            (long userId)
        {
            return _session.QueryOver<UserDetailsDto>()
                .Where(x => x.UserId == userId)
                .SingleOrDefault();
        }

        public void Insert(UserDetailsDto dto)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(dto);
                transaction.Commit();
            }
        }
    }
}
