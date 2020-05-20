using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models
{
    public class UserIdentityUserClaim: IdentityUserClaim<Guid>
    {
        //public virtual UserIdentity User { get; set; }
    }
}
