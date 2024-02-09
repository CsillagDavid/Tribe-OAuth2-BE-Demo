using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models
{
    public class Session
    {
        public long SessionId { get; set; }
        public long UserId { get; set; }
        public string SessionTypeCode { get; set; }
        public string Token { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionStop { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool Archived { get; set; }
    }
}
