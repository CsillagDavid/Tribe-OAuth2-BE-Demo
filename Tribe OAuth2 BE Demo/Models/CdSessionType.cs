using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models
{
    public class CdSessionType
    {
        public string SessionTypeCd { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Archived { get; set; }
    }
}
