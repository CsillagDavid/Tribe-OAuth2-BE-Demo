using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models;
using Tribe_OAuth2_BE_Demo.Models.Dtos;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tribe_OAuth2_BE_Demo.Mappers
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile() {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(s => s.CreatedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(s => s.ModifiedAt, opt => opt.Ignore())
                .ForMember(s => s.UserId, opt => opt.Ignore());

            CreateMap<UserDetails, UserDetailsDto>();
            CreateMap<UserDetailsDto, UserDetails>()
                .ForMember(s => s.ModifiedAt, opt => opt.Ignore())
                .ForMember(s => s.CreatedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(s => s.UserDetailsId, opt => opt.Ignore());

            CreateMap<Payload, UserDetails>();
            CreateMap<Payload, User>();

            CreateMap<SessionDto, Session>();
            CreateMap<Session, SessionDto>()
                .ForMember(s => s.ModifiedAt, opt => opt.Ignore())
                .ForMember(s => s.CreatedAt, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(s => s.SessionId, opt => opt.Ignore());

            CreateMap<CdSessionType, CdSessionTypeDto>();
            CreateMap<CdSessionTypeDto, CdSessionType>();
        }
    }
}
