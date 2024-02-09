using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using Tribe_OAuth2_BE_Demo.Repositories;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public class SessionDataAdapter: ISessionDataAdapter
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public SessionDataAdapter(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public void ArchiveByToken(string token)
        {
            _sessionRepository.ArchiveByToken(token);
        }

        public Session FindByUserId(int userId)
        {
            var sessionDto = _sessionRepository.FindByUserId(userId);
            return _mapper.Map<Session>(sessionDto);
        }

        public Session Insert(Session session)
        {
            var sessionDto = _mapper.Map<SessionDto>(session);
            _sessionRepository.Insert(sessionDto);
            return _mapper.Map<Session>(sessionDto);
        }
    }
}
