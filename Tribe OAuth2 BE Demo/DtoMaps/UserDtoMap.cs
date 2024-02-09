using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.DtoMaps
{
    public class UserDtoMap : ClassMap<UserDto>
    {
        public UserDtoMap()
        {
            Table("USERS");

            Id(m => m.UserId).Column("USER_ID").GeneratedBy.Increment();

            Map(m => m.Name).Column("NAME");
            Map(m => m.Email).Column("EMAIL").Unique();
            Map(m => m.Picture).Column("PICTURE").Nullable();
            Map(m => m.CreatedAt).Column("CREATED_AT");
            Map(m => m.ModifiedAt).Column("MODIFIED_AT").Nullable();
            Map(m => m.Archived).Column("ARCHIVED");
        }
    }
}
