using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models
{
    public class UserIdentityUserRole: IdentityUserRole<Guid>
    {
        public virtual UserIdentity User { get; set; }
        public virtual UserIdentityRole Role { get; set; }


    }
}
