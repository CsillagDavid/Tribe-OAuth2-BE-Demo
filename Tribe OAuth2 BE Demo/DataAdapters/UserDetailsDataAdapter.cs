using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Repositories;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public class UserDetailsDataAdapter : IUserDetailsDataAdapter
    {
        private IUserDetailsRepository _userDetailsRepository { get; }
        private IMapper _mapper { get; }
        public UserDetailsDataAdapter(IUserDetailsRepository userDetailsRepository, IMapper mapper)
        {
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public UserDetails FindByUserId(long userId)
        {
            var dto = _userDetailsRepository.FindByUserId(userId);
            return _mapper.Map<UserDetails>(dto);
        }

        public UserDetails Insert(UserDetails userDetails)
        {
            var userDetailsDto = _mapper.Map<UserDetailsDto>(userDetails);
            _userDetailsRepository.Insert(userDetailsDto);
            return _mapper.Map<UserDetails>(userDetailsDto);
        }
    }
}
