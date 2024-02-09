using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.DtoMaps
{
    public class CdSessionTypeDtoMap : ClassMap<CdSessionTypeDto>
    {
        public CdSessionTypeDtoMap()
        {
            Table("CD_SESSION_TYPE");

            Id(x => x.SessionTypeCode).Column("SESSION_TYPE_CD")
                .GeneratedBy.Assigned();

            Map(x => x.CreatedAt).Column("CREATED_AT")
                .Not.Nullable();

            Map(x => x.Archived).Column("ARCHIVED")
                .Default("0");
        }
    }
}
