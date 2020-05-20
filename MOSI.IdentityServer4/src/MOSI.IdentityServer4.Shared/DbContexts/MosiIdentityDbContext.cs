using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOSI.IdentityServer4.Models;
using MOSI.IdentityServer4.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Shared.DbContexts
{
    public class MosiIdentityDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, Guid, UserIdentityUserClaim, UserIdentityUserRole, UserIdentityUserLogin,UserIdentityRoleClaim, UserIdentityUserToken>
    {
        public MosiIdentityDbContext(DbContextOptions<MosiIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureIdentityContext(builder);
        }

        private void ConfigureIdentityContext(ModelBuilder builder)
        {
            builder.Entity<UserIdentityRole>().ToTable(TableConsts.IdentityRoles);//角色表
            builder.Entity<UserIdentityRoleClaim>().ToTable(TableConsts.IdentityRoleClaims);//角色claim
            builder.Entity<UserIdentityUserRole>().ToTable(TableConsts.IdentityUserRoles);//用户角色

            builder.Entity<UserIdentity>().ToTable(TableConsts.IdentityUsers);//用户表
            builder.Entity<UserIdentityUserLogin>().ToTable(TableConsts.IdentityUserLogins);//用户登陆表
            builder.Entity<UserIdentityUserClaim>().ToTable(TableConsts.IdentityUserClaims);//用户claim表
            builder.Entity<UserIdentityUserToken>().ToTable(TableConsts.IdentityUserTokens);//用户Token表
        }
    }

    
}
