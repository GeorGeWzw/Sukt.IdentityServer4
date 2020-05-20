using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using MOSI.IdentityServer4.Shared.DcContextInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOSI.IdentityServer4.Shared.DbContexts
{
    /// <summary>
    /// API资源和客户端上下文
    /// </summary>
    public class MosiConfigurationDbContext : ConfigurationDbContext<MosiConfigurationDbContext>, IMosiConfigurationDbContext
    {
        /// <summary>
        /// API资源和客户端上下文构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="storeOptions"></param>
        public MosiConfigurationDbContext(DbContextOptions<MosiConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {

        }

        public DbSet<ApiSecret> ApiSecrets { get ; set ; }
        public DbSet<ApiScope> ApiScopes { get ; set ; }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get ; set ; }
        public DbSet<IdentityClaim> IdentityClaims { get ; set ; }
        public DbSet<ApiResourceClaim> ApiResourceClaims { get ; set ; }
        public DbSet<ClientGrantType> ClientGrantTypes { get ; set ; }
        public DbSet<ClientScope> ClientScopes { get ; set ; }
        public DbSet<ClientSecret> ClientSecrets { get ; set ; }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get ; set ; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get ; set ; }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get ; set ; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get ; set ; }
        public DbSet<ClientClaim> ClientClaims { get ; set ; }
        public DbSet<ClientProperty> ClientProperties { get ; set ; }
        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get ; set ; }
        public DbSet<ApiResourceProperty> ApiResourceProperties { get ; set ; }
    }
}
