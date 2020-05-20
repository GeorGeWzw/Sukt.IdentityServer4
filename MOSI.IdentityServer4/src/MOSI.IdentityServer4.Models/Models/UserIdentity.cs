using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Models
{
    public class UserIdentity : IdentityUser<Guid>
    {
        /// <summary>
        /// 1=正常、2=锁定、3=禁用
        /// </summary>
        public AccountEnum AccountType { get; set; } = AccountEnum.Normal;
        /// <summary>
        /// 
        /// </summary>
        public ICollection<UserIdentityUserRole> UserRoles { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public ICollection<UserIdentityUserClaim> UserClaims { get; set; }
    }
}
