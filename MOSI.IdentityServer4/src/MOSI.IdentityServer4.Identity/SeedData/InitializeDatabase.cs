using IdentityModel;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Shared.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOSI.IdentityServer4.Identity.SeedData
{
    public static class InitializeDatabase
    {
        public static void InitializeDatabaseConfiguration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<MosiPersistedGrantDbContext>().Database.Migrate();
                var context = serviceScope.ServiceProvider.GetRequiredService<MosiConfigurationDbContext>();
                //EnumeSeedDataConfig(context);
                {
                    var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();
                    var roleMgr = serviceScope.ServiceProvider.GetRequiredService<RoleManager<UserIdentityRole>>();

                    var userItem = new UserIdentity
                    {
                        UserName = "wangzewei",
                        NormalizedUserName = "wangzewei",
                    };
                    var pwdInit = "Admin@123";//初始密码
                    var result = userMgr.CreateAsync(userItem, pwdInit).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, userItem.UserName),
                        new Claim(JwtClaimTypes.Email, $"{userItem.UserName}@qq.com"),
                        new Claim(JwtClaimTypes.Role, "83f3eecb-5606-45a6-8c9c-c76fba7bda8c"),
                    };
                    var claimsresult = userMgr.AddClaimsAsync(userItem, claims).Result;//添加用户角色
                    if (!claimsresult.Succeeded)
                    {
                        throw new Exception(claimsresult.Errors.First().Description);
                    }

                    //var roleItem = new UserIdentityRole
                    //{
                    //    Id=Guid.NewGuid(),
                    //    Name="admin",
                    //    NormalizedName="admin",
                    //};
                    //var roleresult = roleMgr.CreateAsync(roleItem).Result;
                    //if (!roleresult.Succeeded)
                    //{
                    //    throw new Exception(roleresult.Errors.First().Description);
                    //}
                }

            }
        }
        /// <summary>
        /// 资源服务传入上下文
        /// </summary>
        /// <param name="context"></param>
        private static void EnumeSeedDataConfig(MosiConfigurationDbContext context)
        {
            try
            {
                //客户端写入
                foreach (var item in Config.GetClients())
                {
                    context.Clients.Add(item.ToEntity());
                }
                context.SaveChanges();
                //资源认证方式写入
                foreach (var item in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(item.ToEntity());
                }
                context.SaveChanges();
                //API资源服务器写入
                foreach (var item in Config.GetApiResources())
                {
                    context.ApiResources.Add(item.ToEntity());
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        private static void SetUserandRole()
        {

        }
    }
}
