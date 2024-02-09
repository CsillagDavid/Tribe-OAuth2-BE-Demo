using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models.Dtos
{
    public class UserDetailsDtoMap: ClassMap<UserDetailsDto>
    {
        public UserDetailsDtoMap() {
            Table("USER_DETAILS");

            Id(m => m.UserDetailsId).Column("USER_DETAILS_ID").GeneratedBy.Increment();

            Map(m => m.UserId).Column("USER_ID");
            Map(m => m.GivenName).Column("GIVEN_NAME").Nullable();
            Map(m => m.FamilyName).Column("FAMILY_NAME").Nullable();
            Map(m => m.Locale).Column("LOCALE").Nullable();
            Map(m => m.EmailVerified).Column("EMAIL_VERIFIED").Nullable();
            Map(m => m.HostedDomain).Column("HOSTED_DOMAIN").Nullable();
            Map(m => m.Prn).Column("PRN").Nullable();
            Map(m => m.Scope).Column("SCOPE").Nullable();
            Map(m => m.ModifiedAt).Column("MODIFIED_AT").Nullable();
            Map(m => m.CreatedAt).Column("CREATED_AT");
            Map(m => m.Archived).Column("ARCHIVED");
        }
    }
}
