using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Shared.DbContexts;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace MOSI.IdentityServer4.Shared.AuthenticationExtensions
{
    /// <summary>
    /// 配置授权使用的用户实体和Role实体
    /// </summary>
    public static class Authentication
    {
        public static void AddAuthenticationServices(this IServiceCollection service)
        {
            service.AddIdentity<UserIdentity, UserIdentityRole>(
            //    options =>
            //{
            //    options.User.RequireUniqueEmail = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireDigit = false;

            //}
            )
                .AddEntityFrameworkStores<MosiIdentityDbContext>()
                //.AddClaimsPrincipalFactory<MyUserClaimsPrincipal>()
                .AddDefaultTokenProviders();
            //service.AddHttpContextAccessor();
            //service.AddTransient<IPrincipal>(provider =>
            //{
            //    IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
            //    return accessor?.HttpContext?.User;
            //});
            //.AddClaimsPrincipalFactory<MyUserClaimsPrincipal>();
        }
    }
}
