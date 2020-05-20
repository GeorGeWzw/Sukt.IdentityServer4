using IdentityServer4.EntityFramework.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MOSI.IdentityServer4.Shared.DbContexts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MOSI.IdentityServer4.EntityFramework.MySql.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// 注册数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="conn"></param>
        public static void RegisterMySqlDbContexts(this IServiceCollection services,string conn)
        {
            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<MosiIdentityDbContext>(options => options.UseMySql(conn,sql=>sql.MigrationsAssembly(migrationsAssembly)));

            services.AddConfigurationDbContext<MosiConfigurationDbContext>(options => options.ConfigureDbContext = b => b.UseMySql(conn, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddOperationalDbContext<MosiPersistedGrantDbContext>(opt => opt.ConfigureDbContext = b => b.UseMySql(conn, sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
