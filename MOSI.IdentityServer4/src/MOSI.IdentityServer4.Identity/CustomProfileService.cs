using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Shared.Constants;
using MOSI.IdentityServer4.Shared.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOSI.IdentityServer4.Identity
{
    /// <summary>
    /// 重写生成Token时的方法用于添加自定义Claim
    /// </summary>
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly MosiIdentityDbContext _mosiIdentity;

        public CustomProfileService(UserManager<UserIdentity> userManager, MosiIdentityDbContext mosiIdentity)
        {
            _userManager = userManager;
            _mosiIdentity = mosiIdentity;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var roleNamelist= await _userManager.GetRolesAsync(user);
            var roleid = await _mosiIdentity.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var claims = new List<Claim>();
            claims.AddRange(roleid.Select(x => new Claim(ClaimConsts.RoleId, x.ToString())));
            claims.AddRange(roleNamelist.Select(x => new Claim(ClaimConsts.Role, x)));
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null);
        }
    }
}
