using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using Tribe_OAuth2_BE_Demo.Repositories.Interfaces;

namespace Tribe_OAuth2_BE_Demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ISession _session { get; }
        public UserRepository(ISession session)
        {
            _session = session;
        }

        public UserDto FindUserByEmail(string email)
        {
            return _session.QueryOver<UserDto>()
                .Where(x => x.Email == email)
                .SingleOrDefault();
        }

        public void Insert(UserDto dto)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(dto);
                transaction.Commit();
            }
        }
    }
}
