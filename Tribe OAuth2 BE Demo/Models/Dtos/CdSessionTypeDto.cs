using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tribe_OAuth2_BE_Demo.Models.Dtos
{
    public class CdSessionTypeDto
    {
        public virtual string SessionTypeCode { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool Archived { get; set; }
    }
}
