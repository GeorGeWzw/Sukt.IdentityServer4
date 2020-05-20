using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Shared.Constants;
using MOSI.IdentityServer4.Shared.DbContexts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOSI.IdentityServer4.Shared
{
    public class MyUserClaimsPrincipal : UserClaimsPrincipalFactory<UserIdentity>
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly MosiIdentityDbContext _mosiIdentity;
        private readonly IHttpContextAccessor _httpContext;
        //private readonly SignInManager<UserIdentity> _signInManager;
        //IUserStoreService
        //, SignInManager<UserIdentity> signInManager
        public MyUserClaimsPrincipal(UserManager<UserIdentity> userManager, MosiIdentityDbContext mosiIdentity, IOptions<IdentityOptions> optionsAccessor, IHttpContextAccessor httpContext) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _mosiIdentity = mosiIdentity;
            _httpContext = httpContext;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(UserIdentity user)
        {
            var principal = await base.CreateAsync(user);
            var claim=(await _userManager.GetClaimsAsync(user)).ToList();
            var list= await _mosiIdentity.UserClaims.Where(x => x.UserId == user.Id).ToListAsync();
            var roleid=await _mosiIdentity.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var identity= principal.Identity as ClaimsIdentity;
  
            identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            principal.AddIdentity(identity);
            //((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, "admin"));
            //principal.Identities.First().AddClaims(roleid.Select(x => new Claim(ClaimConsts.RoleId, x.ToString())));
            foreach (var item in principal.Claims)
            {
                System.Console.WriteLine($"{item.Type} {item.Value}");
            }
            System.Console.WriteLine("1");
            _httpContext.HttpContext.User = principal;
            return principal;
        }
    }
}
