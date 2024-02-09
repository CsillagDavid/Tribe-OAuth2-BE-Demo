using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tribe_OAuth2_BE_Demo.Models.Dtos;

namespace Tribe_OAuth2_BE_Demo.Models
{
    public class UserDetailsDto: BaseDto
    {
        public virtual long UserDetailsId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string GivenName { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string Locale { get; set; }
        public virtual bool EmailVerified { get; set; }
        public virtual string HostedDomain { get; set; }
        public virtual string Prn { get; set; }
        public virtual string Scope { get; set; }
    }

}
