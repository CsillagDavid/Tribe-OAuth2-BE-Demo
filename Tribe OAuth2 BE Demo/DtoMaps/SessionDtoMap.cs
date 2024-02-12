using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.DtoMaps
{
    public class SessionDtoMap : ClassMap<SessionDto>
    {
        public SessionDtoMap()
        {
            Table("SESSIONS");

            Id(x => x.SessionId).Column("SESSION_ID").GeneratedBy.Increment();

            Map(x => x.UserId).Column("USER_ID");
            Map(x => x.SessionTypeCode).Column("SESSION_TYPE_CD");
            Map(x => x.Token).Column("TOKEN");
            Map(x => x.SessionStart).Column("SESSION_START");
            Map(x => x.SessionStop).Column("SESSION_STOP");
            Map(x => x.CreatedAt).Column("CREATED_AT");
            Map(x => x.ModifiedAt).Column("MODIFIED_AT").Nullable();
            Map(x => x.Archived).Column("ARCHIVED");
        }
    }
}
