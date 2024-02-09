using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using Tribe_OAuth2_BE_Demo.Repositories.Interfaces;

namespace Tribe_OAuth2_BE_Demo.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private ISession _session { get; }
        public SessionRepository(ISession session)
        {
            _session = session;
        }

        public SessionDto FindByUserId(int userId)
        {
            return _session.QueryOver<SessionDto>()
                .Where(x => x.UserId == userId)
                .SingleOrDefault();
        }

        public void Insert(SessionDto dto)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(dto);
                transaction.Commit();
            }
        }

        public void ArchiveByToken(string token)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var oldSessionDto =_session.QueryOver<SessionDto>()
                    .Where(x => x.Token == token)
                    .SingleOrDefault();

                oldSessionDto.ModifiedAt = DateTime.Now;
                oldSessionDto.Archived = true;

                _session.Update(oldSessionDto);
                transaction.Commit();
            }
        }
    }
}
