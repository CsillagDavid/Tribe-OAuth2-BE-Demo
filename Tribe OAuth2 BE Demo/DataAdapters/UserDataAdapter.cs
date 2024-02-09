using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using Tribe_OAuth2_BE_Demo.Repositories.Interfaces;

namespace Tribe_OAuth2_BE_Demo.DataAdapters
{
    public class UserDataAdapter : IUserDataAdapter
    {
        private IMapper _mapper { get; }
        private IUserRepository _userRepository { get; }
        public UserDataAdapter(IUserRepository userRepository,
            IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User FindUserByEmail(string email)
        {
            var userDto = _userRepository.FindUserByEmail(email);
            return _mapper.Map<User>(userDto);
        }

        public User Insert(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            _userRepository.Insert(userDto);
            return _mapper.Map<User>(userDto);
        }
    }
}
