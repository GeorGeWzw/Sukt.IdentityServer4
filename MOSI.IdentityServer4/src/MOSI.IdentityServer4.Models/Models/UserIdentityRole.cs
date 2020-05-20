using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models
{
    public class UserIdentityRole : IdentityRole<Guid>
    {
        public ICollection<UserIdentityUserRole> UserRoles { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDrop { get; set; } = false;
    }
}
