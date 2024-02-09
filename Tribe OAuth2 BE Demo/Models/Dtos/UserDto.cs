using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class UserDto: BaseDto
    {
        public virtual long UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Picture { get; set; }
    }
}
