using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models.Dtos
{
    public class SessionDto
    {
        public virtual long SessionId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string SessionTypeCode { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime SessionStart { get; set; }
        public virtual DateTime SessionStop { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? ModifiedAt { get; set; }
        public virtual bool Archived { get; set; }
    }
}
